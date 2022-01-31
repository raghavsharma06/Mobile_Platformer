using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCtrl : MonoBehaviour
{
    public string FacebookUrl, TwitterUrl, GooglePlusUrl, RatingUrl;
   
    public void facebookUrl()
    {
        Application.OpenURL(FacebookUrl);
    }
    public void twitterUrl()
    {
        Application.OpenURL(TwitterUrl);
    }
    public void googlePlusUrl()
    {
        Application.OpenURL(GooglePlusUrl);
    }
    public void ratingsUrl()
    {
        Application.OpenURL(RatingUrl);
        
    }

}
