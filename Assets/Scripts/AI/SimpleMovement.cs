using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the enemy move in a straight line
/// Also checks for collisions and reverses enemy movement direction
/// </summary>
public class SimpleMovement : MonoBehaviour
{
    public float speed;                 // speed at which the enemy moves

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        SetStartingDirection();
    }

    void Update()
    {
        Move();
    }

	void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void SetStartingDirection()
    {
        if(speed > 0)
        {
            sr.flipX = true;
        }

        else if (speed < 0)
        {
            sr.flipX = false;
        }
    }

    void FlipOnCollision()
    {
        speed = -speed;
        SetStartingDirection();
    }

	void OnCollisionEnter2D(Collision2D other)
	{
        if(!other.gameObject.CompareTag("Player"))
        {
            FlipOnCollision();
        }
	}
}
