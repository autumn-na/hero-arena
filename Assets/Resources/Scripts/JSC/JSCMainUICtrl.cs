using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JSCMainUICtrl : MonoBehaviour
{
   
    public GameObject Option;
    public GameObject button;
    public GameObject Credits;
    public GameObject toggle; //배경음 토글
    public GameObject ytoggle; //효과음 토글
    public GameObject BackMusic;



    
    void Start()
    {
        //toggle.GetComponent<Toggle>().isOn = true;

        JSCSoundMng.instance.eChangemusic(Resources.Load<AudioClip>("Sounds/BGM/Main"));
    }

    void Update()
    {  
        //if (toggle.GetComponent<Toggle>().isOn == false)
        //{
        //    Debug.Log("a");
        //}
        //else
        //{
        //    Debug.Log("b");
        //}
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Lobby);
        }
    }
    public void reee() // 배경음토글
    {

        if (toggle.GetComponent<Toggle>().isOn == true)
        {
            Debug.Log("배경음 켜짐");
            JSCSoundMng.instance.Fxsound();
            JSCSoundMng.instance.Bgmonsound();




        }
        else
        {

            Debug.Log("배경음 꺼짐");
            JSCSoundMng.instance.Fxsound();
            JSCSoundMng.instance.Bgmoffsound();
        }
     
    }
    public void ereee() // 효과음토글
    {

        if (ytoggle.GetComponent<Toggle>().isOn == true)
        {
     
            Debug.Log("효과음 켜짐");

            JSCSoundMng.instance.Fxonsound();
            JSCSoundMng.instance.Fxsound();
        }
        else
        {

            Debug.Log("효과음 꺼짐");
            JSCSoundMng.instance.Fxsound();
           
            JSCSoundMng.instance.Fxoffsound();
        }

    }

    public void GoToGame()
    {
        
        JSCSoundMng.instance.Fxsound();
       
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Lobby);
    }

    public void CloseOption()
    {
        JSCSoundMng.instance.Fxsound();
        Option.SetActive(false);
        button.SetActive(true);
    }
  

    public void GoToStore()
    {
        JSCSoundMng.instance.Fxsound();
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Shop);
    }

    public void OpenOption()
    {
        JSCSoundMng.instance.Fxsound();
        Option.SetActive(true);
        button.SetActive(false);
    }

    public void QuitGame()
    {
        JSCSoundMng.instance.Fxsound();
        Application.Quit();
    }
    public void OpenCredits()
    {
        JSCSoundMng.instance.Fxsound();
        Credits.SetActive(true);
    }

    public void QuitCredits()
    {
        JSCSoundMng.instance.Fxsound();
        Credits.SetActive(false);
    }
}
