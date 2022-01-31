using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsToggle : MonoBehaviour
{
    public RectTransform BtnFB, BtnT, BtnG, BtnR;
    public float moveFb, moveT, moveG, moveR;
    public float DefaultX, DefaultY;
    public float speed;

    bool expanded;
    void Start()
    {
        expanded = false;
    }

    
    public void toggle()
    {
        if (!expanded)
        {
            BtnFB.DOAnchorPosY(moveFb, speed, false);
            BtnT.DOAnchorPosY(moveT, speed, false);
            BtnG.DOAnchorPosY(moveG, speed, false);
            BtnR.DOAnchorPosY(moveR, speed, false);

            expanded = true;
        }
        else
        {
            BtnFB.DOAnchorPosY(DefaultY, speed, false);
            BtnT.DOAnchorPosY(DefaultY, speed, false);
            BtnG.DOAnchorPosY(DefaultY, speed, false);
            BtnR.DOAnchorPosY(DefaultY, speed, false);
            expanded = false;

        }

    }
}
