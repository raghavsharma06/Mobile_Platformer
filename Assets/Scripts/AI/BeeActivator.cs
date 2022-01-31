using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activates the Bomber Bee when the player comes near it
/// </summary>
public class BeeActivator : MonoBehaviour
{
    public GameObject bee;                  // public reference to the be bomber bee

    BomberBeeAI bbai;

    void Start()
    {
        bbai = bee.GetComponent<BomberBeeAI>();
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
        {
            bbai.ActivateBee(other.gameObject.transform.position);
        }
	}
}
