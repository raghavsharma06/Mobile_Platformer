using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// to get the updated data on this scene as well
/// </summary>
public class refreshData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataCtrl.instance.RefreshData();
    }

    
}
