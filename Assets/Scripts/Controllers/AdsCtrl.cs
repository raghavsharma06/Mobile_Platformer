using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsCtrl : MonoBehaviour
{
    public static AdsCtrl instance = null;
    public string Android_Admob_BannerId;
    public string Android_Admob_Interstitial;
    public bool TestMode;
    public bool showbanner;
    public bool showinterstitial;

    BannerView bannerview;
    AdRequest request;
    InterstitialAd interstitial;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void RequestBanner()
    {
        if (TestMode)
        {
            // create a banner of 320X50 
            bannerview = new BannerView(Android_Admob_BannerId, AdSize.Banner, AdPosition.Top);
        }
        else
        {
            // load live ad (for production)
        }

        // request  the banner
        AdRequest adRequest = new AdRequest.Builder().Build();

        // load the ad 
        bannerview.LoadAd(adRequest);
        hideBanner();
    }

    public void showBanner()
    {
        if(showbanner)
        bannerview.Show();

    }


    public void hideBanner()
    {
        if(showbanner)
        bannerview.Hide();

    }

    public void hideBanner(float duration)
    {
        StartCoroutine("DelayHideBanner", duration);
    }

    IEnumerator DelayHideBanner(float duration)
    {
        yield return new WaitForSeconds(duration);
        bannerview.Hide();
    }

    public void RequestInterstitialAd()
    {
        if (TestMode)
        {
            // create an inerstital ad
            interstitial = new InterstitialAd(Android_Admob_Interstitial);
        }
        else
        {
            //live interstitial ad
        }

        // create a request 
        request = new AdRequest.Builder().Build();

        // load the ad 
        interstitial.LoadAd(request);

        interstitial.OnAdClosed += HandleOnAdClosed;
    }
    public void HandleOnAdClosed(object sender,EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        if (showinterstitial)
        {

            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }
    }

    private void OnEnable()
    {
        if(showbanner)
             RequestBanner();

        if(showinterstitial)
            RequestInterstitialAd();
    }

    private void OnDisable()
    {
        if(showbanner)
            bannerview.Destroy();

        if(showinterstitial)
        interstitial.Destroy();
    }
}
