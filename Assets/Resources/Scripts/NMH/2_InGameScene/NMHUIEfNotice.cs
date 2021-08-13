using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NMHUIEfNotice : MonoBehaviour
{
    public Text txtNotice;
	
    public void SetToCompareSkill(NMHCard _cCard)
    {
        txtNotice.text = _cCard.GetCardNotice() + " : 선제력 " + _cCard.GetTP().ToString();
    }

    public void SetToUseSkill(NMHCard _cCard)
    {
        txtNotice.text = _cCard.GetCardNotice() + " 발동!";
    }
    
    public void SetToDamage(string _strName)
    {
        txtNotice.text = "피해 " + _strName + "!";
    }

    public void SetToHealEnergy(int nHeal)
    {
        txtNotice.text = "기력 회복 " + nHeal.ToString() + "!";
    }
}
