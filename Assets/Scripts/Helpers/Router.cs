using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
  public void ShowPausePanel()
    {
        GameCtrl.instance.ShowPauseMenu();
    }

    public void HidePausePanel()
    {
        GameCtrl.instance.HidePauseMenu();
    }

    public void MusicToggle()
    {
        AudioCtrl.instance.MusicToggle();
    }

    public void SoundToggle()
    {
        AudioCtrl.instance.SoundToggle();
    }
}
