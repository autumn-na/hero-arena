using UnityEngine;
using UnityEngine.UI;

public class SMSLobbyMenu : MonoBehaviour
{
    public GameObject toggle; //배경음 토글
    public GameObject ytoggle; //효과음 토글
    public GameObject HelpGam; //도움말 팝업
    public GameObject[] PageImg; //도움말 설명이미지
    public GameObject[] ExplanImg; //도움말 설명글
    public GameObject[] IconImgGam; //카드리스트 아이콘 선택이미지
    public GameObject[] CardListGam; //직업별 카드리스트
    public Text[] InfoTx; //전적 텍스트
    public Text PageTx; //도움말 페이지 텍스트
    public Button[] IconBt; //카드리스트 아이콘 버튼
    public Animator[] ButtonAni; //버튼 애니메이션
    public Image FaceImg; //초상화 이미지
    int nMaxPlay; //총 플레이 수
    int nMaxVictory; // 총 승리 수
    int nMostPlayCharacter; // 최다 플레이 캐릭터
    int nMostVictoriousCharacter; //최다 승리 캐릭터
    int nPage; //도움말 페이지 수

    void Start()
    {
        Init();
    }

    void Update()
    {
        TextInit();
        CountKey();
        MaxValue();
        Help();
        ImgUt();
    }

    public void reee() // 배경음토글
    {
        if (toggle.GetComponent<Toggle>().isOn == false)
        {
            Debug.Log("배경음 켜짐");
            JSCSoundMng.instance.Fxsound();
            JSCSoundMng.instance.Bgmonsound();
        }
        else
        {
            Debug.Log("배경음 꺼짐");
            JSCSoundMng.instance.Fxsound();
            JSCSoundMng.instance.Bgmoffsound();
        }
    }

    public void ereee() // 효과음토글
    {

        if (ytoggle.GetComponent<Toggle>().isOn == false)
        {
            Debug.Log("효과음 켜짐");
            JSCSoundMng.instance.Fxonsound();
            JSCSoundMng.instance.Fxsound();
        }
        else
        {
            Debug.Log("효과음 꺼짐");
            JSCSoundMng.instance.Fxsound();
            JSCSoundMng.instance.Fxoffsound();
        }
    }

    void CountKey() //치트키
    {
        if (Input.GetKeyDown(KeyCode.A)) // 전사 플레이 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfPlay[0]++;
        }
        if (Input.GetKeyDown(KeyCode.S)) // 법사 플레이 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfPlay[1]++;
        }
        if (Input.GetKeyDown(KeyCode.D)) // 궁수 플레이 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfPlay[2]++;
        }
        if (Input.GetKeyDown(KeyCode.F)) // 도적 플레이 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfPlay[3]++;
        }
        if (Input.GetKeyDown(KeyCode.Z)) // 전사 승 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[0]++;
        }
        if (Input.GetKeyDown(KeyCode.X)) // 법사 승 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[1]++;
        }
        if (Input.GetKeyDown(KeyCode.C)) // 궁수 승 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[2]++;
        }
        if (Input.GetKeyDown(KeyCode.V)) // 도적 승 + 1
        {
            SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[3]++;
        }
    }

    void Init() // 초기화
    {
        JSCSoundMng.instance.eChangemusic(Resources.Load<AudioClip>("Sounds/BGM/Lobby"));

        Debug.Log(Resources.Load<AudioClip>("Sounds/BGM/Lobby").name);

        nMostPlayCharacter = 0;
        nMostVictoriousCharacter = 0;
        HelpGam.SetActive(false);
        nPage = 1;
    }
    
    void TextInit() // 텍스트 업데이트
    {
        nMaxPlay = SMSPlayerInfoMng.GetInstance.nNumberOfPlay[0] + SMSPlayerInfoMng.GetInstance.nNumberOfPlay[1] + SMSPlayerInfoMng.GetInstance.nNumberOfPlay[2] + SMSPlayerInfoMng.GetInstance.nNumberOfPlay[3];
        nMaxVictory = SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[0] + SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[1] + SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[2] + SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[3];
        InfoTx[0].text = "" + nMaxPlay;
        InfoTx[1].text = "" + nMaxVictory;
        if (nMostVictoriousCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[0])
        {
            InfoTx[2].text = "전사";
        }
        else if (nMostVictoriousCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[1])
        {
            InfoTx[2].text = "마녀";
        }
        else if (nMostVictoriousCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[2])
        {
            InfoTx[2].text = "궁수";
        }
        else if (nMostVictoriousCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[3])
        {
            InfoTx[2].text = "암살자";
        }
        if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[0])
        {
            InfoTx[3].text = "전사";
        }
        else if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[1])
        {
            InfoTx[3].text = "마녀";
        }
        else if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[2])
        {
            InfoTx[3].text = "궁수";
        }
        else if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[3])
        {
            InfoTx[3].text = "암살자";
        }
        PageTx.text = "" + nPage;
    }

    void MaxValue() // 최고 승수, 플레이 수
    {
        for (int i = 0; i < 4; i++)
        {
            if (SMSPlayerInfoMng.GetInstance.nNumberOfPlay[i] > nMostPlayCharacter)
            {
                nMostPlayCharacter = SMSPlayerInfoMng.GetInstance.nNumberOfPlay[i];
            }
            if (SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[i] > nMostVictoriousCharacter)
            {
                nMostVictoriousCharacter = SMSPlayerInfoMng.GetInstance.nNumberOfVictorious[i];
            }
        }
    }

    void Help() // 도움말 페이지
    {
        PageImg[nPage - 1].SetActive(true);
        ExplanImg[nPage - 1].SetActive(true);
    }

    void ImgUt() // 최다 플레이 캐릭터 초상화
    {
        if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[0])
        {
            FaceImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_1");
        }
        else if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[1])
        {
            FaceImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_1");
        }
        else if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[2])
        {
            FaceImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_1");
        }
        else if (nMostPlayCharacter == SMSPlayerInfoMng.GetInstance.nNumberOfPlay[3])
        {
            FaceImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_1");
        }
    }

    public void OnMouseEnter(int ButtonNumber) // 마우스 올려놨을 때 버튼 애니메이션
    {
        ButtonAni[ButtonNumber].SetBool("OnMouse", true);
    }

    public void OnMouseExit(int ButtonNumber) // 버튼 기본상태로 되돌아옴
    {
        ButtonAni[ButtonNumber].SetBool("OnMouse", false);
    }

    public void GameStartButton() // 픽씬으로 씬전환
    {
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Pick);
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void ShopButton() // 상점으로 씬전환
    {
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Shop);
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void HelpButton() // 도움말 팝업 활성화
    {
        HelpGam.SetActive(true);
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void CloseHelpButton() // 도움말 팝업 닫기
    {
        HelpGam.SetActive(false);
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void PageFrontButton() // 도움말 전페이지로
    {
        if (nPage > 1)
        {
            nPage--;
        }
        for (int i = 1; i < 6; i++)
        {
            PageImg[i - 1].SetActive(false);
            ExplanImg[i - 1].SetActive(false);
        }
        SMSSoundMng.GetInstance.ClickButton();
    }
    
    public void PageBackButton() // 도움말 다음페이지로
    {
        if (nPage < 5)
        {
            nPage++;
        }
        for (int i = 1; i < 6; i++)
        {
            PageImg[i - 1].SetActive(false);
            ExplanImg[i - 1].SetActive(false);
        }
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void IconSelectButton(int IconNumber) // 카드리스트 아이콘 버튼
    {
        if (IconNumber == 0) // 전사
        {
            IconBt[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_2");
            IconBt[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_1");
            IconBt[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_1");
            IconBt[3].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_1");
            IconImgGam[0].SetActive(true);
            CardListGam[0].SetActive(true);
            for (int i = 1; i < 4; i++)
            {
                IconImgGam[i].SetActive(false);
                CardListGam[i].SetActive(false);
            }
        }

        if (IconNumber == 1) // 마녀
        {
            IconBt[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_1");
            IconBt[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_2");
            IconBt[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_1");
            IconBt[3].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_1");
            IconImgGam[0].SetActive(false);
            IconImgGam[1].SetActive(true);
            for (int i = 2; i < 4; i++)
            {
                IconImgGam[i].SetActive(false);
                CardListGam[i].SetActive(false);
            }
            CardListGam[0].SetActive(false);
            CardListGam[1].SetActive(true);
        }

        if (IconNumber == 2) // 궁수
        {
            IconBt[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_1");
            IconBt[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_1");
            IconBt[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_2");
            IconBt[3].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_1");
            for (int i = 0; i < 2; i++)
            {
                IconImgGam[i].SetActive(false);
                CardListGam[i].SetActive(false);
            }
            IconImgGam[2].SetActive(true);
            IconImgGam[3].SetActive(false);
            CardListGam[2].SetActive(true);
            CardListGam[3].SetActive(false);
        }

        if (IconNumber == 3) // 암살자
        {
            IconBt[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_1");
            IconBt[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_1");
            IconBt[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_1");
            IconBt[3].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_2");
            for (int i = 0; i < 3; i++)
            {
                IconImgGam[i].SetActive(false);
                CardListGam[i].SetActive(false);
            }
            IconImgGam[3].SetActive(true);
            CardListGam[3].SetActive(true);
        }
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void QuitButton() // 종료버튼
    {
        Application.Quit();
        SMSSoundMng.GetInstance.ClickButton();
    }
}