using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NMHGrid
{
    private Vector2 vec2Grid;

    private GridType tGridType;
    private NMHUnit.UnitType nUnitType;

    public enum GridType
    {
        NONE,
        CAUTION,
        PROHIBITION,
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 일반 public 함수

    public NMHGrid(Vector2 _vec2Target, GridType _nT, NMHUnit.UnitType _nTargetUnit)
    {
        vec2Grid = new Vector2();
        SetGridVec2(_vec2Target);

        SetGridType(_nT);
        SetUnitType(_nTargetUnit);
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter

    public Vector2 GetGridVec2()
    {
        return vec2Grid;
    }

    public void SetGridVec2(Vector2 _vec2Target)
    {
        vec2Grid = _vec2Target;
    }

    public GridType GetGridType()
    {
        return tGridType;
    }

    public void SetGridType(GridType _nT)
    {
        tGridType = _nT;
    }

    public NMHUnit.UnitType GetUnitType()
    {
        return nUnitType;
    }

    public void SetUnitType(NMHUnit.UnitType _nT)
    {
        nUnitType = _nT;
    }
}
