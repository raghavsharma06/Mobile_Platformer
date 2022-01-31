using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Makes the camera follow the player
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    //1. declare a variable of type Transform and call it player
    //2. in the Update(), make changes to the transform.position via a new Vector3

    public Transform player;
    public float yOffset;


    void Start()
    {

    }


    void Update()
    {
        // makes the camera follow the player in the x axis
        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // makes the camera follow the player in the x and y axis
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
    }
}
