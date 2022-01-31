using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonCtrl : MonoBehaviour
{
    public void savedata()
    {
        DataCtrl.instance.SaveData();
    }
    
}
