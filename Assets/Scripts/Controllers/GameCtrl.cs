using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;                                   // gets access to the Unity UI elements
using System.IO;                                        // for working with files
using System.Runtime.Serialization.Formatters.Binary;   // RSFB helps Serialization
using DG.Tweening;

/// <summary>
/// Manages the important features like keeping the score, restarting levels,
/// saving/loading data, updating the HUD etc
/// </summary>
public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    public GameData data;                       // to work with game data in the inspector
    public UI ui;                               // for neatly arranging UI elements
    public GameObject bigCoin;                  // reward the cat gets on killing the enemy
    public GameObject player;                   // the cat game character
    public GameObject lever;                    // the lever which releases the dog
    public GameObject enemySpawner;             // spawns the enemies during boss battle
    public GameObject signPlatform;             // the one that leads to the boss battle
    
    public int coinValue;                       // value of one small coin
    public int bigCoinValue;                    // value of one big coin
    public int enemyValue;                      // value of one enemy
    public float maxTime;                       // max time allowed to compelete the level

    public enum Item
    {
        Coin,
        BigCoin,
        Enemy
    }

    string dataFilePath;                        // path to store the data file
    BinaryFormatter bf;                         // help in saving/loading to binary files
    float timeLeft;                             // time left before the timer goes off
    bool timerOn;                               // checks if timer should be on or off
    bool isPaused;


	void Awake()
	{
        if (instance == null)
            instance = this;

        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";
       // Debug.Log(dataFilePath);
	}

	void Start()
	{
        DataCtrl.instance.RefreshData();
        data = DataCtrl.instance.data;
        timeLeft = maxTime;

        timerOn = true;
        isPaused = false;
        HandleFirstBoot();

        UpdateHearts();

        ui.bossHealth.gameObject.SetActive(false);
	}

	void Update()
	{
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (timeLeft > 0 && timerOn)
            UpdateTimer();
	}

    public void DeleteCheckPoint()
    {
        PlayerPrefs.DeleteKey("CPX");
        PlayerPrefs.DeleteKey("CPY");
    }
  

    public void RefreshUI()
    {
            ui.txtCoinCount.text = " x " + data.coinCount;
            ui.txtScore.text = "Score: " + data.score;
    }

   

    /// <summary>
    /// Saves the stars awarded for a level
    /// </summary>
    /// <param name="levelNumber">Level number.</param>
    /// <param name="numOfStars">Number of stars.</param>
    public void SetStarsAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;

        // print star count in console for testing
       // Debug.Log("Number of Stars Awarded = " + data.levelData[levelNumber].starsAwarded);
    }

    /// <summary>
    /// Unlocks the next level
    /// </summary>
    /// <param name="levelNumber">Level number.</param>
    public void UnlockLevel(int levelNumber)
    {
        if((levelNumber+1) <= (data.levelData.Length-1))
            data.levelData[levelNumber+1].isUnlocked = true;

        DataCtrl.instance.data.levelData[levelNumber].isUnlocked = true;
    }

    /// <summary>
    /// Gets the current score for Level Complete Menu
    /// </summary>
    /// <returns>The score.</returns>
    public int GetScore()
    {
        return data.score;
    }

	void OnEnable()
	{
	//	Debug.Log("Data Loaded");
        RefreshUI();
	}

    void OnDisable()
    {
      //  Debug.Log("Data Saved");
        DataCtrl.instance.SaveData(data);

        Time.timeScale = 1;
        // hide the ad banner 
        AdsCtrl.instance.hideBanner();
    }

	/// <summary>
	/// called when the player dies
	/// </summary>
	public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        CheckLives();
        //Invoke("RestartLevel", restartDelay);

    }

    public void PlayerDiedAnimation(GameObject player)
    {
        // throw the player back in the air
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f, 400f));

        // rotate the player a bit
        player.transform.Rotate(new Vector3(0, 0, 45f));

        // disable the PlayerCtrl script
        player.GetComponent<PlayerCtrl>().enabled = false;

        // disable the colliders attached to the player so that the player can
        // fall through the ground
        foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }

        // disable the child gameobjects attached to the player cat
        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        // disable the camera attached with the player cat
        Camera.main.GetComponent<CameraCtrl>().enabled = false;

        // set the velocity of the cat to zero
        rb.velocity = Vector2.zero;

        // restart level
        StartCoroutine("PauseBeforeReload", player);
    }

    public void PlayerStompsEnemy(GameObject enemy)
    {
        // change the enemy's tag
        enemy.tag = "Untagged";

        // destroy the enemy
        Destroy(enemy);

        // updat the score
        UpdateScore(Item.Enemy);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f);  // causes a specified delay
        PlayerDied(player);
    }

    /// <summary>
    /// called when the player falls in water
    /// </summary>
    public void PlayerDrowned(GameObject player)
    {
        CheckLives();
        //Invoke("RestartLevel", restartDelay);
    }

    public void UpdateCoinCount()
    {
        data.coinCount += 1;

        ui.txtCoinCount.text = " x " + data.coinCount;

    }

    public void BulletHitEnemy(Transform enemy)
    {
        // show the enemy explosion SFX
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        // show the big coin
        Instantiate(bigCoin, pos, Quaternion.identity);

        // destroy the enemy
        Destroy(enemy.gameObject);

        AudioCtrl.instance.EnemyExplosion(pos);

        // update the score
    }

    public void UpdateScore(Item item)
    {
        int itemValue = 0;

        switch (item)
        {
            case Item.BigCoin:
                itemValue = bigCoinValue;
                break;
            case Item.Coin:
                itemValue = coinValue;
                break;
            case Item.Enemy:
                itemValue = enemyValue;
                break;
            default:
                break;
        }

        data.score += itemValue;

        ui.txtScore.text = "Score: " + data.score;
    }

    public void UpdateKeyCount(int keyNumber)
    {
        data.keyFound[keyNumber] = true;

        if (keyNumber == 0)
            ui.key0.sprite = ui.key0Full;
        else if (keyNumber == 1)
            ui.key1.sprite = ui.key1Full;
        if (keyNumber == 2)
            ui.key2.sprite = ui.key2Full;

        if (data.keyFound[0] && data.keyFound[1])
            ShowSignPlatform();
    }

    void ShowSignPlatform()
    {
        signPlatform.SetActive(true);

        SFXCtrl.instance.ShowPlayerLanding(signPlatform.transform.position);

        timerOn = false;

        ui.bossHealth.gameObject.SetActive(true);
    }

    public void LevelComplete()
    {
        
        ui.panelMobileUI.SetActive(false);
        ui.levelCompleteMenu.SetActive(true);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;

        ui.txtTimer.text = "Timer: " + (int)timeLeft;

        if(timeLeft <=0)
        {
            ui.txtTimer.text = "Timer: 0";

            // inform the GameCtrl to do the needful
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerDied(player);
        }
    }

    void HandleFirstBoot()
    {
        if(data.isFirstBoot)
        {
            // set lives to 3
            data.lives = 3;

            // set number of coins to 0
            data.coinCount = 0;

            // set keys collected to 0
            data.keyFound[0] = false;
            data.keyFound[1] = false;
            data.keyFound[2] = false;

            // set score to 0
            data.score = 0;

            // set isFirstBoot to false
            data.isFirstBoot = false;
        }
    }

    void UpdateHearts()
    {
        if (data.lives == 3)
        {
            ui.heart1.sprite = ui.fullHeart;
            ui.heart2.sprite = ui.fullHeart;
            ui.heart3.sprite = ui.fullHeart;
        }

        if(data.lives == 2)
        {
            ui.heart1.sprite = ui.emptyHeart;
        }

        if (data.lives == 1)
        {
            ui.heart2.sprite = ui.emptyHeart;
            ui.heart1.sprite = ui.emptyHeart;
        }
    }

    void CheckLives()
    {
        int updatedLives = data.lives;
        updatedLives -= 1;
        data.lives = updatedLives;

        if(data.lives == 0)
        {
            data.lives = 3;
            DataCtrl.instance.SaveData(data);
            Invoke("GameOver", restartDelay);
        }
        else
        {
            DataCtrl.instance.SaveData(data);
            Invoke("RestartLevel", restartDelay);
        }
    }

    public void StopCameraFollow()
    {
        Camera.main.GetComponent<CameraCtrl>().enabled = false;
        player.GetComponent<PlayerCtrl>().isStuck = true;       // stops parallax
        player.transform.Find("Left_Check").gameObject.SetActive(false);
        player.transform.Find("Right_Check").gameObject.SetActive(false);
    }

    public void ShowLever()
    {
        lever.SetActive(true);

        DeactivateEnemySpawner();

        SFXCtrl.instance.ShowPlayerLanding(lever.gameObject.transform.position);

        AudioCtrl.instance.EnemyExplosion(lever.gameObject.transform.position);
    }

    public void ActivateEnemySpawner()
    {
        enemySpawner.SetActive(true);
    }

    public void DeactivateEnemySpawner()
    {
        enemySpawner.SetActive(false);
    }

    void GameOver()
    {
        // todo
        if (timerOn)
            timerOn = false;
        AdsCtrl.instance.showBanner();
        AdsCtrl.instance.ShowInterstitialAd();
        DeleteCheckPoint();
        ui.panelGameOver.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.7f, false);
    }

    public void ShowPauseMenu()
    {
        if (ui.panelMobileUI.activeInHierarchy)
            ui.panelMobileUI.SetActive(false);

        ui.paneluPause.SetActive(true);

        ui.paneluPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.7f, false);
        AdsCtrl.instance.showBanner();
        AdsCtrl.instance.ShowInterstitialAd();
        Invoke("SetPause", 1.1f);

    }

    void SetPause()
    {
        isPaused = true;
    }

    public void HidePauseMenu()
    {
        isPaused = false;

        if (!ui.panelMobileUI.activeInHierarchy)
           ui.panelMobileUI.SetActive(true);
        //ui.paneluPause.SetActive(false);
        AdsCtrl.instance.hideBanner();
        
        ui.paneluPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(600f, 0.7f, false);
        //AdsCtrl.instance.hideBanner();
        
    }
}
