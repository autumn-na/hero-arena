using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSSoundMng : MonoBehaviour
{
    public static SMSSoundMng instance;
    public AudioClip[] Sound;
    AudioSource MyAudio;

    public static SMSSoundMng GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SMSSoundMng)) as SMSSoundMng;
            }
            return instance;
        }
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        MyAudio = GetComponent<AudioSource>();
    }

    public void ClickButton()
    {
        MyAudio.PlayOneShot(Sound[0]);
    }
    
    public void BuyButton()
    {
        MyAudio.PlayOneShot(Sound[1]);
    }
}