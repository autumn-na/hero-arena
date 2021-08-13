using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSMoveMove : MonoBehaviour
{
    int nHorizontal = 0;
    int nVertical = 0;
    public Transform HeroTr = null;
    public Transform[] GreedTr;

    public void UpButton()
    {
        if (nVertical < 3)
        {
            nVertical++;
        }
        HeroTr.position = GreedTr[nVertical * 5 + nHorizontal].position;
    }

    public void DownButton()
    {
        if (nVertical > 0)
        {
            nVertical--;
        }
        HeroTr.position = GreedTr[nVertical * 5 + nHorizontal].position;
    }

    public void LeftButton()
    {
        if (nHorizontal > 0)
        {
            nHorizontal--;
        }
        HeroTr.position = GreedTr[nHorizontal + nVertical * 5].position;
    }

    public void RightButton()
    {
        if (nHorizontal < 4)
        {
            nHorizontal++;
        }
        HeroTr.position = GreedTr[nHorizontal + nVertical * 5].position;
    }

    public void StartPosition1()
    {
        HeroTr.position = GreedTr[0].position;
        nVertical = 0;
        nHorizontal = 0;
    }

    public void StartPosition2()
    {
        HeroTr.position = GreedTr[15].position;
        nVertical = 3;
        nHorizontal = 0;
    }
}