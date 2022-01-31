using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the coin behavior when the player interacts with it
/// </summary>
public class CoinCtrl : MonoBehaviour
{
    public enum CoinFX
    {
        Vanish,
        Fly
    }
            
    public CoinFX coinFX;                       // variable of type CoinFX enum
    public float speed;                         // speed at which the coin flies
    public bool startFlying;                    // if true, coin will fly to the HUD when collected

    GameObject coinMeter;                       // this "receives" the coin in the HUD

	private void Start()
	{
        startFlying = false;

        if(coinFX == CoinFX.Fly)
        {
            coinMeter = GameObject.Find("img_Coin_Count");
        }
	}

	void Update()
	{
        if (startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.CompareTag("Player"))
        {
            if(coinFX == CoinFX.Vanish)
                Destroy(gameObject);    // destroys the coin
            else if(coinFX == CoinFX.Fly)
            {
                gameObject.layer = 0;
                startFlying = true;
            }

        }
	}

}
