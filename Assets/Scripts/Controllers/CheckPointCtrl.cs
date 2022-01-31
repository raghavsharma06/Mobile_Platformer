using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPrefs.GetFloat("CPX", other.gameObject.transform.position.x);
        PlayerPrefs.GetFloat("CPY", other.gameObject.transform.position.y);
    }
}
