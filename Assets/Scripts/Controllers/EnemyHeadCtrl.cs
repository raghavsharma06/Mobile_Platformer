using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the enemy behavior when the player jumps on the enemy's head
/// </summary>
public class EnemyHeadCtrl : MonoBehaviour
{
    public GameObject enemy;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);

            AudioCtrl.instance.EnemyHit(enemy.transform.position);

            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);

        }
	}
}
