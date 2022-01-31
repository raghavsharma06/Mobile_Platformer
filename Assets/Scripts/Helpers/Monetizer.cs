using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monetizer : MonoBehaviour
{
    public bool timedBanner;
    public float bannerDuration;
    // Start is called before the first frame update
    void Start()
    {
        AdsCtrl.instance.showBanner();
    }

    
    void OnDisable()
    {
        if (!timedBanner)
        {
            AdsCtrl.instance.hideBanner();
        }
        else AdsCtrl.instance.hideBanner(bannerDuration);
    }
}
