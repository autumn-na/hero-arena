using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHUnit : MonoBehaviour
{
    private int nID;

    private int nHP;
    private int nEn;

    private NMHPoint pCurPoint;

    private UnitType tUnitType;

    UnitDirection tDirection;

    Vector2 vec2Move;

    bool bIsMoving = false;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public enum UnitType
    {
        NONE,
        PLAYER,
        ENEMY
    }


    public enum UnitDirection
    {
        LEFT,
        RIGHT
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void Awake()
    {
        InitUnit();
        AddUnitToMap();
    }

    private void Update()
    {
        if (bIsMoving == true)
        {
            this.gameObject.transform.localPosition = Vector3.MoveTowards(this.gameObject.transform.localPosition, (Vector3)vec2Move, 100 * Time.deltaTime);

            if((Vector2)this.gameObject.transform.localPosition == vec2Move)
            {
                bIsMoving = false;
            }
        }
    }

    private void InitUnit()
    {
        nID = -1;

        nHP = -1;
        nEn = -1;

        pCurPoint = new NMHPoint(0, 0);

        vec2Move = new Vector2();
    }

    private void AddUnitToMap()
    {
        nID = NMHMapMng.instance.GetCurUnitNum();
        NMHMapMng.instance.AddUnit();
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void MovePointByAbs(NMHPoint _pTPoint)                      //절대적인 포인트로 이동
    {
        gameObject.transform.position = NMHMapMng.instance.GetGrid((int)_pTPoint.fX, (int)_pTPoint.fY).GetGridVec2();       //캐릭터를 그리드의 Vec2 위치로 옮김

        SetCurPoint(_pTPoint);

        NMHMapMng.instance.UpdateMapByUnitID(nID);                      //ID를 넘겨줌
    }

    public void MovePointByRel(NMHPoint _pTPoint)                      //상대적인 포인트로 이동
    {
        NMHPoint pTargetPoint = new NMHPoint(
                                                Mathf.Clamp((int)pCurPoint.fX + (int)_pTPoint.fX, 0, NMHMapMng.instance.GetMapWidth() - 1),
                                                Mathf.Clamp((int)pCurPoint.fY + (int)_pTPoint.fY, 0, NMHMapMng.instance.GetMapHeight() - 1)
                                            );

        gameObject.transform.position = NMHMapMng.instance.GetGrid(
                                                                    (int)pTargetPoint.fX,  //0부터 최대 맵 사이즈 사이에서 이동시킴
                                                                    (int)pTargetPoint.fY
                                                                    ).GetGridVec2();       //캐릭터를 그리드의 Vec2 위치로 옮김

        SetCurPoint(pTargetPoint);

        NMHMapMng.instance.UpdateMapByUnitID(nID);

        vec2Move = NMHMapMng.instance.GetGrid(
                                                                    (int)pTargetPoint.fX,
                                                                    (int)pTargetPoint.fY
                                                                    ).GetGridVec2();

        bIsMoving = true;
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter

    public int GetHP()
    {
        return nHP;
    }

    public void SetHP(int _nHP)
    {
        if(_nHP < 0)
        {
            _nHP = 0;
        }

        nHP = _nHP;
    }

    public int GetEn()
    {
        return nEn;
    }

    public void SetEn(int _nE)
    {
        if (_nE < 0)
        {
            _nE = 0;
        }

        nEn = _nE;
    }

    public NMHPoint GetCurPoint()
    {
        return pCurPoint;
    }

    public void SetCurPoint(NMHPoint _pTarget)
    {
        pCurPoint = new NMHPoint(_pTarget.fX, _pTarget.fY);
    }

    public int GetID()
    {
        return nID;
    }

    public void SetID()
    {

    }

    public UnitType GetUnitType()
    {
        return tUnitType;
    }

    public void SetUnitType(UnitType _tTargetType)
    {
        tUnitType = _tTargetType;
    }


    public UnitDirection GetUnitirection()
    {
        return tDirection;
    }

    public void SetUnitDeirection(UnitDirection _tTDirection)
    {
        tDirection = _tTDirection;

        if (_tTDirection == UnitDirection.LEFT)
        {
            gameObject.transform.position = NMHMapMng.instance.GetGrid(GetCurPoint()).GetGridVec2() + new Vector2(50, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_tTDirection == UnitDirection.RIGHT)
        {
            gameObject.transform.position = NMHMapMng.instance.GetGrid(GetCurPoint()).GetGridVec2() + new Vector2(-50, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public Vector2 GetVec2Move()
    {
        return vec2Move;
    }

    public void SetVec2Move(Vector2 _vec2TargetMove)
    {
        vec2Move = _vec2TargetMove;
    }
}
