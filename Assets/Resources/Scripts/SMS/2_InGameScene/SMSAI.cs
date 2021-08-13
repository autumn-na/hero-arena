using UnityEngine;

public class SMSAI : MonoBehaviour
{
    int[,] nPlayerAct;
    int[,] nAIAct;
    int[] nNowAct;

    void Start()
    {
        Init();
    }

    void Update()
    {
        AI_Low();
        AI_High();
    }

    void Init()
    {
        nPlayerAct = new int[2, 3];
        nAIAct = new int[2, 3];
        nNowAct = new int[2];
    }

    void AI_Low()
    {
        for (int i = 0; i < 3; i++)
        {
            if (nNowAct[0] == nPlayerAct[0, i])
            {
                nNowAct[1] = nAIAct[0, Random.Range(0, 1)];
            }
            if (nNowAct[0] == nPlayerAct[1, i])
            {
                nNowAct[1] = nAIAct[1, Random.Range(1, 1)];
            }
        }
    }

    void AI_High()
    {
        for (int i = 0; i < 3; i++)
        {
            if (nNowAct[0] == nPlayerAct[0, i])
            {
                nNowAct[1] = nAIAct[0, Random.Range(0, 2)];
            }
            if (nNowAct[0] == nPlayerAct[1, i])
            {
                nNowAct[1] = nAIAct[1, Random.Range(1, 2)];
            }
        }
    }
}