using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSCSoundMng : MonoBehaviour
{
    public AudioClip bgm;
    public AudioClip fx;
    public GameObject da; //배경음
    public AudioSource fxaudio;
    AudioSource audioe;
    public AudioClip[] audioClips = new AudioClip[4];
  


    enum state { ed, es , ef};

    public static JSCSoundMng instance;
    void Awake()
    {
    
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }   
    }
    AudioSource GetBGM()
    {
        return GetComponent<AudioSource>();
    }
    void Start()
    {
        fxaudio.clip = fx;
        bgm = GetComponent<AudioClip>();
        audioe = new AudioSource();
        audioe = GetComponent<AudioSource>();
        audioe.Play();
        audioe.loop = true ;
        
    }
    public void Bgmsound() //배경음
    {
        audioe.PlayOneShot(audioe.clip);
    }
    public void Bgmoffsound() 
    {
        audioe.Pause();
    }
    public void Bgmonsound() 
    {
        audioe.Play();
    }
    public void eChangemusic(AudioClip car)  //배경음악교체
    {
        if(GetComponent<AudioSource>().clip != null)
        {
            audioe.Stop();
        }

        GetComponent<AudioSource>().clip = car;
        GetComponent<AudioSource>().Play();
    }

    public void Fxsound() //효과음
    {
        fxaudio.PlayOneShot(fxaudio.clip);
    }

    public void Fxoffsound() //효과음 on
    {
        fxaudio.mute = true;
    }

    public void Fxonsound()  //효과음 off
    {
        fxaudio.mute = false;
    }

    public void Changemusic(AudioClip fx)  //효과음악교체
    {
        fxaudio.Stop();
      
        fxaudio.clip = audioClips[2];
      fxaudio.Play();
    }

    public void RunSoundByPath(string _strPath)
    {
        fxaudio.PlayOneShot(Resources.Load<AudioClip>("Sounds/FX/" + _strPath));
    }

    public void RunSoundByClip(AudioClip ac)
    {
        fxaudio.PlayOneShot(ac);
    }

    void Update()
    {

    }
}