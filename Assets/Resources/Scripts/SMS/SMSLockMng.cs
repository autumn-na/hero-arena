using UnityEngine.UI;
using UnityEngine;

public class SMSLockMng : MonoBehaviour
{
    public Button[] MountButtonB; //부스터 장착버튼
    public Button[] MountButtonC; //캐릭터 장착버튼
    public Image[] ItemImgB; //부스터 이미지
    public Image[] ItemImgC; //캐릭터 이미지
    public Animator ButtonAni; //버튼 애니메이션
    public Text MoneyTx; //돈 텍스트

    void Awake() // 돈,아이템 정보 가져오기
    {
        SMSMoneyMng.GetInstance.nMoneyUpdate = PlayerPrefs.GetInt(SMSMoneyMng.GetInstance.stMoneyData);
        LoadItemInfo();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        UtInit();
        MoneyUp();
    }

    void Init() // 돈 초기화
    {
        SMSMoneyMng.GetInstance.nMyMoney = PlayerPrefs.GetInt("MONEY", 1000);
    }

    void UtInit() // 돈 업데이트, 텍스트 출력
    {
        SMSMoneyMng.GetInstance.nMyMoney = SMSMoneyMng.GetInstance.nMoneyUpdate;
        MoneyTx.text = "" + SMSMoneyMng.GetInstance.nMyMoney;
    }

    void SaveData(string key, int value) // 정보 저장
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    void OnDestroy() // 끝날 때 돈 정보 저장
    {
        SaveData(SMSMoneyMng.GetInstance.stMoneyData, SMSMoneyMng.GetInstance.nMoneyUpdate);
    }

    void LoadItemInfo() // 아이템 정보
    {
        for (int i = 0; i < 6; i++)
        {
            if (SMSPlayerInfoMng.GetInstance.bIsLock[0, i] == true)
            {
                if (SMSPlayerInfoMng.GetInstance.nMountConditionB[i] == false)
                {
                    MountButtonB[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                }
                else if (SMSPlayerInfoMng.GetInstance.nMountConditionB[i] == true)
                {
                    MountButtonB[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
                }
            }
        }
        for (int i = 0; i < 6; i++)
        {
            if (SMSPlayerInfoMng.GetInstance.bIsLock[1, i] == true)
            {
                if (SMSPlayerInfoMng.GetInstance.nMountConditionC[i] == false)
                {
                    MountButtonC[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                }
                else if (SMSPlayerInfoMng.GetInstance.nMountConditionC[i] == true)
                {
                    MountButtonC[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
                }
            }
        }
    }

    public void OnMountButtonBooster(int nButtonNumber) // 부스터 구매, 장착, 장착해제 버튼
    {
        if (SMSPlayerInfoMng.GetInstance.bIsLock[0, nButtonNumber] == true)
        {
            if (MountButtonB[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui"))
            {
                MountButtonB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionB[nButtonNumber] = true;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingItem[0, nButtonNumber] = true;
                SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] = 2;
            }
            else if (MountButtonB[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui_2"))
            {
                MountButtonB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionB[nButtonNumber] = true;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingItem[0, nButtonNumber] = true;
                SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] = 2;
            }
            else if (MountButtonB[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui"))
            {
                MountButtonB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionB[nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingItem[0, nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] = 1;
            }
            else if (MountButtonB[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui_2"))
            {
                MountButtonB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionB[nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingItem[0, nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] = 1;
            }
            JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/button"));
        }
        if (SMSPlayerInfoMng.GetInstance.bIsLock[0, nButtonNumber] == false)
        {
            if (SMSMoneyMng.GetInstance.nMyMoney > SMSPlayerInfoMng.GetInstance.nUnlockCondition[0, nButtonNumber])
            {
                SMSMoneyMng.GetInstance.nMoneyUpdate -= SMSPlayerInfoMng.GetInstance.nUnlockCondition[0, nButtonNumber];
                SMSPlayerInfoMng.GetInstance.bIsLock[0, nButtonNumber] = true;
                MountButtonB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] = 1;

                JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/buy"));
            }
        }
    }

    public void OnMountBtChar(int nButtonNumber) // 캐릭터 구매, 장착, 장착해제 버튼
    {
        if (SMSPlayerInfoMng.GetInstance.bIsLock[1, nButtonNumber] == true)
        {
            if (MountButtonC[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui"))
            {
                MountButtonC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionC[nButtonNumber] = true;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingChar = true;
                SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] = 2;
            }
            else if (MountButtonC[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui_2"))
            {
                MountButtonC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionC[nButtonNumber] = true;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingChar = true;
                SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] = 2;
            }
            else if (MountButtonC[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui"))
            {
                MountButtonC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionC[nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingItem[1, nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] = 1;
            }
            else if (MountButtonC[nButtonNumber].GetComponent<Image>().sprite == Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui_2"))
            {
                MountButtonC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                SMSPlayerInfoMng.GetInstance.nMountConditionC[nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.bCurrentMountingItem[1, nButtonNumber] = false;
                SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] = 1;
            }
            JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/button"));
        }
        if (SMSPlayerInfoMng.GetInstance.bIsLock[1, nButtonNumber] == false)
        {
            if (SMSMoneyMng.GetInstance.nMyMoney > SMSPlayerInfoMng.GetInstance.nUnlockCondition[1, nButtonNumber])
            {
                SMSMoneyMng.GetInstance.nMoneyUpdate -= SMSPlayerInfoMng.GetInstance.nUnlockCondition[1, nButtonNumber];
                SMSPlayerInfoMng.GetInstance.bIsLock[1, nButtonNumber] = true;
                MountButtonC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
                SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] = 1;

                JSCSoundMng.instance.RunSoundByClip(Resources.Load<AudioClip>("Sounds/FX/UI/button"));
            }
        }
    }

    public void OnMouseEnter() // 뒤로가기 애니메이션
    {
        ButtonAni.SetBool("OnMouse", true);
    }

    public void OnMouseExit() // 뒤로가기 애니메이션 끄기
    {
        ButtonAni.SetBool("OnMouse", false);
    }

    public void OnMouseEnterBooster(int nButtonNumber) // 부스터 구매, 장착, 장착해제 버튼 애니메이션
    {
        if (SMSPlayerInfoMng.GetInstance.nItemCount[0,nButtonNumber] == 0)
        {
            ItemImgB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/100_Jewelry_Ui_2");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] == 1)
        {
            ItemImgB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui_2");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] == 2)
        {
            ItemImgB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui_2");
        }
    }

    public void OnMouseEnterChar(int nButtonNumber) // 캐릭터 구매, 장착, 장착해제 버튼 애니메이션
    {
        if (SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] == 0)
        {
            ItemImgC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/100_Jewelry_Ui_2");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] == 1)
        {
            ItemImgC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui_2");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] == 2)
        {
            ItemImgC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui_2");
        }
    }

    public void OnMouseExitBooster(int nButtonNumber) // 부스터 구매, 장착, 장착해제 애니메이션 끄기
    {
        if (SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] == 0)
        {
            ItemImgB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/100_Jewelry_Ui");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] == 1)
        {
            ItemImgB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[0, nButtonNumber] == 2)
        {
            ItemImgB[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
        }

    }
    public void OnMouseExitChar(int nButtonNumber) // 캐릭터 구매, 장착, 장착해제 애니메이션 끄기
    {
        if (SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] == 0)
        {
            ItemImgC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/100_Jewelry_Ui");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] == 1)
        {
            ItemImgC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearing_Ui");
        }
        else if (SMSPlayerInfoMng.GetInstance.nItemCount[1, nButtonNumber] == 2)
        {
            ItemImgC[nButtonNumber].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Wearingrelease_Ui");
        }
    }
    
    void MoneyUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SMSMoneyMng.GetInstance.nMoneyUpdate += 1000;
        }
    }
}