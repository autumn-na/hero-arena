using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JSCRope : MonoBehaviour {

    private float done;
    public Slider Rope;
    public Slider Flame;

    float fTimeCnt = 0;

     void Start()
    {
        ResetBar();
    }

    void Update()
    {
        fTimeCnt += Time.deltaTime;

        if (Rope.value > 0f)
        {
            Rope.value = (done - fTimeCnt) / done * 100;
        }
        else
        {

        }
    
        if (Flame.value > 0f)
        {
            Flame.value = (done - fTimeCnt) / done * 100;
        }
    }


    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void ResetBar()
    {
        fTimeCnt = 0;

        done = NMHGameMng.instance.GetSelectingTIme();
        Rope.value = 100;
        Flame.value = 100;
    }

    public void ShowBar()
    {
        Rope.gameObject.SetActive(true);
        Flame.gameObject.SetActive(true);
    }

    public void HideBar()
    {
        Rope.gameObject.SetActive(false);
        Flame.gameObject.SetActive(false);
    }
}
