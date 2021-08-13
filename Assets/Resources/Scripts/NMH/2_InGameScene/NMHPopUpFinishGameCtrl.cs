using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NMHPopUpFinishGameCtrl : MonoBehaviour
{
    public Text txtPlayTime;
    public Text txtTotalRound;

    public Text txtLeftHP;
    public Text txtLeftEN;
    public Text txtEarnedMoney;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 함수

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 버튼 작동 함수

    public void ButtonChallengeAgain()                                                    //재도전
    {
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.InGame);
    }

    public void ButtonGoLobby()                                                           //로비로 이동
    {
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Lobby);
    }
    
    public void HidePopUp()                                                         //팝업을 가려라
    {
        this.gameObject.SetActive(false);
    }

    public void ShowPopUp()                                                         //팝업을 보여라
    {
        this.gameObject.SetActive(true);
    }

    public void SetText()
    {
        int nPlayTimeHour = (int)(NMHGameMng.instance.GetPlayTime() / 3600);
        int nPlayTimeMinute = (int)(NMHGameMng.instance.GetPlayTime() % 3600) / 60;
        int nPlayTimeSecond = (int)(NMHGameMng.instance.GetPlayTime() % 60);

        txtPlayTime.text = nPlayTimeHour.ToString("00") + " : " + nPlayTimeMinute.ToString("00") + " : " + nPlayTimeSecond.ToString("00");
        txtTotalRound.text = NMHGameMng.instance.GetCurRound().ToString();

        txtLeftHP.text = NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).GetHP().ToString();
        txtLeftEN.text = NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).GetEn().ToString();

        txtEarnedMoney.text = 150.ToString();
    }
}
