using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

    public Text HeroName;
    public Image Player_Image;
  
    
    // Use this for initialization
    void Start () {
        LJJHeroDataMng lJJHeroDataMng = LJJHeroDataMng.Instance;

    }

    // Update is called once per frame
    void Update () {
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            HeroName.text = "ARCHER";
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            HeroName.text = "ASSASSIN";
        }
     
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            HeroName.text = "WARRIOR";
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            HeroName.text = "WIZARD";
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
    }
}
