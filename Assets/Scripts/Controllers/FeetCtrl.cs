using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// helps in showing the dust particle effects when the player lands
/// </summary>
public class FeetCtrl : MonoBehaviour
{
    public GameObject player;
    public Transform dustParticlePos;

    PlayerCtrl playerCtrl;

	private void Start()
	{
        playerCtrl = gameObject.transform.parent.gameObject.GetComponent<PlayerCtrl>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.CompareTag("Ground"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);

            playerCtrl.isJumping = false;
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);

            playerCtrl.isJumping = false;

            player.transform.parent = other.gameObject.transform;
        }
	}

	void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
	}

}
