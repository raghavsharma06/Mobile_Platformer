using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // provides access to Unity UI
using DG.Tweening;                  // for using DoTween

/// <summary>
/// Handles the Level Complete AI
/// </summary>
public class LevelCompleteCtrl : MonoBehaviour
{

    public Button btnNext;          // this button loads the next level
    public Sprite goldenStar;       // awarded when score is above a certain value
    public Image Star1;             // the UI image for a white star
    public Image Star2;             // same as Star1
    public Image Star3;             // same as Star1
    public Text txtScore;           // for showing the score
    public int levelNumber;         // which level is this

    [HideInInspector]
    public int score;               // the current score   set[HideInInspector] after dev/testing
    public int ScoreForThreeStars;  // score required for earning 3 stars
    public int ScoreForTwoStars;    // score required for earning 2 stars
    public int ScoreForOneStar;     // score required for earning 1 star
    public int ScoreForNextLevel;   // score rquired for unlocking the next level
    public float animStartDelay;    // brief delay in seconds before golden stars are awarded
    public float animDelay;         // animation delay between each golden star 0.7f

    bool showTwoStars, showThreeStars;  // for checking how many stars to show


    void Start()
    {
        // enable when deploying/beta testing
        score = GameCtrl.instance.GetScore();   // get the current score

        // update the score text
        txtScore.text = "" + score;

        if(score >= ScoreForThreeStars)
        {
            showThreeStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 3);
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if (score >= ScoreForTwoStars && score < ScoreForThreeStars)
        {
            showTwoStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 2);
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if (score <= ScoreForOneStar && score != 0)
        {
            GameCtrl.instance.SetStarsAwarded(levelNumber, 1);
            Invoke("ShowGoldenStars", animStartDelay);
        }
    }

    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Star1);
    }

    IEnumerator HandleFirstStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // cause a delay before showing the next star
        yield return new WaitForSeconds(animDelay);

        // called if more than one star is awarded
        if (showTwoStars || showThreeStars)
            StartCoroutine("HandleSecondStarAnim", Star2);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // cause a delay before showing the next star
        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;

        if (showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // cause a delay before showing the next star
        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;

        Invoke("CheckLevelStatus", 1.2f);

    }

    void CheckLevelStatus()
    {
        // ---- unlock the next level if a certain score is reached
        if(score >= ScoreForNextLevel)
        {
            btnNext.interactable = true;

            // show a nice particle effect
            SFXCtrl.instance.ShowBulletSparkle(btnNext.gameObject.transform.position);

            // play some audio
            AudioCtrl.instance.KeyFound(btnNext.gameObject.transform.position);

            // unlock the next level
            GameCtrl.instance.UnlockLevel(levelNumber );
        }
        else
        {
            btnNext.interactable = false;
        }
    }

    void DoAnim(Image starImg)
    {
        // increase the star size
        starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);

        // show the golden star
        starImg.sprite = goldenStar;

        // reduce the star size to normal using DoTween animation
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);

        // play an audio effect
        AudioCtrl.instance.KeyFound(starImg.gameObject.transform.position);

        // show a sparkle effect
        SFXCtrl.instance.ShowBulletSparkle(starImg.gameObject.transform.position); 
    }

    void Update()
    {

    }
}
