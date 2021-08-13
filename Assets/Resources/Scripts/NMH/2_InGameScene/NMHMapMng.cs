using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHMapMng : MonoBehaviour
{
    private NMHGrid[,] MapGridArr;

    private int nNormalMapWidth = 5;
    private int nNormalMapHeight = 4;

    private int nCurUnitNum;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public static NMHMapMng instance;

    public GameObject ObjUnitParent;

    public GameObject ObjGridParent;                                    //임시 그리드 부모
    public GameObject[] ObjGridPrefab;                                //임시 그리드 변경 알림

    public enum MapType
    {
        TEST,
        NORMAL
    }

    public enum MapLogType
    {
        ALL,
        HEROS,
        GRIDS
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            CreateMap();
            InitUnitNum();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

    }

    private void CreateMap()
    {
        MapGridArr = new NMHGrid[nNormalMapWidth, nNormalMapHeight];

        for(int i = 0; i < nNormalMapWidth; i ++)
        {
            for(int j = 0; j < nNormalMapHeight; j ++)
            {
                MapGridArr[i,j] = new NMHGrid(new Vector2(-505 + 261 * i, -290 + 136 * j), (int)NMHGrid.GridType.NONE, (int)NMHUnit.UnitType.NONE);          //맵 그리드 배열을 생성하고 초기화
            }
        }
    }

    private void InitUnitNum()
    {
        nCurUnitNum = 0;
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void LogMap(MapLogType _targetType)
    {
        NMHPoint pPlayer = new NMHPoint(0, 0);
        NMHPoint pEnemy = new NMHPoint(0, 0);

        if(_targetType == MapLogType.ALL || _targetType == MapLogType.HEROS)                    // 플레이어, 적의 영웅 위치 로그
        {
            for(int i = 0; i < nNormalMapWidth; i ++)
            {
                for(int j = 0; j < nNormalMapHeight; j ++)
                {
                    if(MapGridArr[i, j].GetUnitType() == NMHUnit.UnitType.PLAYER)
                    {
                        pPlayer = new NMHPoint(i, j);
                    }
                    else if(MapGridArr[i, j].GetUnitType() == NMHUnit.UnitType.ENEMY)
                    {
                        pEnemy = new NMHPoint(i, j);
                    }
                }
            }

            Debug.Log(
                        "PLAYER is on " + pPlayer.fX + ", " + pPlayer.fY + "\n" + 
                        "ENEMY is on " + pEnemy.fX + ", " + pEnemy.fY
                    );
        }

        if (_targetType == MapLogType.ALL || _targetType == MapLogType.GRIDS)                    // 그리드 정보 로그
        {
            for (int i = 0; i < nNormalMapWidth; i++)
            {
                for (int j = 0; j < nNormalMapHeight; j++)
                {
                    Debug.Log(
                        "Grid : " + i.ToString() + ", " + j.ToString() + " / " +
                        "Vec2 : " + GetGrid(i, j).GetGridVec2() + "\n" +
                        "GT : " + GetGrid(i, j).GetGridType() + " / " +
                        "UT : " + GetGrid(i, j).GetUnitType()
                            );
                }
            }
        }
    }

    public void UpdateMapByUnitID(int _nTargetID)                         //히어로의 정보 가져와서 Map의 point 변수에 저장 -> 수정 필요
    {
        for(int i = 0; i < GetMapWidth(); i ++)
        {
            for(int j = 0; j < GetMapHeight(); j ++)
            {
                if(MapGridArr[i, j].GetUnitType() == GetUnitByUnitID(_nTargetID).GetUnitType())
                {
                    MapGridArr[i, j].SetUnitType(NMHUnit.UnitType.NONE);
                }
            }
        }

        MapGridArr[(int)GetUnitByUnitID(_nTargetID).GetCurPoint().fX, (int)GetUnitByUnitID(_nTargetID).GetCurPoint().fY].SetUnitType(GetUnitByUnitID(_nTargetID).GetUnitType());

       LogMap(MapLogType.HEROS);
    }

    public void AddUnit()                               //맵에 유닛이 생성됨을 전달, 유닛에 ID 제공
    {
        SetCurUnitNum(GetCurUnitNum() + 1);
    }

    public NMHUnit GetUnitByUnitID(int _nTargetID)
    {
        NMHUnit[] uUnitArr = new NMHUnit[nCurUnitNum];

        uUnitArr = ObjUnitParent.GetComponentsInChildren<NMHUnit>();

        for (int i = 0; i < 50; i ++)
        {
            if(uUnitArr[i].GetID() == _nTargetID)
            {
                return uUnitArr[i];
            }
        }

        Debug.Log("No Unit : ID " + _nTargetID.ToString());

        return null;
    }

    public NMHUnit GetUnitByUnitType(NMHUnit.UnitType _tTargetType)
    {
        NMHUnit[] uUnitArr = new NMHUnit[nCurUnitNum];

        uUnitArr = ObjUnitParent.GetComponentsInChildren<NMHUnit>();

        for (int i = 0; i < 50; i++)
        {
            if (uUnitArr[i].GetUnitType() == _tTargetType)
            {
                return uUnitArr[i];
            }
        }

        Debug.Log("No Unit : ID " + _tTargetType.ToString());

        return null;
    }

    /// <summary>
    /// 모든 A 타입의 그리드를 B 타입의 그리드로 바꾼다.
    /// </summary>
    /// <param name="_tAType"></param>
    /// <param name="_tBType"></param>
    public void SetGridAToB(NMHGrid.GridType _tAType, NMHGrid.GridType _tBType)
    {
        bool bIsAvailable = false;                              //A타이입의 그리드가 존재하는가?

        for (int i = 0; i < nNormalMapWidth; i++)
        {
            for (int j = 0; j < nNormalMapHeight; j++)
            {
                if (GetGrid(i, j).GetGridType() == _tAType)
                {
                    bIsAvailable = true;
                    break;
                }
            }
        }

        if (bIsAvailable == false)                                // 만약 A타입 그리드가 존재하지 않다면
        {
            return;                                              //함수 종료
        }   
        else if (bIsAvailable == true)                          //A타입 그리드가 존재하는 경우
        {
            for (int i = 0; i < GetMapWidth(); i++)             //전체 맵을 돌아가며 A타입 그리드를 B 타입 그리드로 변경
            {
                for (int j = 0; j < GetMapHeight(); j++)
                {
                    if (GetGrid(i, j).GetGridType() == _tAType)
                    {
                        GetGrid(i, j).SetGridType(_tBType);

                        if (_tBType != NMHGrid.GridType.NONE)    //빈 그리드가 아니면
                        {
                            GameObject objClone = Instantiate(ObjGridPrefab[(int)_tBType], ObjGridParent.transform);           //임시 그리드 변경 알림    (수정 필요)
                            objClone.GetComponent<RectTransform>().localPosition = new Vector3(GetGrid(i, j).GetGridVec2().x, GetGrid(i, j).GetGridVec2().y, 0);
                        }
                    }
                }
            }
        }

       // LogMap(MapLogType.GRIDS);
    }

    /// <summary>
    /// bool값을 true를 넘길 경우 랜덤한 A 타입의 그리드를 B 타입의 그리드로 바꾼다.
    /// false를 넘겨주는 경우는 위 코드와 결과가 일치하고 전체 그리드를 바꿔 줌.
    /// </summary>
    /// <param name="_tAType"></param>
    /// <param name="_tBType"></param>
    public void SetGridAToB(NMHGrid.GridType _tAType, NMHGrid.GridType _tBType, bool _bIsRandom)
    {
        bool bIsAvailable = false;                              //A타이입의 그리드가 존재하는가?

        for(int i = 0; i < nNormalMapWidth; i++)
        {
            for(int j = 0; j < nNormalMapHeight; j ++)
            {
                if(GetGrid(i, j).GetGridType() == _tAType)
                {
                    bIsAvailable = true;
                    break;
                }
            }
        }

        if(bIsAvailable == false)                                // 만약 A타입 그리드가 존재하지 않다면
        {
            return;                                              //함수 종료
        }
        else if(bIsAvailable == true && _bIsRandom == true)                              //A타입 그리드가 존재하는 경우
        {
            for(;;)                                              //무한루프를 돌다가 A타입 그리드를 만나면 탈출
            {
                NMHPoint pRandomPoint;                                                                              //경고 그리드로 바꿀 그리드의 위치 포인트
                pRandomPoint = new NMHPoint(Random.Range(0, nNormalMapWidth), Random.Range(0, nNormalMapHeight));   //0 ~ 맵의 넓이, 0 ~ 맵의 높이 중 한 포인트를 랜덤하게 정함

                if(GetGrid(pRandomPoint).GetGridType() == _tAType)                                                      //랜덤하게 돌린 위치의 그리드가 A 타입이면
                {
                    GetGrid(pRandomPoint).SetGridType(_tBType);                                                         //B 타입 그리드로 변경

                    GameObject objClone = Instantiate(ObjGridPrefab[(int)_tBType], ObjGridParent.transform);           //임시 그리드 변경 알림    (수정 필요)
                    objClone.GetComponent<RectTransform>().localPosition = new Vector3(GetGrid(pRandomPoint).GetGridVec2().x, GetGrid(pRandomPoint).GetGridVec2().y, 0);

                    break;                                                                                              //무한 루프 탈출
                }
            }
        }
        else if(bIsAvailable == true && _bIsRandom == false)    //랜덤이 아닌, 전체를 바꾸는 경우
        {
            for (int i = 0; i < GetMapWidth(); i++)             //전체 맵을 돌아가며 A타입 그리드를 B 타입 그리드로 변경
            {
                for (int j = 0; j < GetMapHeight(); j++)
                {
                    if (GetGrid(i, j).GetGridType() == _tAType)
                    {
                        GetGrid(i, j).SetGridType(_tBType);

                        GameObject objClone = Instantiate(ObjGridPrefab[(int)_tBType], ObjGridParent.transform);           //임시 그리드 변경 알림    (수정 필요)
                        objClone.GetComponent<RectTransform>().localPosition = new Vector3(GetGrid(i, j).GetGridVec2().x, GetGrid(i, j).GetGridVec2().y, 0);
                    }
                }
            }
        }

       // LogMap(MapLogType.GRIDS);
    }

    public void DestroyGridNotifications()                              //그리드 알림 삭제
    {
        foreach(Transform transChild in ObjGridParent.transform)
        {
            GameObject.Destroy(transChild.gameObject);
        }
    }

    public void DestroyGridNotifications(NMHGrid.GridType _tTType)                              //그리드 알림 삭제
    {
        foreach (Transform transChild in ObjGridParent.transform)
        {
            switch(transChild.name)
            {
                case "grid_caution (clone)":
                    if(_tTType == NMHGrid.GridType.CAUTION)
                    {
                        GameObject.Destroy(transChild.gameObject);
                    }
                    break;
                case "grid_prohibition (clone)":
                    if (_tTType == NMHGrid.GridType.PROHIBITION)
                    {
                        GameObject.Destroy(transChild.gameObject);
                        Debug.Log(transChild.name);
                    }
                    break;

            }
            GameObject.Destroy(transChild.gameObject);
        }
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter

    public int GetMapWidth()
    {
        return nNormalMapWidth;
    }

    public int GetMapHeight()
    {
        return nNormalMapHeight;
    }

    public NMHGrid GetGrid(int _nTargetX, int _nTargetY)
    {
        _nTargetX = (int)Mathf.Clamp(_nTargetX, 0, GetMapWidth() - 1);
        _nTargetY = (int)Mathf.Clamp(_nTargetY, 0, GetMapHeight() - 1);

        return MapGridArr[_nTargetX, _nTargetY];
    }

    public NMHGrid GetGrid(NMHPoint _pT)
    {
        _pT.fX = (int)Mathf.Clamp(_pT.fX, 0, GetMapWidth() - 1);
        _pT.fY = (int)Mathf.Clamp(_pT.fY, 0, GetMapHeight() - 1);

        return MapGridArr[(int)_pT.fX, (int)_pT.fY];
    }

    public void SetGrid()
    {

    }

    public int GetCurUnitNum()
    {
        return nCurUnitNum;
    }

    public void SetCurUnitNum(int _nTarget)
    {
        nCurUnitNum = _nTarget;
    }
}
