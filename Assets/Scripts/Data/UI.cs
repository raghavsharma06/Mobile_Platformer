using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;                       // this gives us access to the [Serialable] attribute
using UnityEngine.UI;               // this gives us access to the Unity UI elements

/// <summary>
/// Groups all the user interface elements together for GameCtrl to use
/// </summary>

[Serializable]
public class UI 
{
    [Header("Text")]
    public Text txtCoinCount;          // for updating the number of coins collected
    public Text txtScore;              // for showing the score in the HUD
    public Text txtTimer;              // for showing the timer in the HUD

    [Header("Images / Sprites")]
    public Image key0;
    public Image key1;
    public Image key2;
    public Sprite key0Full;
    public Sprite key1Full;
    public Sprite key2Full;
    public Image heart1;                // represents one player life
    public Image heart2;                // represents one player life
    public Image heart3;                // represents one player life
    public Sprite emptyHeart;           // shown when one life is lost
    public Sprite fullHeart;            // shown when lives are full
    public Slider bossHealth;           // the health meter of the level boss

    [Header("Images / Sprites")]
    public GameObject panelGameOver;    // the game over panel
    public GameObject levelCompleteMenu;        // shown when a level is completed
    public GameObject panelMobileUI;
    public GameObject paneluPause;



}
