using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JSCHpbar : MonoBehaviour {

    public Slider Hpbar;


    public int Hp_value;  // 이거 줄였다 늘렸다

    private void Start()
    {

    }

    void Update()
    {
        //Hpbar.value = Mathf.MoveTowards(Hpbar.value, a, Time.deltaTime * 20);
        Hpbar.value = Mathf.Lerp (Hpbar.value, Hp_value, Time.deltaTime * 2);

    }

    public void SetHPMax(int _nTMax)
    {
        Hpbar.maxValue = _nTMax;
        Hp_value = _nTMax;
    }

    public void SetHPBar(int _nTValue)
    {
        Hp_value = _nTValue;
    }
}

