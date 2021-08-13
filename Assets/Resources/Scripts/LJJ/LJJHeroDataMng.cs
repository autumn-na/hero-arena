using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJJHeroDataMng : MonoBehaviour
{
    private static LJJHeroDataMng _instance = null;

    public static LJJHeroDataMng Instance;
    
    public int[] nHp;                   //영웅의 체력
    public int[] nEn;                   //영웅의 기력
    public int[] nClearCount;

    public string[] strHeroName;        //영웅의 이름
    public Sprite[] sprHeroPortrait;    //영웅 초상화
    public Sprite[] sprHeroExplanation;
    public Sprite[] sprHeroIllust;      //영웅 일러스트
    public Sprite[] sprHeroCase;

    public Sprite[] sprEnemyPortrait;
    public Sprite[] sprEnemyExplanation;
    public Sprite[] sprEnemyIllust;
    public Sprite[] sprEnemyCase;

    public int nHeroPick;
    public int nEnemyPick;
    //public Sprite[] sprHeroBigIllust;
    public HeroJob herojob;
    public HeroJob enemyjob;

    public enum HeroJob
    {
        NONE = 0,
        WARRIOR = 1,            //1일 경우 전사
        ASSASSIN = 2,             //2일 경우 암살자
        WIZARD = 3,             //3일 경우 법사
        ARCHER = 4,            //4일 경우 궁수

        BOSS_1 = 5,
        BOSS_2 = 6,

    }
    public enum EnemyJob
    {
        NONE = 0,
        ENEMY_WARRIOR = 1,
        ENEMY_ASSASSIN = 2,
        ENEMY_WIZARD = 3,
        ENEMY_ARCHER = 4,
        ENEMY_BOSS_1 = 5,
        ENMEY_BOSS_2 = 6,
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (nClearCount[i] > 3)
            {
                nClearCount[i] = 3;
            }
            else if(nClearCount[i] < 0)
            {
                nClearCount[i] = 0;
            }
            
        }
    }
}