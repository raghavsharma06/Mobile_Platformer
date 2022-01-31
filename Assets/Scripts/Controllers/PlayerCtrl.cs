using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages:
/// 1. the player movement and flipping
/// 2. the player animation
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    //1. declare public bool isGrounded, Transform feet, float feetRadius, layerMask whatIsGround
    //2. show Physics2D.OverlapCircle() method to check if player is grounded
    //3. then show preferred way for this cat by using Physics2D.OverlapBox

    [Tooltip("this is a positive integer which speed up the player movement")]
    public int speedBoost;  // set this to 5
    public float jumpSpeed; // set this to 600
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public LayerMask whatIsGround;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool sfxOn;
    public bool canFire;
    public bool isJumping;
    public bool isStuck;
    public bool leftPressed, rightPressed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    bool canDoubleJump;

     void Awake()
    {
        if (PlayerPrefs.HasKey("CPX"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("CPX"), PlayerPrefs.GetFloat("CPY"), transform.position.z);
        }
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGround);

        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x,feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

        float playerSpeed = Input.GetAxisRaw("Horizontal"); // value will be 1, -1 or 0
        playerSpeed *= speedBoost;

        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullets();
        }

        ShowFalling();

        if (leftPressed)
            MoveHorizontal(-speedBoost);

        if (rightPressed)
            MoveHorizontal(speedBoost);
        if (transform.position.y <= -3.2)
        {
            DisableCameraFollow();
        }
    }

	void OnDrawGizmos()
	{
        //Gizmos.DrawWireSphere(feet.position, feetRadius);

        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
	}

	void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if (playerSpeed < 0)
            sr.flipX = true;
        else if(playerSpeed > 0)
            sr.flipX = false;

        if(!isJumping)
            anim.SetInteger("State", 1);
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if(!isJumping)
            anim.SetInteger("State", 0);
    }

    void ShowFalling()
    {
        if(rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if(isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
            anim.SetInteger("State", 2);

            // play the jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if(canDoubleJump && !isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
            anim.SetInteger("State", 2);

            // play the jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            canDoubleJump = false;
        }
    }

    void FireBullets()
    {
        if(canFire)
        {
            // makes the player fire bullets in the left direction
            if (sr.flipX)
            {
                Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
            }

            // makes the player fire bullets in the right direction
            if (!sr.flipX)
            {
                Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
            }

            AudioCtrl.instance.FireBullets(gameObject.transform.position);
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    public void MobileMoveLeft()
    {
        leftPressed = true;
    }

    public void MobileMoveRight()
    {
        rightPressed = true;
    }

    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;

        StopMoving();
    }

    public void MobileFireBullets()
    {
        FireBullets();
    }

    public void MobileJump()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);

            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
        }

        if (other.gameObject.CompareTag("BigCoin"))
        {
            GameCtrl.instance.UpdateCoinCount();

            SFXCtrl.instance.ShowBulletSparkle(other.gameObject.transform.position);

            Destroy(other.gameObject);

            GameCtrl.instance.UpdateScore(GameCtrl.Item.BigCoin);

            AudioCtrl.instance.CoinPickup(gameObject.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        switch (other.gameObject.tag)
        {
            case "Coin":
                if(sfxOn)
                {
                    SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
                }
                GameCtrl.instance.UpdateCoinCount();
                GameCtrl.instance.UpdateScore(GameCtrl.Item.Coin);
                AudioCtrl.instance.CoinPickup(gameObject.transform.position);
                break;
            case "Water":
                // show the splash effect
                SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);

                AudioCtrl.instance.WaterSplash(gameObject.transform.position);

                break;
            case "Powerup_Bullet":
                canFire = true;
                Vector3 powerupPos = other.gameObject.transform.position;
                AudioCtrl.instance.PowerUp(gameObject.transform.position);
                Destroy(other.gameObject);
                if (sfxOn)
                    SFXCtrl.instance.ShowBulletSparkle(powerupPos);
                break;
            case "Enemy":
                GameCtrl.instance.PlayerDiedAnimation(gameObject);
                AudioCtrl.instance.PlayerDied(gameObject.transform.position);
                break;
            case "BossKey":
                GameCtrl.instance.ShowLever();
                break;
            default:
                break;
        }		
	}

    void DisableCameraFollow()
    {
        Camera.main.GetComponent<CameraCtrl>().enabled = false;
    }
}
