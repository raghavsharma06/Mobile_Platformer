using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the platform's dropping behavior
/// </summary>
public class SamplePlatform : MonoBehaviour 
{
	public float droppingDelay;

	Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("StartDropping", droppingDelay);
        }
	}

	void StartDropping()
	{
		rb.isKinematic = false;
        gameObject.GetComponent<DestroyWithDelay>().enabled = true;
	}

}
