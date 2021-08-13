using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHCardInfoMng : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 변수

    int nCardTotal;                                                                 //총 카드 갯수

    List<NMHCard> listCard;                                                  //카드의 정보가 담긴 리스트

    TextAsset taCardInfo;                                                           //텍스트에셋 : 텍스트 파일을 담음
    XmlDocument xdCardInfo;                                                         //XmlDocument : 로드한 xml파일을 담음
    XmlNodeList xnlCardInfo;                                                        //XmlNodeList : 로드한 xml파일의 자식 노드와 그 속성을 담음

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public static NMHCardInfoMng instance;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 private 함수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            InitMng();

            LoadCardInfo();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

    }

    private void InitMng()
    {
        taCardInfo = (TextAsset)Resources.Load("XMLs/CardInfo");                                            //텍스트에셋에 리소스폴더에서 로드한 xml파일을 형변환해서 넣어줌

        xdCardInfo = new XmlDocument();                                                                     //xml파일 담는곳에 공간을 만들어줌
        xdCardInfo.LoadXml(taCardInfo.text);                                                                //xml파일의 텍스트 불러옴
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void LoadCardInfo()  //XML 파일로 저장된 CardInfo 불러와 리스트에 저장
    {
        listCard = new List<NMHCard>();

        int nCardTotal;
        nCardTotal = int.Parse(xdCardInfo.SelectSingleNode("CardInfo/card_total").InnerText);

        xnlCardInfo = xdCardInfo.SelectNodes("CardInfo/card");                                                                        //루트 노드에서 자식 노드를 찾아 선택

        int nIDCnt;
        nIDCnt = 0;

        foreach (XmlNode xn in xnlCardInfo)
        { 
            int nTempID;
            nTempID = int.Parse(xn.SelectSingleNode("id").InnerText);

            if (nTempID == nIDCnt)
            {
                int nTotalAttack = int.Parse(xn.SelectSingleNode("attack").SelectSingleNode("attack_total").InnerText);                 //총 공격 포인트 갯수 받아옴

                NMHPoint[] pAttackPoint;
                pAttackPoint = new NMHPoint[nTotalAttack];                                                                              //총 공격 포인트 갯수만큼 포인트 배열 선언

                for (int i = 0; i < nTotalAttack; i++)                                                                                  //for문 돌며 포인트 배열에 공격 포인트 넣어줌
                {
                    NMHPoint pTempPoint = new NMHPoint(float.Parse(xn.SelectSingleNode("attack").SelectSingleNode("attack_point_" + i.ToString()).SelectSingleNode("x").InnerText),
                                                       float.Parse(xn.SelectSingleNode("attack").SelectSingleNode("attack_point_" + i.ToString()).SelectSingleNode("y").InnerText));

                    pAttackPoint[i] = pTempPoint;
                }

                NMHCard cItemCard;                                                                                   //데이터 전달을 위한 임시 카드 변수
                cItemCard = new NMHCard();

                cItemCard.SetCard(
                                    int.Parse(xn.SelectSingleNode("id").InnerText),
                                    int.Parse(xn.SelectSingleNode("ad").InnerText),
                                    int.Parse(xn.SelectSingleNode("tp").InnerText),
                                    int.Parse(xn.SelectSingleNode("en").InnerText),
                                    xn.SelectSingleNode("move").SelectSingleNode("move_type").InnerText,
                                    new NMHPoint(int.Parse(xn.SelectSingleNode("move").SelectSingleNode("move_point").SelectSingleNode("x").InnerText),
                                                 int.Parse(xn.SelectSingleNode("move").SelectSingleNode("move_point").SelectSingleNode("y").InnerText)),
                                    xn.SelectSingleNode("attack").SelectSingleNode("attack_type").InnerText,
                                    pAttackPoint
                                );

                cItemCard.SetCardJob((LJJHeroDataMng.HeroJob) System.Enum.Parse(typeof(LJJHeroDataMng.HeroJob), xn.SelectSingleNode("job").InnerText));

                cItemCard.SetCardNotice(xn.SelectSingleNode("notice").InnerText);

                cItemCard.SetCardKey(xn.SelectSingleNode("key").InnerText);

                listCard.Add(cItemCard);

                nIDCnt++;

                if(nIDCnt >= nCardTotal)
                {
                   // Debug.Log(nIDCnt);        //전체 카드 갯수 파악

                    break;
                }
            }
        }
    }

    public NMHCard GetCardInfoByID(int _nID)
    {
        return listCard[_nID];
    }
}
