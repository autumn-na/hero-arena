using UnityEngine;
using UnityEngine.UI;

public class SMSInGameUI : MonoBehaviour
{
    public SpriteRenderer[] PlayerEnemyImg;

    void Start()
    {
        Init();
    }

    void Init()
    {

        LJJHeroDataMng lJJHeroDataMng = LJJHeroDataMng.Instance;
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            PlayerEnemyImg[0].sprite = Resources.Load<Sprite>("Textures/Illustrations/Warrior_Illust");
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            PlayerEnemyImg[0].sprite = Resources.Load<Sprite>("Textures/Illustrations/Witch_Illust");
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            PlayerEnemyImg[0].sprite = Resources.Load<Sprite>("Textures/1_PickScene/champion_4");
        }
        if (LJJHeroDataMng.Instance.herojob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            PlayerEnemyImg[0].sprite = Resources.Load<Sprite>("Textures/1_PickScene/champion_2");
        }

        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.WARRIOR)
        {
            PlayerEnemyImg[1].sprite = Resources.Load<Sprite>("Textures/Illustrations/Warrior_Illust");
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.WIZARD)
        {
            PlayerEnemyImg[1].sprite = Resources.Load<Sprite>("Textures/Illustrations/Witch_Illust");
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.ARCHER)
        {
            PlayerEnemyImg[1].sprite = Resources.Load<Sprite>("Textures/1_PickScene/champion_4");
        }
        if (LJJHeroDataMng.Instance.enemyjob == LJJHeroDataMng.HeroJob.ASSASSIN)
        {
            PlayerEnemyImg[1].sprite = Resources.Load<Sprite>("Textures/1_PickScene/champion_2");
        }
    }
}