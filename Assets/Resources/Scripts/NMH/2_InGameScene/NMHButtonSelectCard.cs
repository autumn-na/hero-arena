using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NMHButtonSelectCard : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 변수

    NMHCard cCard;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public Image imgFrame;
    public Image imgIcon;
    public Image imgToolTip;

    public Text txtTP;
    public Text txtAD;
    public Text txtEN;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 함수

    void Awake ()
    {
        InitButton();
    }

    private void Start()
    {

    }

    private void InitButton()
    {
        cCard = new NMHCard();
    }

    private void SetCardFrame()                       //카드 정보를 텍스트에 입력시킴
    {
        imgIcon.sprite = Resources.Load<Sprite>("Textures/Cards/Icons/card_icon_" + cCard.GetID().ToString());

        imgToolTip.sprite = Resources.Load<Sprite>("Textures/Cards/Explanations/card_explanation_" + cCard.GetID().ToString());

        switch (LJJHeroDataMng.Instance.herojob)
        {
            case LJJHeroDataMng.HeroJob.ARCHER:
                imgFrame.sprite = Resources.Load<Sprite>("Textures/Cards/Frames/archer_card_frame");
                break;
            case LJJHeroDataMng.HeroJob.ASSASSIN:
                imgFrame.sprite = Resources.Load<Sprite>("Textures/Cards/Frames/assassin_card_frame");
                break;
            case LJJHeroDataMng.HeroJob.WARRIOR:
                imgFrame.sprite = Resources.Load<Sprite>("Textures/Cards/Frames/warrior_card_frame");
                break;
            case LJJHeroDataMng.HeroJob.WIZARD:
                imgFrame.sprite = Resources.Load<Sprite>("Textures/Cards/Frames/witch_card_frame");
                break;
        }

        txtTP.text = cCard.GetTP().ToString();
        txtAD.text = cCard.GetAD().ToString();
        txtEN.text = cCard.GetEn().ToString();
        //txtTooltip;       //툴팁 수정 필요
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public NMHCard GetCard()
    {
        return cCard;
    }

    public void SetCard(NMHCard _cTargetCard)
    {
        cCard.SetCard(_cTargetCard);

        SetCardFrame();
    }
}
