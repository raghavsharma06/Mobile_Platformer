using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// destroys the gameobject after a specified delay
/// </summary>
public class DestroyWithDelay : MonoBehaviour
{
    public float delay;

	void Start()
	{
        Destroy(gameObject, delay);	
	}

}
