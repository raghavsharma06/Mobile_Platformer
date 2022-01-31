using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCtrl : MonoBehaviour
{
    public GameObject panelLoading;
    public static LoadingCtrl instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    public void ShowLoadingScreen()
    {
        panelLoading.SetActive(true);
    }
}
