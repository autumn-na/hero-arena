using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NMHPoint
{
    public float fX;
    public float fY;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////

    public NMHPoint(float _x, float _y)
    {
        fX = _x;
        fY = _y;
    }

    public static NMHPoint operator+(NMHPoint _p1, NMHPoint _p2)
    {
        float fNewX = _p1.fX + _p2.fX;
        float fNewY = _p1.fY + _p2.fY;

        return new NMHPoint(fNewX, fNewY);
    }

    public static NMHPoint operator -(NMHPoint _p1, NMHPoint _p2)
    {
        float fNewX = _p1.fX - _p2.fX;
        float fNewY = _p1.fY - _p2.fY;

        return new NMHPoint(fNewX, fNewY);
    }

    public static bool operator ==(NMHPoint _p1, NMHPoint _p2)
    {
        if ((_p1.fX == _p2.fX) && (_p1.fY == _p2.fY))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator !=(NMHPoint _p1, NMHPoint _p2)
    {
        if ((_p1.fX == _p2.fX) && (_p1.fY == _p2.fY))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
