using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LJJPickUICtrl : MonoBehaviour
{
    public GameObject ShowHeroCase;

    public GameObject ShowEnemyCase;

    public GameObject ShowChampionExplanation;          //챔피언 설명을 보여주는 거

    public GameObject ShowChampion_Illustration;        //챔피언 일러스트를 보여주는 거

    public GameObject ShowEnemyExplanation;

    public GameObject ShowEnemy_Illustration;

    public GameObject Please_Choice;

    public GameObject[] Lock_Hero;

    public GameObject[] Champions_Button;

    public GameObject[] Enemy_Button;

    public GameObject[] Hero_Pick_Check;

    public GameObject[] Enemy_Pick_Check;

    public Sprite[] Do_Not_Choice_Enemy_Or_Champion;    //적 혹은 챔피언을 선택하지 않을 경우 나오는 이미지

    public GameObject[] AGPCF;

    public Animator[] Anibutton;

    public Image[] hero;
    public Image[] enemy;

    int testValue;

    int nCCValue = 0;


    ///////적 이미지//////////

    // Use this for initialization
    void Start()
    {
        LJJHeroDataMng lJJHeroDataMng = LJJHeroDataMng.Instance;
        JSCSoundMng jSCSoundMng = JSCSoundMng.instance;
        for (int i = 0; i < 4; i++)
        {
            Enemy_Button[i].GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyPortrait[i];
            Champions_Button[i].GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprHeroPortrait[i];
            Hero_Pick_Check[i].SetActive(false);
            Enemy_Pick_Check[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
            Enemy_Button[i + 1].GetComponent<Button>().interactable = false;
        Please_Choice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LJJHeroDataMng.Instance.nClearCount[nCCValue]++;
            Invoke("UnLock_Enemy", 0f);
        }
    }
    ////////////////챔피언에 관련된 버튼/////////////////////////



    public void Champion(int _type)
    {
        JSCSoundMng.instance.fxaudio.PlayOneShot(JSCSoundMng.instance.fxaudio.clip);
        Enemy_Pick_Check[3].SetActive(false);
        LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.NONE;
        ShowEnemy_Illustration.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyCase[6];
        ShowEnemyCase.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyCase[6];
        ShowEnemyExplanation.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyExplanation[6];
        for (int i = 0; i < 3; i++)
        {
            Enemy_Pick_Check[i].SetActive(false);
        }

        nCCValue = _type;
        int hero_value = _type + 1;
        testValue = _type;
        if (_type == 0 || _type == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                if (hero_value > 3)
                    hero_value = 0;
                Enemy_Button[i].GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyPortrait[hero_value];
                hero_value++;
            }
            Enemy_Button[3].GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyPortrait[4];
        }
        else if (_type == 2 || _type == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                if (hero_value > 3)
                    hero_value = 0;
                Enemy_Button[i].GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyPortrait[hero_value];
                hero_value++;
            }
            Enemy_Button[3].GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyPortrait[5];
        }
        for (int i = 0; i < 4; i++) { Hero_Pick_Check[i].SetActive(false); }
        Hero_Pick_Check[_type].SetActive(true);
        ShowChampionExplanation.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprHeroExplanation[_type];
        ShowChampion_Illustration.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprHeroIllust[_type];
        ShowHeroCase.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprHeroCase[_type];

        if (_type == 0)
        {
            LJJHeroDataMng.Instance.herojob = LJJHeroDataMng.HeroJob.WARRIOR;
        }
        if (_type == 1)
        {
            LJJHeroDataMng.Instance.herojob = LJJHeroDataMng.HeroJob.ASSASSIN;
        }
        if (_type == 2)
        {
            LJJHeroDataMng.Instance.herojob = LJJHeroDataMng.HeroJob.WIZARD;
        }
        if (_type == 3)
        {
            LJJHeroDataMng.Instance.herojob = LJJHeroDataMng.HeroJob.ARCHER;
            //LJJHeroDataMng.Instance.strHeroName[_type] = "ARCHER";
        }
        Invoke("UnLock_Enemy", 0f);
    }

    public void Enemy(int _type)
    {
        JSCSoundMng.instance.fxaudio.PlayOneShot(JSCSoundMng.instance.fxaudio.clip);
        int enemyValue;
        enemyValue = _type;
        enemyValue = testValue + _type + 1;

        if (enemyValue == 4)
        {
            enemyValue = 0;
        }
        else if (enemyValue == 5)
        {
            enemyValue = 1;
        }
        else if (enemyValue == 6)
        {
            enemyValue = 2;
        }
        else if (enemyValue == 7)
        {
            enemyValue = 3;
        }
        for (int i = 0; i < 4; i++) { Enemy_Pick_Check[i].SetActive(false); }
        Enemy_Pick_Check[_type].SetActive(true);
        ShowEnemy_Illustration.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyIllust[enemyValue];
        ShowEnemyCase.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyCase[enemyValue];
        ShowEnemyExplanation.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyExplanation[enemyValue];
        //ShowEnemyCase.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyCase[enemyValue];
        LJJHeroDataMng.Instance.nEnemyPick = enemyValue;
        if (_type == 3)
        {
            if (testValue == 0 || testValue == 1)
            {
                ShowEnemyExplanation.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyExplanation[4];
                ShowEnemy_Illustration.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyIllust[4];
                ShowEnemyCase.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyCase[4];
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.BOSS_1;
            }
            if (testValue == 2 || testValue == 3)
            {
                ShowEnemyExplanation.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyExplanation[5];
                ShowEnemy_Illustration.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyIllust[5];
                ShowEnemyCase.GetComponent<Image>().sprite = LJJHeroDataMng.Instance.sprEnemyCase[5];
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.BOSS_2;
            }
        }
        if (testValue == 0)
        {
            if (_type == 0)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.ASSASSIN;
            }
            if (_type == 1)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.WIZARD;
            }
            if (_type == 2)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.ARCHER;
            }
        }
        if (testValue == 1)
        {
            if (_type == 0)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.WIZARD;
            }
            if (_type == 1)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.ARCHER;
            }
            if (_type == 2)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.WARRIOR;
            }
        }
        if (testValue == 2)
        {
            if (_type == 0)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.ARCHER;
            }
            if (_type == 1)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.WARRIOR;
            }
            if (_type == 2)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.ASSASSIN;
            }
        }
        if (testValue == 3)
        {
            if (_type == 0)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.WARRIOR;
            }
            if (_type == 1)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.ASSASSIN;
            }
            if (_type == 2)
            {
                LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.WIZARD;
            }
        }
    }


    public void Back_Button()
    {
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Lobby);
        LJJHeroDataMng.Instance.herojob = LJJHeroDataMng.HeroJob.NONE;
        LJJHeroDataMng.Instance.enemyjob = LJJHeroDataMng.HeroJob.NONE;
    }

    public void Go_Battle_Button()
    {
        Please_Choice.SetActive(false);
        if (LJJHeroDataMng.Instance.herojob != LJJHeroDataMng.HeroJob.NONE && LJJHeroDataMng.Instance.enemyjob != LJJHeroDataMng.HeroJob.NONE)
            NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.InGame);
    }
    void Show_Select_Champion()
    {
        Please_Choice.SetActive(false);
    }

    void UnLock_Enemy()
    {
        for (int i = 0; i < 3; i++)
        {
            AGPCF[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < LJJHeroDataMng.Instance.nClearCount[nCCValue]; i++)
        {
            // Lock_Hero[i].SetActive(false);
            AGPCF[i].GetComponent<Button>().interactable = true;
        }
    }

    public void OnMouseEnter(int _type)
    {
        Anibutton[_type].SetBool("OnMouse", true);
    }

    public void OnMouseExit(int _type)
    {
        Anibutton[_type].SetBool("OnMouse", false);
    }
}
