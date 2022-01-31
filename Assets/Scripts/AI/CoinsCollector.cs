using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script "receives" the coins which fly towards it when the player picks them
/// </summary>
public class CoinsCollector : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
	}

}
