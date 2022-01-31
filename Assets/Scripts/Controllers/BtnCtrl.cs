using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnCtrl : MonoBehaviour

{
    int levelnumber;  // to get the level number 
    Button btn;        // button attached to this script
    Image btnImg;
    Text btntext;
    Transform star1, star2, star3;

    public Sprite locked;
    public Sprite unclocked;
    public string scenename;
    // Start is called before the first frame update
    void Start()
    {
        levelnumber = int.Parse(transform.gameObject.name);  // as the name set is the button number but that is text so we need to convert it into an integer
        btn = transform.gameObject.GetComponent<Button>();  // reference to the button attached
        btnImg = btn.GetComponent<Image>();                 // reference to the image object 
        btntext = btn.gameObject.transform.GetChild(0).GetComponent<Text>(); // reference to the child which is the text 
        star1 = btn.gameObject.transform.GetChild(1);
        star2 = btn.gameObject.transform.GetChild(2);
        star3 = btn.gameObject.transform.GetChild(3);

        ButtonStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonStatus()
    {
        bool unlocked=DataCtrl.instance.isUnlocked(levelnumber);
        int starsAwarded = DataCtrl.instance.StarsAwarded(levelnumber);

        if (unlocked)
        {
            if (starsAwarded == 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);

            }
            if (starsAwarded == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);
            }
            if (starsAwarded == 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
            if (starsAwarded == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
        }
        else
        {
            btnImg.overrideSprite = locked;   // override spirte basically converts the image type to sprite type

            // dont show any stars 
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            //button text 

            btntext.text = "";
        }

    }

   public void loadScene()
    {
        LoadingCtrl.instance.ShowLoadingScreen();
        if(DataCtrl.instance.data.levelData[levelnumber].isUnlocked)
        SceneManager.LoadScene(scenename);
    }
}
