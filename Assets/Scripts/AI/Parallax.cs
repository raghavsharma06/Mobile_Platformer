using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides the Parallax Effect
/// </summary>
public class Parallax : MonoBehaviour
{
    public GameObject player;       // to get access to the PlayerCtrl script
    public float speed;             // speed at which parallax BG moves. set this to 0.001

    float offSetX;                  // this is the secret to horizontal parallax
    Material mat;                   // the material attached to the renderer of the Quad
    PlayerCtrl playerCtrl;          // this script has access to the isStuck variable


    void Start()
    {
        mat = GetComponent<Renderer>().material;
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }


    void Update()
    {
        if(!playerCtrl.isStuck)
        {
            offSetX += Input.GetAxisRaw("Horizontal") * speed;

            // handles the mobile parallax
            if (playerCtrl.leftPressed)
                offSetX += -speed;
            else if(playerCtrl.rightPressed)
                offSetX += speed;

            mat.SetTextureOffset("_MainTex", new Vector2(offSetX, 0));
        }
    }
}
