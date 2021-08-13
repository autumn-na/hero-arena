using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Hero_Window : MonoBehaviour {

    public Text Hero_Name;
    public Text Enemy_Hero_Name;
    public Image Player_Image;
    public Image Enemy_Player_Image;


    // Use this for initialization
    void Start()
    {
        LJJHeroDataMng lJJHeroDataMng = LJJHeroDataMng.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            Hero_Name.text = "아쳐";
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            Hero_Name.text = "어쌔신";
        }

        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            Hero_Name.text = "나이트";
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            Hero_Name.text = "위치";
        }







        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            Enemy_Hero_Name.text = "아쳐";
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            Enemy_Hero_Name.text = "어쌔신";
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            Enemy_Hero_Name.text = "나이트";
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            Enemy_Hero_Name.text = "위치";
        }












        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_1");
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_1");
        }

        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_1");
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_1");
        }



        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            Enemy_Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Archer_Icon_1");
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            Enemy_Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Assassin_Icon_1");
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            Enemy_Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Warrior_Icon_1");
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            Enemy_Player_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/4_LobbyScene/Witch_Icon_1");
        }

    }
}
