using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the audio in the game
/// </summary>
public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl instance;           // for calling public methods in this script
    public PlayerAudio playerAudio;             // for accessing player audio effects
    public AudioFX audioFX;                     // for accessing non player audio
    public Transform player;                    // we need this to play sund at player position
    public GameObject BGMusic;                  // to toggle BG Music
    public bool bgMusicOn;                      // to toggle BG Music
    public GameObject btnsound, btnMusic;
    public Sprite imgsoundON, imgSoundOff;
    public Sprite imgMusicOn, imgMusicOff;

    [Tooltip("soundOn is used to toggle sound on/off from the Inspector")]
    public bool soundOn;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (DataCtrl.instance.data.PlayMusic)
        {
            BGMusic.SetActive(true);
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
        }
        else
        {
            BGMusic.SetActive(false);
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
        }


        if (DataCtrl.instance.data.PlaySound)
        {
            soundOn=true;
            btnsound.GetComponent<Image>().sprite = imgsoundON;
        }
        else
        {
            soundOn = false;
            btnsound.GetComponent<Image>().sprite = imgSoundOff;
        }
    }

    public void PlayerJump(Vector3 playerPos)
    {
        if(soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerJump, playerPos);
        }
    }

    public void CoinPickup(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.coinPickup, playerPos);
        }
    }

    public void FireBullets(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.fireBullets, playerPos);
        }
    }

    public void EnemyExplosion(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyExplosion, playerPos);
        }
    }

    public void BreakableCrates(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.breakCrates, playerPos);
        }
    }

    public void WaterSplash(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.waterSplash, playerPos);
        }
    }

    public void PowerUp(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.powerUp, playerPos);
        }
    }

    public void KeyFound(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.keyFound, playerPos);
        }
    }

    public void EnemyHit(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyHit, playerPos);
        }
    }

    public void PlayerDied(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerDied, playerPos);
        }
    }

    public void MusicToggle()
    {
        if (DataCtrl.instance.data.PlayMusic)
        {
            // stop the bg music
            BGMusic.SetActive(false);

            // change the sprite to disable sprite
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;

            // save the vchange to database 
            DataCtrl.instance.data.PlayMusic = false;

        }
        else
        {
            // stop the bg music
            BGMusic.SetActive(true);

            // change the sprite to disable sprite
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;

            // save the vchange to database 
            DataCtrl.instance.data.PlayMusic = true;
        }

    }

    public void SoundToggle()
    {
        if (DataCtrl.instance.data.PlaySound)
        {
            // stop the sound
            soundOn = false;

            // change the sprite to disable sprite
            btnsound.GetComponent<Image>().sprite = imgSoundOff;

            // save the vchange to database 
            DataCtrl.instance.data.PlaySound = false;

        }
        else
        {
            // stop the sound
            soundOn = true;

            // change the sprite to disable sprite
            btnsound.GetComponent<Image>().sprite = imgsoundON;

            // save the vchange to database 
            DataCtrl.instance.data.PlaySound = true;
        }
    }
}
