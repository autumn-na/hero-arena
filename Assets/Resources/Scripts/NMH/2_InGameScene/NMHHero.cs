using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHHero : NMHUnit
{
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public GameObject objBody;
    public GameObject objTriangle;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 변수

    LJJHeroDataMng.HeroJob tHeroJob;

    NMHCard[] cSelCard;

    Animator aniconHero;

    Vector2[] vec2AttackArr;

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 unity 이벤트 함수

    private void Awake()
    {
        InitHero();                 //히어로 초기화
    }

    private void Start()
    {
        InitHero();
    }

    private void Update()
    {
        MoveHeroPointByKey();                                       //키보드로 히어로 움직임 -- 디버그용
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void RunAnimationByPara(string _strTpara)
    {
        gameObject.GetComponent<Animator>().SetTrigger(_strTpara);
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void InitHero()
    {
        switch(GetUnitType())
        {
            case UnitType.PLAYER:
                tHeroJob = LJJHeroDataMng.Instance.herojob;
                break;
            case UnitType.ENEMY:
                tHeroJob = LJJHeroDataMng.Instance.enemyjob;
                break;
        }

        cSelCard = new NMHCard[3];

        for(int i = 0; i < 3; i ++)
        {
            cSelCard[i] = new NMHCard();

            cSelCard[i].SetCard(0);
        }

        aniconHero = gameObject.GetComponent<Animator>();

        SetAniconHero();

        SetTriangle(false);
    }

    private void MoveHeroPointByKey()                               //키보드로 히어로 움직임 -- 디버그용
    {
        if (GetUnitType() == UnitType.PLAYER)                       //유닛 타입 검사 ; 플레이어
        {
            if (Input.GetKeyDown(KeyCode.W))                        //wsad로 플레이어 움직임
            {
                MovePointByRel(new NMHPoint(0, 1));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                MovePointByRel(new NMHPoint(0, -1));
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                MovePointByRel(new NMHPoint(-1, 0));
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                MovePointByRel(new NMHPoint(1, 0));
            }
        }

        if (GetUnitType() == UnitType.ENEMY)                        //유닛 타입 검사 : 적
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))                  //상화좌우 키보드로 적 움직임
            {
                MovePointByRel(new NMHPoint(0, 1));
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MovePointByRel(new NMHPoint(0, -1));
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePointByRel(new NMHPoint(-1, 0));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovePointByRel(new NMHPoint(1, 0));
            }
        }
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter

    public LJJHeroDataMng.HeroJob GetHeroJob()
    {
        return tHeroJob;
    }

    public void SetHeroJob(LJJHeroDataMng.HeroJob _tTHeroJob)
    {
        tHeroJob = _tTHeroJob;
    }

    public NMHCard GetHeroSelCard(int _nTPhase)
    {
        return cSelCard[_nTPhase];
    }

    public NMHCard[] GetHeroSelCard()
    {
        return cSelCard;
    }

    public void SetHeroSelCard(int _nTPhase, NMHCard _cTCard)
    {
        cSelCard[_nTPhase] = _cTCard;
        cSelCard[_nTPhase].SetCardJob(_cTCard.GetCardJob());
        cSelCard[_nTPhase].SetCardKey(_cTCard.GetCardKey());
    }

    public void SetHeroSelCard(NMHCard[] _cTCard)
    {
        cSelCard = _cTCard;
    }

    public Animator GetAniconHero()
    {
        return GetComponent<Animator>();
    }
    
    private void SetAniconHero()
    { 
        switch(GetUnitType())
        {
            case UnitType.PLAYER:

                switch (LJJHeroDataMng.Instance.herojob)
                {
                    case LJJHeroDataMng.HeroJob.WARRIOR:
                        aniconHero.runtimeAnimatorController = Resources.Load("Animators/2_InGameScene/Hero/anicon_warrior") as RuntimeAnimatorController;

                        break;

                    case LJJHeroDataMng.HeroJob.WIZARD:
                        aniconHero.runtimeAnimatorController = Resources.Load("Animators/2_InGameScene/Hero/anicon_witch") as RuntimeAnimatorController;
                        break;
                }

                break;
            case UnitType.ENEMY:

                switch (LJJHeroDataMng.Instance.enemyjob)
                {
                    case LJJHeroDataMng.HeroJob.WARRIOR:
                        aniconHero.runtimeAnimatorController = Resources.Load("Animators/2_InGameScene/Hero/anicon_warrior") as RuntimeAnimatorController;

                        break;

                    case LJJHeroDataMng.HeroJob.WIZARD:
                        aniconHero.runtimeAnimatorController = Resources.Load("Animators/2_InGameScene/Hero/anicon_witch") as RuntimeAnimatorController;
                        break;
                }

                break;
        }
    }

    public void SetTriangle(bool _bTarget)
    {
        if(_bTarget == true)
        {
            objTriangle.SetActive(true);
        }
        else if(_bTarget == false)
        {
            objTriangle.SetActive(false);
        }
    }

    public Vector2[] GetAttackArr()
    {
        return vec2AttackArr;
    }

    public void SetAttackArr(Vector2[] _vec2TargetArr)
    {
        vec2AttackArr = new Vector2[_vec2TargetArr.Length];

        vec2AttackArr = _vec2TargetArr;
    }

    public void RunSound(string _strPath)
    {
        JSCSoundMng.instance.RunSoundByPath(_strPath);
    }

    public void RunSoundByClip(AudioClip ac)
    {
        JSCSoundMng.instance.RunSoundByClip(ac);
    }

    public void RunSprEffect(string _strName)
    {
        for(int i = 0; i < vec2AttackArr.Length; i++)
        {
            if(GetUnitirection() == UnitDirection.LEFT)     //왼쪽을 바라보면
            {
                NMHSpriteEffectMng.instance.CreateEffect(_strName, vec2AttackArr[i], true);
            }
            else if (GetUnitirection() == UnitDirection.RIGHT)     //오른쪽을 바라보면
            {
                NMHSpriteEffectMng.instance.CreateEffect(_strName, vec2AttackArr[i], false);
            }
        }
    }

    public void RunSprEffectToNonFlip(string _strName)
    {
        for (int i = 0; i < vec2AttackArr.Length; i++)
        {
                NMHSpriteEffectMng.instance.CreateEffect(_strName, vec2AttackArr[i]);
        }
    }

    public void RunSprEffectToHero(string _strName)
    {
        NMHSpriteEffectMng.instance.CreateEffect(_strName, NMHMapMng.instance.GetGrid(GetCurPoint()).GetGridVec2());
    }
}
