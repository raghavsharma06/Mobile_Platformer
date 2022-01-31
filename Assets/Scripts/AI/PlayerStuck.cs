using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks when the player is stuck
/// </summary>
public class PlayerStuck : MonoBehaviour
{
    public GameObject player;       // to get access to the PlayerCtl script

    PlayerCtrl playerCtrl;          // to reference the PlayerCtrl script

    void Start()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        playerCtrl.isStuck = true;
	}
    void OnTriggerStay2D(Collider2D other)
    {
        playerCtrl.isStuck = true;
    }

    void OnTriggerExit2D(Collider2D other)
	{
        playerCtrl.isStuck = false;
	}
}
