using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHGameMng : MonoBehaviour
{
    public static NMHGameMng instance;

    public NMHInGameUICtrl InGameUICtrl;
    public NMHPopUpSelectCardCtrl PopUpUICtrl;
    public NMHPopUpFinishGameCtrl FinishGameUICtrl;

    public LJJCtrlCamara CtrlCamera;

    public GameObject objHero;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 변수

    NMHHero hPlayer;
    NMHHero hEnemy;

    int nCurRound;
    int nCurPhase;
    int nCurTurn;

    int nCardSelectingTime = 180;                                                     //카드 선택 시간, 초기화

    bool bIsFinishedCardSelecting = false;                                              //카드 선택이 완료되었는가?, 초기화
    bool bIsDead = false;                                                                //누군가가 죽었는가?, 초기화

    float fPlayTIme = 0f;                                                              //이번 게임 플레이 타임, 초기화

    bool bIEBehaviorLive = false;       //각 턴에 행동을 하는 코루틴이 살아있는가?
    bool bIEPhase = false; //페이즈 중인가?

    int[] nMoveCardCnt;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitGame();
    }

    private void Update()
    {
        DebugKillEnemy();
        CheckDead();
        CheckPlayTime();
    }

    private void InitGame()
    {
        NMHSpriteEffectMng.instance.CreateEffect("UIEffects/spr_ef_vs");

        nMoveCardCnt = new int[3];

        CreateHero();

        hPlayer = GetHeroByType(NMHUnit.UnitType.PLAYER);
        hEnemy = GetHeroByType(NMHUnit.UnitType.ENEMY);

        hPlayer.SetHP(150);
        hEnemy.SetHP(150); //HP 초기화 -> 나중에 직업 코드를 통해서 초기화 할거임

        hPlayer.SetEn(140);
        hEnemy.SetEn(140); //En 초기화 -> 나중에 직업 코드를 통해 초기화 시킴

        hEnemy.gameObject.AddComponent<NMHAI>();        //적에게 AI 추가

        for(int i = 0; i < 3; i ++)
        {
            hPlayer.SetHeroSelCard(i, new NMHCard(0, 0, 0));         //내 카드를 AD 0, TP 0, En 0 으로 초기화
            hEnemy.SetHeroSelCard(i, new NMHCard(0, 0, 0));             //상대 카드를 AD 0, TP 0으로 초기화
        }

        nCurRound = 1;
        nCurPhase = 0;
        nCurTurn = 0;

        hPlayer.MovePointByAbs(new NMHPoint(0, 2));                 //임시 캐릭터 이동
        hEnemy.MovePointByAbs(new NMHPoint(4, 2));                  //임시 적 이동

        CheckHeroDirection();           //히어로 방향 초기화

        InGameUICtrl.SetHPBar(hPlayer);                     //HP 바 초기화 (수정 필요)
        InGameUICtrl.SetHPBar(hEnemy);

        InGameUICtrl.SetEnBar(hPlayer);                     //기력 바 초기화 (수정 필요)
        InGameUICtrl.SetEnBar(hEnemy);

        StartCoroutine(StartRound(GetCurRound())); //라운드 시작 (게임 시작)
    }

    private void DeselectCard()
    {

    }

    private void CheckPlayTime()                //플레이타임을 체크하는 함수
    {   
        if (bIsDead == false)                   //누군가 죽지 않았으면
        {
            fPlayTIme += Time.deltaTime;        //플레이타임 체크                
        }
    }

    private void CheckHeroDirection()
    {
        if (hPlayer.GetCurPoint().fX >= hEnemy.GetCurPoint().fX)                         //플레이어가 더 오른쪽에 있을때
        {
            hPlayer.SetUnitDeirection(NMHUnit.UnitDirection.LEFT);
            hEnemy.SetUnitDeirection(NMHUnit.UnitDirection.RIGHT);
        }
        else                                                                          //플레이어가 왼쪽에 있을 때
        {
            hPlayer.SetUnitDeirection(NMHUnit.UnitDirection.RIGHT);
            hEnemy.SetUnitDeirection(NMHUnit.UnitDirection.LEFT);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 디버그용 private 함수

    void DebugKillEnemy()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            hEnemy.SetHP(0);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 일반 public 함수

    public void CreateHero()
    {
        GameObject objClonePlayer =  Instantiate(objHero, NMHMapMng.instance.ObjUnitParent.transform);
        objClonePlayer.GetComponent<NMHHero>().SetUnitType(NMHUnit.UnitType.PLAYER);
        objClonePlayer.GetComponent<NMHHero>().objBody.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/2_IngameScene/NMH/Units/player_temp");            //임시로 스프라이트 넣어줌 -> 수정 필요!
        objClonePlayer.name = "Player";

        GameObject objCloneEnemy = Instantiate(objHero, NMHMapMng.instance.ObjUnitParent.transform);
        objCloneEnemy.GetComponent<NMHHero>().SetUnitType(NMHUnit.UnitType.ENEMY);
        objCloneEnemy.GetComponent<NMHHero>().objBody.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/2_IngameScene/NMH/Units/enemy_temp");              //임시로 스프라이트 넣어줌 -> 수정 필요!
        objCloneEnemy.name = "Enemy";
    }

    public IEnumerator StartRound(int _nRoundNum)     //라운드 시작시 호출      라운드1 시작 -> 카드 선택 시작 -> 카드 선택 끝 -> 페이즈1 시작 -> 턴1 시작 -> 턴1 행동 -> 턴2 시작 -> 턴2 행동 -> 페이즈 2 시작 -> ... 페이즈 3 시작 -> ... 라운드 1 끝           
    {
        yield return new WaitForSeconds(3.0f);

        NMHSpriteEffectMng.instance.CreateEffect("spr_ef_round_start", new Vector2(0, 0));          //라운드 시작 애니메이션

        SetCurRound(_nRoundNum);

        InGameUICtrl.SetRoundText();            //인게임 UI의 현재 라운드를 표시하는 텍스트를 현재 라운드로 set 함

        NMHMapMng.instance.DestroyGridNotifications();          //이전 라운드의 그리드 알림 삭제

        NMHMapMng.instance.SetGridAToB(NMHGrid.GridType.PROHIBITION, NMHGrid.GridType.NONE);         //이전 라운드의 금지 그리드를 모두 NONE 그리드로 바꿈

        for (int i = 0; i < GetCurRound() - 1; i++)     //현재 라운드 -1 만큼 랜덤한 NONE 그리드를 CAUTION 그리드로 바꿈
        {
            NMHMapMng.instance.SetGridAToB(NMHGrid.GridType.NONE, NMHGrid.GridType.CAUTION, true);
        }

        if(nCurRound != 1)      //1라운드가 아니면
        {
            hPlayer.SetEn(hPlayer.GetEn() + 10);        //기력 10 회복
            hEnemy.SetEn(hPlayer.GetEn() + 10);        //기력 10 회복

            InGameUICtrl.SetEnBar(hPlayer);         //게이지바 셋
            InGameUICtrl.SetEnBar(hEnemy);

            NMHUIEffectMng.instance.CreateEnergyEffect(hPlayer);
            NMHUIEffectMng.instance.CreateEnergyEffect(hEnemy);
        }

        StartSelectCard();                                               //카드 선택 시작
    }

    public void StartSelectCard()              //카드 선택 시작시 호출
    {
        InGameUICtrl.jscRope.ResetBar();        //시간 제한 리셋
        InGameUICtrl.jscRope.ShowBar();         //시간 제한 시작

        bIsFinishedCardSelecting = false;       //카드 선택 false -> 이제 카드 선택 시작

        Invoke("FinishSelectCard", nCardSelectingTime);         //카드 선택 시간이 지나면 강제로 카드 선택을 종료시킴
    }

    public void FinishSelectCard()             //카드 선택 끝난 후 호출
    {
        for(int i = 0; i < 3; i ++)
        {
            nMoveCardCnt[i] = 0;   //초기화
        }

        InGameUICtrl.jscRope.HideBar();         //시간 제한 종료

        if (bIsFinishedCardSelecting == false)                          //아직 카드 선택이 끝나지 않았으면
        {
            CancelInvoke();     //카드 강제 선택 invoke 취소
                
            bIsFinishedCardSelecting = true;                                                //카드 선택이 끝났다고 알려줌

            hEnemy.SetHeroSelCard(hEnemy.GetComponent<NMHAI>().GetAISelectedCard());        //AI의 선택을 적의 카드에 집어넣음 

            for (int i = 0; i < 3; i++)
            {
                InGameUICtrl.ShowEnemySelectedCards(i);
            }

            NMHMapMng.instance.DestroyGridNotifications(NMHGrid.GridType.CAUTION);  //경고 그리드 알림 삭제

            NMHMapMng.instance.SetGridAToB(NMHGrid.GridType.CAUTION, NMHGrid.GridType.PROHIBITION);     //방금 만든 경고 그리드를 모두 금지 그리드로 바꿈

            InGameUICtrl.ButtonFinishSelectCard();

            for(int i = 0; i < 3; i ++)
            {
                if (hPlayer.GetHeroSelCard(i).GetAttackPointArr().Length == 0 && (hPlayer.GetHeroSelCard(i).GetID() != 5) && (hPlayer.GetHeroSelCard(i).GetID() != 11))
                {
                    nMoveCardCnt[i]++;
                }

                if (hEnemy.GetHeroSelCard(i).GetAttackPointArr().Length == 0 && (hEnemy.GetHeroSelCard(i).GetID() != 5) && (hEnemy.GetHeroSelCard(i).GetID() != 11))
                {
                    nMoveCardCnt[i]++;
                }

                Debug.Log(nMoveCardCnt[i]);
            }


            StartCoroutine(StartPhase(0));
        }
    }

    public IEnumerator StartPhase(int _nTargetPhase)           //페이즈 시작
    {
        SetCurPhase(_nTargetPhase);

        StartCoroutine(CompareTP(_nTargetPhase));

        if(nCurPhase == 2)
        {
            yield return new WaitForSeconds(16.0f - (nMoveCardCnt[nCurPhase]) * 3f);
        }
        else
        {
            yield return new WaitForSeconds(19.0f - (nMoveCardCnt[nCurPhase]) * 3f);
        }
     
        if (nCurPhase < 2) //페이즈 1, 2 ,3까지
        {
            nCurPhase++;

            StartCoroutine(StartPhase(nCurPhase));
        }
        else
        {
            yield return new WaitForSeconds(1.0f);

            FinishRound();
        }
    }

    public IEnumerator CompareTP(int _nTPhase)
    {
        InGameUICtrl.OpenSelectedCard(_nTPhase);        //양 쪽의 카드 아이콘을 열어줌

        NMHUIEffectMng.instance.CreateCompareEffect();  //카드 비교 이펙트

        yield return new WaitForSeconds(2.0f);

        if (hPlayer.GetHeroSelCard(nCurPhase).GetTP() >= hEnemy.GetHeroSelCard(nCurPhase).GetTP())    //선제력 비교 : 플레이어가 더 높거나 같으면
        {
            yield return StartCoroutine(StartTurnInOrder(hPlayer, hEnemy));  //플레이어 - 적 순으로 행동
        }
        else if (hPlayer.GetHeroSelCard(nCurPhase).GetTP() < hEnemy.GetHeroSelCard(nCurPhase).GetTP())      //선제력 비교 : 적이 더 높으면
        {
            yield return StartCoroutine(StartTurnInOrder(hEnemy, hPlayer));  //적 - 플레이어 순으로 행동
        }
    }

    public IEnumerator StartTurnInOrder(NMHHero _hFirstHero, NMHHero _hSecondHero) //매개변수의 순서에 따라 각자의 턴의 행동을 실행함
    {
        bIEPhase = true;

        StartCoroutine(DoBehaviorInTurn(_hFirstHero));                  //첫번째 플레이어의 행동

        InGameUICtrl.CloseSelectedCard(_hSecondHero, GetCurPhase());    //두번째 플레이어의 카드 아이콘을 닫음

        if(_hFirstHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length != 0 || (_hFirstHero.GetHeroSelCard(nCurPhase).GetID() == 5) || (_hFirstHero.GetHeroSelCard(nCurPhase).GetID() == 11))
        {
            yield return new WaitForSeconds(8f);
        }
        else
        {
            yield return new WaitForSeconds(3.5f);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        InGameUICtrl.CloseSelectedCard(_hFirstHero, GetCurPhase());    //첫번째 플레이어의 카드 아이콘을 닫음
        InGameUICtrl.OpenSelectedCard(_hSecondHero, GetCurPhase());    //두번째 플레이어의 카드 아이콘을 염

        StartCoroutine(DoBehaviorInTurn(_hSecondHero));                //두번째 플레이어의 행동

        while (bIEBehaviorLive)                 //행동중이면 
        {
            yield return null;                  //기다림
        }

        InGameUICtrl.CloseSelectedCard(_hSecondHero, GetCurPhase());    //두번째 플레이어의 카드 아이콘을 닫음

        if (_hSecondHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length != 0 || (_hSecondHero.GetHeroSelCard(nCurPhase).GetID() == 5) || (_hSecondHero.GetHeroSelCard(nCurPhase).GetID() == 11) || nCurPhase == 2)
        {
            yield return new WaitForSeconds(3.5f);
        }
        else
        {
            yield return new WaitForSeconds(1.75f);
        }

        bIEPhase = false;
    }

    public IEnumerator DoBehaviorInTurn(NMHHero _hTHero)               //각자의 턴에 하는 행동
    {
        bIEBehaviorLive = true;

        _hTHero.SetTriangle(true);      //삼각형 표시 (이 영웅이 움직이고 있다)

        NMHUIEffectMng.instance.CreateUseSkillEffect(_hTHero);  //카드 사용 이펙트

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //아래는 금지 타일에 의한 피해

        if (NMHMapMng.instance.GetGrid(_hTHero.GetCurPoint()).GetGridType() == NMHGrid.GridType.PROHIBITION)                      //현재 영웅이 있는 그리드가 금지 그리드이면
        {
            _hTHero.SetHP(_hTHero.GetHP() - 3);                  //10만큼 데미지를 줌
            NMHUIEffectMng.instance.CreateProhibitionEffect(_hTHero);  //카드 사용 이펙트
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //아래는 카드의 기력 소모

        _hTHero.SetEn(_hTHero.GetEn() - _hTHero.GetHeroSelCard(nCurPhase).GetEn());

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //아래는 이동

        StartCoroutine(MoveHero(_hTHero));

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        yield return new WaitForSeconds(1.5f);

        if ( (_hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length != 0) || (_hTHero.GetHeroSelCard(nCurPhase).GetID() == 5) || (_hTHero.GetHeroSelCard(nCurPhase).GetID() == 11))      //공격을 하거나, 특수스킬이면
        {
            switch (_hTHero.GetUnitType())
            {
                case NMHUnit.UnitType.PLAYER:

                    //아래는 공격

                    yield return new WaitForSeconds(1f);

                    StartCoroutine(AttackHero(hPlayer, hEnemy));

                    yield return new WaitForSeconds(2.5f);

                    break;

                case NMHUnit.UnitType.ENEMY:
                    Debug.Log("Turn For Enemy");

                    //아래는 공격

                    yield return new WaitForSeconds(1f);

                    StartCoroutine(AttackHero(hEnemy, hPlayer));

                    yield return new WaitForSeconds(2.5f);

                    break;
            }
        }

        InGameUICtrl.SetHPBar(hPlayer);
        InGameUICtrl.SetHPBar(hEnemy);

        InGameUICtrl.SetEnBar(hPlayer);
        InGameUICtrl.SetEnBar(hEnemy);

        _hTHero.SetTriangle(false);      //삼각형 표시닫음 (이 영웅의 움직임이 멈췃다.)

        bIEBehaviorLive = false;
    }



    public IEnumerator MoveHero(NMHHero _hTHero)                           //각자의 턴에 하는 이동
    {
        if ( (_hTHero.GetHeroSelCard(nCurPhase).GetMoveType() == NMHCard.MoveType.RELATIVE) && (_hTHero.GetHeroSelCard(nCurPhase).GetMovePoint() != new NMHPoint(0, 0)) )                      //관계해서 움직이고 이동이 없지 않으면
        {
            NMHPoint pMovePoint = _hTHero.GetCurPoint() + _hTHero.GetHeroSelCard(nCurPhase).GetMovePoint();

            if( (_hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length != 0) && (_hTHero.GetUnitirection() == NMHUnit.UnitDirection.LEFT) ) //왼쪽을 바라보고 공격하는 카드일때
            {
                pMovePoint = new NMHPoint(_hTHero.GetCurPoint().fX - _hTHero.GetHeroSelCard(nCurPhase).GetMovePoint().fX,
                                          _hTHero.GetCurPoint().fY + _hTHero.GetHeroSelCard(nCurPhase).GetMovePoint().fY);
            }

            pMovePoint.fX = Mathf.Clamp(pMovePoint.fX, 0, 4);               // 이펙트 생성 위치 범위 조절
            pMovePoint.fY = Mathf.Clamp(pMovePoint.fY, 0, 3);

            NMHSpriteEffectMng.instance.CreateEffect(NMHSpriteEffectMng.SprEfType.GRID_MOVE, NMHMapMng.instance.GetGrid(pMovePoint).GetGridVec2());             //이펙트 생성

            _hTHero.RunAnimationByPara("move"); //애니메이션

            yield return new WaitForSeconds(1.5f);                                                                                                                               //이펙트 생성 후 1.5초 딜레이

            _hTHero.MovePointByRel(pMovePoint - _hTHero.GetCurPoint());            //카드의 이동 정보에 따라 이동

            CheckHeroDirection();       //히어로 방향 조정
        }
    }

    public IEnumerator AttackHero(NMHHero _hTHero, NMHHero _hTVictim)                     //각자의 턴에 하는 공격
    {
        for (int i = 0; i < _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length; i++)                  //임시 공격 이펙트 생성,, 이후 수정 필요 -> 코드 더 이쁘게, 중복 코드 줄이고 커플링 없애주기
        {
            NMHPoint pAttackPoint = new NMHPoint(0, 0);

            if (_hTHero.GetUnitirection() == NMHUnit.UnitDirection.RIGHT)            //유닛이 오른쪽을 바라보고 있을 때
            {
                pAttackPoint = _hTHero.GetCurPoint() + _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr()[i];
            }
            else if (_hTHero.GetUnitirection() == NMHUnit.UnitDirection.LEFT)            //유닛이 왼쪽을 바라보고 있을 때
            {
                pAttackPoint = new NMHPoint(_hTHero.GetCurPoint().fX - _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr()[i].fX,
                                            _hTHero.GetCurPoint().fY + _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr()[i].fY);  
            }

            if (_hTHero.GetHeroSelCard(nCurPhase).GetAttackType() == NMHCard.AttackType.RELATIVE && pAttackPoint.fX < 5 && pAttackPoint.fX >= 0 && pAttackPoint.fY < 4 && pAttackPoint.fY >= 0)         // 공격 포인트가 맵 범위 안이면 이펙트 생성
            {
                NMHSpriteEffectMng.instance.CreateEffect(NMHSpriteEffectMng.SprEfType.GRID_ATTACK, NMHMapMng.instance.GetGrid(pAttackPoint).GetGridVec2());         //이펙트 생성
            }
        }
       
        yield return new WaitForSeconds(1.5f);                                                                                                                               //이펙트 생성 후 1.5초 딜레이

        Vector2[] vec2AttackArr = new Vector2[_hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length];

        for (int i = 0; i < _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr().Length; i++)
        {
            NMHPoint pAttackPoint = new NMHPoint(0, 0);

            if (_hTHero.GetUnitirection() == NMHUnit.UnitDirection.RIGHT)            //유닛이 오른쪽을 바라보고 있을 때
            {
                pAttackPoint = _hTHero.GetCurPoint() + _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr()[i];
            }
            else if (_hTHero.GetUnitirection() == NMHUnit.UnitDirection.LEFT)            //유닛이 왼쪽을 바라보고 있을 때
            {
                pAttackPoint = new NMHPoint(_hTHero.GetCurPoint().fX - _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr()[i].fX,
                                            _hTHero.GetCurPoint().fY + _hTHero.GetHeroSelCard(nCurPhase).GetAttackPointArr()[i].fY);
            }

            if (pAttackPoint.fX > 4 || pAttackPoint.fX < 0 || pAttackPoint.fY > 3 || pAttackPoint.fY < 0)
            {
                vec2AttackArr[i] = new Vector2(-9999, -9999);
            }
            else
            {
                vec2AttackArr[i] = NMHMapMng.instance.GetGrid(pAttackPoint).GetGridVec2();      //공격하는 vec2 저장

            }
            
            if (pAttackPoint == _hTVictim.GetCurPoint())      //카드의 공격 범위와 적의 위치 비교
            {
                _hTVictim.SetHP(_hTVictim.GetHP() - _hTHero.GetHeroSelCard(nCurPhase).GetAD());          //공격 범위를 돌아가며 검색하여 카드 범위와 적 위치가 같으면 공격

                NMHUIEffectMng.instance.CreateDamageEffect(_hTVictim);  //카드 사용 이펙트

                _hTVictim.RunAnimationByPara("hit");
            }
        }

        _hTHero.SetAttackArr(vec2AttackArr);

        _hTHero.RunAnimationByPara(_hTHero.GetHeroSelCard(nCurPhase).GetCardKey());

        yield return new WaitForSeconds(1.5f);
    }

    public void FinishRound()
    {
        NMHSpriteEffectMng.instance.CreateEffect("spr_ef_round_end", new Vector2(0, 0));          //라운드 종료 애니메이션

        InGameUICtrl.ShowPlayerSelectCardAfterFight();      //숨겼던 카드 선택 창을 다시 켬

        for (int i = 0; i < 3; i++)
        {
            InGameUICtrl.HideEnemySelectedCards(i);     //적이 선택한 카드를 가림
        }

        nCurRound++;

        StartCoroutine(StartRound(GetCurRound()));

        for (int i = 0; i < 3; i++)
        {
            InGameUICtrl.DeselectCard(i);
        }
    }

    public void CheckDead()
    {
        if(bIsDead == false)
        {
            if(hPlayer.GetHP() <= 0 || hEnemy.GetHP() <= 0)
            {
                bIsDead = true;                             //누군가가 죽었다

                StartCoroutine(NoticeFinishGame());             //게임 종료를 알림
            }
        }
    }

    public IEnumerator NoticeFinishGame()                      //게임 종료를 알릴 때 호출
    {
        Debug.Log("Game Finished!");

        yield return new WaitForSeconds(4.0f);

        FinishGameUICtrl.SetText();                     //게임 종료 팝업의 텍스트 set
        FinishGameUICtrl.ShowPopUp();                   //게임 종료 팝업을 보여라

        if(hPlayer.GetHP() <= 0 )
        {
            NMHSpriteEffectMng.instance.CreateEffect("spr_ef_result_defeat");
        }
        else if (hEnemy.GetHP() <= 0)
        {
            NMHSpriteEffectMng.instance.CreateEffect("spr_ef_result_victory");
        }

        StopAllCoroutines();
    }  

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 getter, setter

    public NMHHero GetHeroByType(NMHUnit.UnitType _tTargetType)                                         //Unit Type을 통해 Hero 가져옴
    {
        NMHHero tmpHero = NMHMapMng.instance.GetUnitByUnitType(_tTargetType).GetComponent<NMHHero>();

        return tmpHero;
    }

    /// <summary>
    /// 선택한 카드의 정보를 찾아 데이터로 넘겨준 후, 그 데이터를 받아옴 (즉, 내가 선택한 카드가 무엇인지 받아옴)
    /// </summary>
    /// <param name="_nTargetHeroType"></param>
    /// <param name="_nTargetPhase"></param>
    /// <returns></returns>
    public NMHCard GetSelCard(NMHHero _hTHero, int _nTargetPhase)
    {
        NMHCard cTargetCard = new NMHCard(0, 0, 0); //선택한 카드의 데이터 정보를 넘기기 위해 만든 빈 Card 클래스

        switch (_hTHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                cTargetCard.SetCard(hPlayer.GetHeroSelCard(_nTargetPhase).GetCard());
                break;
            case NMHUnit.UnitType.ENEMY:
                cTargetCard.SetCard(hEnemy.GetHeroSelCard(_nTargetPhase).GetCard());
                break;
        }

        return cTargetCard;
    }

    /// <summary>
    /// 선택한 카드의 정보를 찾아 데이터로 넘겨줌.
    /// </summary>
    /// <param name="_nTargetHeroType"></param>
    /// <param name="_nTargetPhase"></param>
    /// <param name="_cTargetCard"></param>
    public void SetSelCard(NMHHero _hTHero, int _nTargetPhase, NMHCard _cTargetCard)
    {
        switch (_hTHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                hPlayer.GetHeroSelCard(_nTargetPhase).SetCard(_cTargetCard);
                break;
            case NMHUnit.UnitType.ENEMY:
                hEnemy.GetHeroSelCard(_nTargetPhase).SetCard(_cTargetCard);
                break;
        }
    }

    public int GetCurRound()
    {
        return nCurRound;
    }

    public void SetCurRound(int _nRoundNum)
    {
        nCurRound = _nRoundNum;
    }

    public int GetCurPhase()
    {
        return nCurPhase;
    }

    public void SetCurPhase(int _nTargetPhase)
    {
        nCurPhase = _nTargetPhase;
    }

    public int GetCurTurn()
    {
        return nCurTurn;
    }

    public void SetCurTurn(int _nTargetTurn)
    {
        nCurTurn = _nTargetTurn;
    }

    public float GetSelectingTIme()
    {
        return nCardSelectingTime;
    }

    public float GetPlayTime()
    {
        return fPlayTIme;
    }
}
