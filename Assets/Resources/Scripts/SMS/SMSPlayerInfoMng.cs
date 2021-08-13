using UnityEngine;

public class SMSPlayerInfoMng : MonoBehaviour
{
    public static SMSPlayerInfoMng instance; //싱글톤
    public bool[,] bCurrentMountingItem; //현재 장착중인 아이템 정보(상점) [ex) 0,3 = 부스터 3번째 아이템 / 1,5 = 캐릭터 5번째 아이템] [*true = 장착중]
    public int[,] nUnlockCondition; //아이템 해금조건(상점)
    public bool[,] bIsLock; //아이템 잠금상태(상점)
    public int[] nNumberOfVictorious; //캐릭터별 승 수(로비)
    public int[] nNumberOfPlay; //캐릭터별 플레이 수(로비)
    public bool[] nMountConditionB; //부스터 아이템 장착상태(상점)
    public bool[] nMountConditionC; //캐릭터 아이템 장착상태(상점)
    public bool bCurrentMountingChar; //오류방지(상점)
    public int[,] nItemCount; //아이템 애니메이션 상태(상점)

    private void Awake()
    {
        Init();
    }

    public static SMSPlayerInfoMng GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SMSPlayerInfoMng)) as SMSPlayerInfoMng;
            }
            return instance;
        }
    }

    public void Init() // 초기화
    {
        instance = null;
        bCurrentMountingItem = new bool[2, 6];
        nUnlockCondition = new int[2, 6];
        bIsLock = new bool[2, 6];
        nNumberOfVictorious = new int[4];
        nNumberOfPlay = new int[4];
        bCurrentMountingChar = false;
        nMountConditionB = new bool[6];
        nMountConditionC = new bool[6];
        nItemCount = new int[2, 6];
        for (int i = 0; i < 4; i++)
        {
            nNumberOfVictorious[i] = 0;
            nNumberOfPlay[i] = 0;
        }
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                nUnlockCondition[j, i] = 100;
                bIsLock[j, i] = false;
                bCurrentMountingItem[j, i] = false;
                nItemCount[j, i] = 0;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            nMountConditionB[i] = false;
            nMountConditionC[i] = false;
        }
    }
}