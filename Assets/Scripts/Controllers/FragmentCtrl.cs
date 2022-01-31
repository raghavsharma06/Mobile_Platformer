using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages the behavior of the box fragments
/// </summary>
public class FragmentCtrl : MonoBehaviour
{
    public Vector3 jumpForce;
    public float destroyDelay;

    Rigidbody2D rb;

	void Start()
	{
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(jumpForce);

        Destroy(gameObject, destroyDelay);
	}


}
