using UnityEngine.UI;
using UnityEngine;

public class SMSMoneyMng : MonoBehaviour
{
    public static SMSMoneyMng instance;
    public int nMyMoney;
    public int nMoneyUpdate;
    public int nItemCost;
    public string stMoneyData;

    void Awake()
    {
        Init();
    }

    public static SMSMoneyMng GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SMSMoneyMng)) as SMSMoneyMng;
            }
            return instance;
        }
    }

    void Init()
    {
        nMyMoney = 0;
        nMoneyUpdate = 1000;
        nItemCost = 100;
        stMoneyData = "Money";
    }

    //void Awake()
    //{
    //    nMoneyUpdate = PlayerPrefs.GetInt(MoneyDataSt);
    //}

    //void Start()
    //{
    //    Init();
    //}

    //void Update()
    //{
    //    nMyMoney = nMoneyUpdate;
    //    MoneyTx.text = "" + nMyMoney;
    //}

    //public void Init()
    //{
    //    nMyMoney = PlayerPrefs.GetInt("MONEY", 1000);
    //}

    //public void GetMoney()
    //{
    //    nMoneyUpdate += nItemCost;
    //}

    //public void BuyItem()
    //{
    //    if (nMyMoney > 0)
    //    {
    //        nMoneyUpdate -= nItemCost;
    //    }
    //}

    //public void SaveData(string key, int value)
    //{
    //    PlayerPrefs.SetInt(key, value);
    //    PlayerPrefs.Save();
    //}

    //public void OnDestroy()
    //{
    //    SaveData(MoneyDataSt, nMoneyUpdate);
    //}
}