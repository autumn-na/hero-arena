using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NMHInGameUICtrl : MonoBehaviour
{
    public JSCRope jscRope;

    public GameObject objPlayerSelectCard;
    public GameObject objEnemySelectCard;

    public Button butOpenCard;

    public GameObject[] objEnemySelectedCardArr;

    public Text txtCurRound;

    public Text[] txtHPText; //0:my 1:enemy
    public Text[] txtEnText; // 0: my, 1: enemy

    public Slider[] sliHP; //0: my, 1: enemy
    public Slider[] sliEN; //0: my, 1: enemy

    public Button butStartRound;
    public Button butSelectCard;

    public Button[] butSelectedCard;

    public InputField[] inputMyAD;
    public InputField[] inputMyTP;
    public InputField[] inputMyEn;
    public InputField[] inputEAD;
    public InputField[] inputETP;
    public InputField[] inputEEn;

    public RectTransform panelRectTransform;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 변수

    bool bIsCardSelecting = false;  //카드를 선택하고 있는가?, 초기화

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 unity 이벤트 함수

    void Start()
    {
        JSCSoundMng.instance.eChangemusic(Resources.Load<AudioClip>("Sounds/BGM/Ingame"));

        panelRectTransform.anchorMin = new Vector2(1, 0);
        panelRectTransform.anchorMax = new Vector2(0, 1);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);

        for(int i = 0; i < 2; i ++)
        {
            sliHP[i].GetComponent<JSCHpbar>().SetHPMax(150);
            sliEN[i].GetComponent<JSCHpbar>().SetHPMax(140);
        }
    }

    private void Update()
    {
 
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 버튼 작동 함수

    //public void ButtonStartRound() //프로토타입 작동용 함수
    //{
    //    //라운드 시작
    //    NMHGameMng.instance.StartRound(NMHGameMng.instance.GetCurRound());

    //    //시작한 라운드 표시
    //    txtCurRound.text = NMHGameMng.instance.GetCurRound().ToString();

    //    //라운드 버튼 선택 불가로
    //    butStartRound.interactable = false;
    //}

    public void ButtonShowHideSelectCard()
    {

        JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/open_close_select_card"));

        if (bIsCardSelecting == false) //카드 고르는 화면이 꺼져있으면
        {
            bIsCardSelecting = true;         //카드가 켜졌다.
            objPlayerSelectCard.GetComponent<Animation>().Play("anim_open_player_select_card");       //여는 애니메이션1
            objPlayerSelectCard.GetComponent<NMHPopUpSelectCardCtrl>().ShowHideOKButton(true);            //버튼 킴

            RotateButtonOpenCard(false);        //카드 여는 버튼을 닫는 버튼으로
        }
        else if (bIsCardSelecting) //카드 고르는 화면이 켜져있으면
        {
            bIsCardSelecting = false;   //카드가 꺼졌다.
            objPlayerSelectCard.GetComponent<Animation>().Play("anim_close_player_select_card");       //닫는 애니메이션
            objPlayerSelectCard.GetComponent<NMHPopUpSelectCardCtrl>().ShowHideOKButton(false);            //버튼 끔

            RotateButtonOpenCard(true);        //카드 닫는 버튼을 여는 버튼으로
        }
    }
    
    public void ButtonShowHideSelectCard(bool _bTargetShow)
    {

        JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/open_close_select_card"));

        if (_bTargetShow == true) //카드 고르는 화면이 꺼져있으면
        {
            bIsCardSelecting = true;         //카드를 키고싶으면
            objPlayerSelectCard.GetComponent<Animation>().Play("anim_open_player_select_card");       //여는 애니메이션1
            objPlayerSelectCard.GetComponent<NMHPopUpSelectCardCtrl>().ShowHideOKButton(true);            //버튼 킴

            RotateButtonOpenCard(false);        //카드 여는 버튼을 닫는 버튼으로
        }
        else if (_bTargetShow == false) //카드 고르는 화면이 켜져있으면
        {
            bIsCardSelecting = false;   //카드를 끄고싶으면
            objPlayerSelectCard.GetComponent<Animation>().Play("anim_close_player_select_card");       //닫는 애니메이션
            objPlayerSelectCard.GetComponent<NMHPopUpSelectCardCtrl>().ShowHideOKButton(false);            //버튼 끔


            RotateButtonOpenCard(true);        //카드 닫는 버튼을 여는 버튼으로

        }
    }

    public void ButtonFinishSelectCard()        //OK. 버튼
    {
        HidePlayerSelectCardToFight();
        RotateButtonOpenCard(true);             //여는 버튼으로 바꿔줌


        JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/open_close_select_card"));
    }

    public void ButtonDeselectCard(int _nTarget)
    {
        if (objPlayerSelectCard.activeInHierarchy)                                                                                         //카드 선택 창이 켜져 있으면
        {
            Sprite sprSelected = EventSystem.current.currentSelectedGameObject.GetComponent<NMHSelectedCard>().GetForeground();     //임시 변수에 누른 버튼의 정보를 넣어줌 

            if (sprSelected != null)                                                                                                  //임시 변수의 스프라이트가 널이 아니면(선택되었으면)
            {
                DeselectCard(_nTarget);
            }
        }
    }

    //public void ButtonFinishSelectCard() //프로토타입 작동용 함수
    //{    
    //    for (int i = 0; i < 3; i++) //내가 선택한 카드를 set 함.
    //    {
    //        NMHGameMng.instance.SetSelCard(NMHGameMng.HeroType.PLAYER, i, new NMHCard(int.Parse(inputMyAD[i].text), int.Parse(inputMyTP[i].text), int.Parse(inputMyEn[i].text)));
    //    }

    //    for (int i = 0; i < 3; i++) //상대가 선택한 카드를 set 함.
    //    {
    //        NMHGameMng.instance.SetSelCard(NMHGameMng.HeroType.ENEMY, i, new NMHCard(int.Parse(inputEAD[i].text), int.Parse(inputETP[i].text), int.Parse(inputEEn[i].text)));
    //    }

    //    NMHGameMng.instance.FinishSelectCard();
    //}

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 일반 public 함수

    public void SetRoundText() //프로토타입 작동용 함수
    {
        //라운드 텍스트 내용을 현재 라운드로 변경
        txtCurRound.text = NMHGameMng.instance.GetCurRound().ToString();
    }

    public void ResetInputsAndButtons()
    {
        butStartRound.interactable = true;

        for (int i = 0; i < 3; i++)
        {
            inputMyAD[i].text = "";
            inputMyTP[i].text = "";
            inputEAD[i].text = "";
            inputETP[i].text = "";
            inputMyEn[i].text = "";
            inputEEn[i].text = "";
        }
}

    public void SetHPBar(NMHHero _hTHero)
    {
        switch(_hTHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                txtHPText[0].text = NMHMapMng.instance.GetUnitByUnitType(NMHUnit.UnitType.PLAYER).GetHP().ToString() + " / " + 150.ToString();
                sliHP[0].GetComponent<JSCHpbar>().SetHPBar(NMHMapMng.instance.GetUnitByUnitType(NMHUnit.UnitType.PLAYER).GetHP());
                break;
            case NMHUnit.UnitType.ENEMY:
                txtHPText[1].text = NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetHP().ToString() + " / " + 150.ToString();
                sliHP[1].GetComponent<JSCHpbar>().SetHPBar(NMHMapMng.instance.GetUnitByUnitType(NMHUnit.UnitType.ENEMY).GetHP());
                break;
        }
    }

    public void SetEnBar(NMHHero _hTHero)
    {
        switch (_hTHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                txtEnText[0].text = NMHMapMng.instance.GetUnitByUnitType(NMHUnit.UnitType.PLAYER).GetEn().ToString() + " / " + 140.ToString();
                sliEN[0].GetComponent<JSCHpbar>().SetHPBar(NMHMapMng.instance.GetUnitByUnitType(NMHUnit.UnitType.PLAYER).GetEn());
                break;
            case NMHUnit.UnitType.ENEMY:
                txtEnText[1].text = NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetEn().ToString() + " / " + 140.ToString();
                sliEN[1].GetComponent<JSCHpbar>().SetHPBar(NMHMapMng.instance.GetUnitByUnitType(NMHUnit.UnitType.ENEMY).GetEn());
                break;
        }
    }

    public void DeselectCard(int _nTarget)
    {
        butSelectedCard[_nTarget].GetComponent<NMHSelectedCard>().SetForeground(Resources.Load<Sprite>("Textures/Cards/card_unvisible"));     //투명한 이미지로 바꿔줌

        NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).SetHeroSelCard(_nTarget, NMHCardInfoMng.instance.GetCardInfoByID(0));      //데이터를 0으로 바꿔줌

        objPlayerSelectCard.GetComponent<NMHPopUpSelectCardCtrl>().bIsSelectable[_nTarget] = true;
    }

    public void ShowEnemySelectedCards(int _nTPhase)        //선택한 페이즈의 적 카드 공개
    {
        objEnemySelectedCardArr[_nTPhase].GetComponent<NMHSelectedCard>().SetForeground(Resources.Load<Sprite>("Textures/Cards/Icons/card_icon_" + NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetHeroSelCard(_nTPhase).GetID()));
    }

    public void HideEnemySelectedCards(int _nTPhase)
    {
        objEnemySelectedCardArr[_nTPhase].GetComponent<NMHSelectedCard>().SetForeground(Resources.Load<Sprite>("Textures/Cards/card_unvisible"));
    }

    public void ShowPlayerSelectCardAfterFight()
    {
        objPlayerSelectCard.GetComponent<Animation>().Play("anim_open_player_select_card_after_fight");
        butOpenCard.GetComponent<Animation>().Play("anim_open_button_open_card");

        bIsCardSelecting = false;       // 아직 카드가 선택되지는 않았다.
    }

    public void HidePlayerSelectCardToFight()
    {
        objPlayerSelectCard.GetComponent<Animation>().Play("anim_close_player_select_card_to_fight");
        butOpenCard.GetComponent<Animation>().Play("anim_close_button_open_card");

        objPlayerSelectCard.GetComponent<NMHPopUpSelectCardCtrl>().ShowHideOKButton(false);

        bIsCardSelecting = false;       // 아직 카드가 선택되지는 않았다.
    }

    public void RotateButtonOpenCard(bool _bIsCanOpen)
    {
        if(_bIsCanOpen == true) //열 수 있는 상태로 돌려줌
        {
            butSelectCard.transform.rotation = Quaternion.Euler(0, 0, 0);                     //카드 여는 버튼 회전 - 열 수 있는 (여는) 버튼의 모양으로 바꿔줌
        }
        else if(_bIsCanOpen == false)   //열 수 없는 상태로 돌려줌
        {
            butSelectCard.transform.rotation = Quaternion.Euler(0, 0, 180);                     //카드 여는 버튼 회전 - 열 수 없는 (닫는) 버튼의 모양으로 바꿔줌
        }
    }

    public void OpenSelectedCard(int _nPhase)
    {
        objPlayerSelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_open");
        objEnemySelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_open");
    }

    public void CloseSelectedCard(int _nPhase)
    {
        objPlayerSelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_close");
        objEnemySelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_close");
    }

    public void OpenSelectedCard(NMHHero _hHero, int _nPhase)
    {
        switch(_hHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                objPlayerSelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_open");
                break;
            case NMHUnit.UnitType.ENEMY:
                objEnemySelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_open");
                break;
        }
    }

    public void CloseSelectedCard(NMHHero _hHero, int _nPhase)
    {
        switch (_hHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                objPlayerSelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_close");
                break;
            case NMHUnit.UnitType.ENEMY:
                objEnemySelectCard.GetComponent<Animator>().SetTrigger(_nPhase.ToString() + "_close");
                break;
        }
    }
}
