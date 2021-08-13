using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHAI : MonoBehaviour
{ 
    int[, ] nArrCard;

    NMHCard[] cAICard;

	void Start ()
    {
        InitAI();
    }
	
	void Update ()
    {
		
	}

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    void InitAI()
    {
        cAICard = new NMHCard[3];

        nArrCard = new int[3, 10];

        nArrCard[0, 0] = 13;    //좌 이동
        nArrCard[1, 0] = 1;     //스킬 1
        nArrCard[2, 0] = 16;    //하 이동 

        nArrCard[0, 1] = 14;     //우 이동
        nArrCard[1, 1] = 3;    //스킬 3
        nArrCard[2, 1] = 2;     //스킬 2

        nArrCard[0, 2] = 4;     //스킬 4
        nArrCard[1, 2] = 13;    //좌 이동
        nArrCard[2, 2] = 5;     //스킬 5

        nArrCard[0, 2] = 4;     //스킬 4
        nArrCard[1, 2] = 13;    //좌 이동
        nArrCard[2, 2] = 5;     //스킬 5

        nArrCard[0, 3] = 2;     //스킬 2
        nArrCard[1, 3] = 14;    //우 이동
        nArrCard[2, 3] = 6;     //궁극기

        nArrCard[0, 4] = 5;     //스킬 5
        nArrCard[1, 4] = 13;    //좌 이동
        nArrCard[2, 4] = 5;     //스킬 5(특수 기술)

        nArrCard[0, 5] = 5;     //스킬 5(특수 기술)
        nArrCard[1, 5] = 13;    //좌 이동
        nArrCard[2, 5] = 14;     //우 이동
    } 

    void SelectCard()
    {
        for(int i = 0; i < 3; i ++)
        {
            cAICard[i] = NMHCardInfoMng.instance.GetCardInfoByID(nArrCard[i, NMHGameMng.instance.GetCurRound() - 1]);
        }

        for(int i = 0; i < 3; i ++)
        {
            //int nRand = Random.Range(0, 2);

            //if (nRand == 0)             //공격
            //{
            //    switch(this.gameObject.GetComponent<NMHHero>().GetHeroJob())
            //    {
            //        case LJJHeroDataMng.HeroJob.WARRIOR:
            //            cAICard[i] = NMHCardInfoMng.instance.GetCardInfoByID(Random.Range(1, 7));
            //            break;

            //        case LJJHeroDataMng.HeroJob.WIZARD:
            //            cAICard[i] = NMHCardInfoMng.instance.GetCardInfoByID(Random.Range(7, 13));
            //            break;

            //        case LJJHeroDataMng.HeroJob.BOSS_2:
            //            cAICard[i] = NMHCardInfoMng.instance.GetCardInfoByID(Random.Range(21, 27));
            //            break;
            //    }


            //}
            //else if(nRand == 1)             //이동
            //{
            //    cAICard[i] = NMHCardInfoMng.instance.GetCardInfoByID(Random.Range(13, 17));
            //}
        }
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 getter, setter 함수

    public NMHCard[] GetAISelectedCard()
    {
        SelectCard();

        return cAICard;
    }
}
