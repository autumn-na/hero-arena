using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class NMHCard
{
    int nID;

    int nAD;
    int nTP;
    int nEn;

    LJJHeroDataMng.HeroJob tHeroJob; //카드의 직업

    string strKey; //카드의 키(무슨 행동?)
    string strNotice; //카드의 알림

    MoveType tMoveType;     //enum
    AttackType tAttackType;

    NMHPoint pMovePoint;    //움직일 포인트
    NMHPoint[] pAttackPointArr;

    public enum MoveType
    {
        RELATIVE,
        ABSOLUTE
    }

    public enum AttackType
    {
        RELATIVE,
        ABSOLUTE
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 일반 public 함수

    public NMHCard()
    {
        nID = -99;
        nAD = -99;
        nTP = -99;
        nEn = -99;
    }

    public NMHCard(int _nTargetAD, int _nTargetTP, int _nTargetEn)
    {
        nAD = _nTargetAD;
        nTP = _nTargetTP;
        nEn = _nTargetEn;
    }

    public NMHCard GetCard()
    {
        return this;
    }

    public void SetCard(int _nTargetAD, int _nTargetTP, int _nTargetEn)
    {
        nAD = _nTargetAD;
        nTP = _nTargetTP;
        nEn = _nTargetEn;
    }

    public void SetCard(int _nTargetID, int _nTargetAD, int _nTargetTP, int _nTargetEn, string _strTMoveType, NMHPoint _pTargetMovePoint, string _strTAttackType, NMHPoint[] _pTAttackPointArr)
    {
        nID = _nTargetID;
        nAD = _nTargetAD;
        nTP = _nTargetTP;
        nEn = _nTargetEn;

        if (_strTMoveType == "RELATIVE")
        {
            tMoveType = MoveType.RELATIVE;
        }
        else if (_strTMoveType == "ABSOLUTE")
        {
            tMoveType = MoveType.ABSOLUTE;
        }

        pMovePoint = new NMHPoint(_pTargetMovePoint.fX, _pTargetMovePoint.fY);

        if (_strTAttackType == "RELATIVE")
        {
            tAttackType = AttackType.RELATIVE;
        }
        else if (_strTAttackType == "ABSOLUTE")
        {
            tAttackType = AttackType.ABSOLUTE;
        }

        int nAttackPointArrLen = _pTAttackPointArr.Length;

        pAttackPointArr = new NMHPoint[nAttackPointArrLen];
        pAttackPointArr = _pTAttackPointArr;
    }

    public void SetCard(NMHCard _cTargetCard)                       //수정 필요.. 왜 어택타입이 안들어오지???
    {
        nID = _cTargetCard.nID;
        nAD = _cTargetCard.nAD;
        nTP = _cTargetCard.nTP;
        nEn = _cTargetCard.nEn;
        tMoveType = _cTargetCard.tMoveType;
        pMovePoint = _cTargetCard.pMovePoint;
        tAttackType = _cTargetCard.tAttackType;             

        pAttackPointArr = _cTargetCard.pAttackPointArr;

        strKey = _cTargetCard.strKey;
        strNotice = _cTargetCard.strNotice;

        tHeroJob = _cTargetCard.tHeroJob;
    }

    public void SetCard(int _nID)                                       //카드 ID에 따라 XML에서 파일 불러와서 저장 (수정 필요)
    {
        this.SetCard(NMHCardInfoMng.instance.GetCardInfoByID(_nID));
    }

    public int GetID()
    {
        return nID;
    }

    public void SetID(int _nTargetID)
    {
        nID = _nTargetID;
    }

    public int GetAD()
    {
        return nAD;
    }

    public void SetAD(int _nTargetAD)
    {
        nAD = _nTargetAD;
    }

    public int GetTP()
    {
        return nTP;
    }

    public void SetTP(int _nTargetTP)
    {
        nTP = _nTargetTP;
    }

    public int GetEn()
    {
        return nEn;
    }

    public void SetEn(int _nTargetEn)
    {
        nEn = _nTargetEn;
    }

    public MoveType GetMoveType()
    {
        return tMoveType;
    }

    public void SetMoveType(MoveType _tTMoveType)
    {
        tMoveType = _tTMoveType;
    }

    public NMHPoint GetMovePoint()
    {
          return pMovePoint;
    }

    public void SetMovePoint(NMHPoint _nTPoint)
    {
        pMovePoint = _nTPoint;
    }

    public AttackType GetAttackType()
    {
        return tAttackType;
    }

    public void SetAttackType(AttackType _tAttackType)
    {
        tAttackType = _tAttackType;
    }

    public NMHPoint[] GetAttackPointArr()
    {
        return pAttackPointArr;
    }

    public void SetAttackPointArr(NMHPoint[] _nTPointArr)
    {
        pAttackPointArr = _nTPointArr;
    }

    public LJJHeroDataMng.HeroJob GetCardJob()
    {
        return tHeroJob; //카드의 직업
    }

    public void SetCardJob(LJJHeroDataMng.HeroJob _tTargetJob)
    {
        tHeroJob = _tTargetJob; //카드의 직업
    }

    public string GetCardKey()
    {
        return strKey; //카드의 키(무슨 행동?)
    }

    public void SetCardKey(string _strTarget)
    {
        strKey = _strTarget;
    }

    public string GetCardNotice()
    {
        return strNotice;
    }

    public void SetCardNotice(string _strNotice)
    {
        strNotice = _strNotice;
    }


}
