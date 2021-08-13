using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NMHPopUpSelectCardCtrl : MonoBehaviour
{
    public GameObject objButtonCard;
    public GameObject objScrollContent;
    public GameObject objButtonOK;

    public NMHSelectedCard[] nmhSelectedCard;       //선택한 카드를 보여주는 오브젝트

    public RectTransform rtrScroll;

    public bool[] bIsSelectable;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 변수



    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void Awake()
    {
        InitPopUp();
    }

    private void Start()
    {
        
    }

    private void InitPopUp()                    //팝업 초기화 하는 부분
    {
        InitSelectable();
        InitButtonCard();
    }

    private void InitSelectable()                                             //선택 가능한 카드의 위치 초기화 함수
    {
        bIsSelectable = new bool[3];

        for (int i = 0; i < 3; i++)
        {
            bIsSelectable[i] = true; //선택 가능한 카드의 위치, 초기화
        }
    }

    private void InitButtonCard()                                                   //팝업에 나타나는 카드의 종류를 초기화하는 함수, 수정 필요!! 직업별로 다른 카드가 나와야 함
    {
        int nCardCnt = 0;
        

        for (int i = 13; i <= 16; i++)                  //이동 카드 생성
        {
            GameObject cloneButtonCard = Instantiate(objButtonCard, objScrollContent.transform);
            cloneButtonCard.transform.localPosition = new Vector3(-3000 + 450 * (nCardCnt), 0);
            cloneButtonCard.GetComponent<Button>().onClick.AddListener(() => { ButtonSelectCard(); });
            cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(i));

            nCardCnt++;
        }

        for (int i = 0; i < 4; i++)                  //이동 부스트 카드 생성
        {
            if(SMSPlayerInfoMng.instance.bCurrentMountingItem[0, i] == true)
            {
                GameObject cloneButtonCard = Instantiate(objButtonCard, objScrollContent.transform);
                cloneButtonCard.transform.localPosition = new Vector3(-3000 + 450 * (nCardCnt), 0);
                cloneButtonCard.GetComponent<Button>().onClick.AddListener(() => { ButtonSelectCard(); });

                switch(i)
                {
                    case 0:
                        cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(20));
                        break;
                    case 1:
                        cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(17));
                        break;
                    case 2:
                        cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(18));
                        break;
                    case 3:
                        cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(19));
                        break;
                }

                nCardCnt++;
            }
        }

        switch (LJJHeroDataMng.Instance.herojob)
        {
            case LJJHeroDataMng.HeroJob.WARRIOR:

                for (int i = 1; i <= 6; i++)
                {
                    GameObject cloneButtonCard = Instantiate(objButtonCard, objScrollContent.transform);
                    cloneButtonCard.transform.localPosition = new Vector3(-3000 + 450 * (nCardCnt), 0);
                    cloneButtonCard.GetComponent<Button>().onClick.AddListener(() => { ButtonSelectCard(); });
                    cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(i));

                    nCardCnt++;
                }

                break;

            case LJJHeroDataMng.HeroJob.WIZARD:

                for (int i = 7; i <= 12; i++)
                {
                    GameObject cloneButtonCard = Instantiate(objButtonCard, objScrollContent.transform);
                    cloneButtonCard.transform.localPosition = new Vector3(-3000 + 450 * (nCardCnt), 0);
                    cloneButtonCard.GetComponent<Button>().onClick.AddListener(() => { ButtonSelectCard(); });
                    cloneButtonCard.GetComponent<NMHButtonSelectCard>().SetCard(NMHCardInfoMng.instance.GetCardInfoByID(i));

                    nCardCnt++;
                }

                break;
        }

    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 버튼 작동 함수

    public void ButtonSelectCard()                                                                      //카드 선택 창에서 카드를 클릭하여 선택할 때 호출됨  //수정 필요!!!
    {
        JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/select_card"));
        
        GameObject objSelected = EventSystem.current.currentSelectedGameObject;                         //선택한 오브젝트를 가져옴

        for (int i = 0; i < 3; i++)                                                                     //카드가 선택되었는지 아닌지 검색
        {
            if (NMHGameMng.instance.GetSelCard(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER), i).GetID() <= 0)                                    //카드가 선택되지 않았으면
            {
                bIsSelectable[i] = true;                                                                //선택할 수 있도록
            }
            else if (NMHGameMng.instance.GetSelCard(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER), i).GetID() > 0)                                       //카드가 이미 선택되었다면
            {
                bIsSelectable[i] = false;                                                               //선택 불가능
            }
        }

        for (int i = 0; i < 3; i++)                                                                      //선택한 카드를 가장 위에 있는 빈 공간(카드 선택 가능한 공간)에 집어넣음
        {
            if (bIsSelectable[i] == true)
            {
                nmhSelectedCard[i].SetForeground(Resources.Load<Sprite>("Textures/Cards/Icons/card_icon_" + objSelected.GetComponent<NMHButtonSelectCard>().GetCard().GetID().ToString()));            //선택한 카드의 스프라이트를 선택한 카드를 보여주는 부분의 스프라이트에 넣어줌

                NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).SetHeroSelCard(i, objSelected.GetComponent<NMHButtonSelectCard>().GetCard());

                bIsSelectable[i] = false;

                break;
            }
        }
    }

    public void ButtonFinishSelectCard()            //카드 선택 완료 시 호출
    {
        if(NMHGameMng.instance.GetSelCard(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER), 0).GetID() != 0 &&
           NMHGameMng.instance.GetSelCard(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER), 1).GetID() != 0 &&
           NMHGameMng.instance.GetSelCard(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER), 2).GetID() != 0)                 //임시 -> 수정 필요!, 플레이어가 각 턴에 할 행동을 모두 선택했다면
        {
            ShowHideOKButton(false);            //OK버튼 숨김
            NMHGameMng.instance.FinishSelectCard();     //GameMng에 카드 선택 완료임을 전달
        }
    }

    public void HidePopUp()
    {
        this.gameObject.SetActive(false);           //카드 선택 창 투명화
    }

    public void ShowHideOKButton(bool _bTarget)
    {
        if(_bTarget == false)               //끄라고 하면 있으면
        {
            objButtonOK.SetActive(false);                       //끄고
        }
        else if(_bTarget == true)         //키라고 하면
        {
            objButtonOK.SetActive(true);                        //킨다
        }
    }
}
