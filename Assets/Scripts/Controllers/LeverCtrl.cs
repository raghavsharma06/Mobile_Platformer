using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Releases the Dog from its cage
/// </summary>
public class LeverCtrl : MonoBehaviour
{
    public GameObject dog;              // the gameobject dog
    public Vector2 jumpSpeed;           // speed at which the dog jumps out of the cage

    public GameObject[] stairs;         // stairs through which the dog jumps
    public Sprite leverPulled;          // the image of the pulled lever

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = dog.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(jumpSpeed);

            foreach (GameObject stair in stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }

            SFXCtrl.instance.ShowPlayerLanding(gameObject.transform.position);
            sr.sprite = leverPulled;

            Invoke("ShowLevelCompleteMenu", 3f);
        }
    }

    void ShowLevelCompleteMenu()
    {
        GameCtrl.instance.LevelComplete();
    }
}
