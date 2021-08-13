using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Herowindow : MonoBehaviour {

   
   
  public GameObject Player_Bar;
    public GameObject Ai_Bar;
    public GameObject effect;

    void Start()
    {


        StartCoroutine(Player_effect());



    }


    IEnumerator Player_effect()
    {
        int count = 0;
        while (count < 12)
        {
            effect.transform.localScale = new Vector3(6, 6, 6);
            yield return new WaitForSeconds(0.5f);
            effect.transform.localScale = new Vector3(2, 2, 2);
            yield return new WaitForSeconds(0.5f);
        }
        count++;
    }
    

   


    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < 12)
        {

            Player_Bar.SetActive(true);
            yield return new WaitForSeconds(0.1f);

            Player_Bar.SetActive(false);
            
            yield return new WaitForSeconds(0.1f);
     


            count++;
        }
    }

    IEnumerator DEDD()
    {
        int count = 0;
        while (count < 12)
        {


            Ai_Bar.SetActive(true);

            yield return new WaitForSeconds(0.1f);

            Ai_Bar.SetActive(false);

            

            yield return new WaitForSeconds(0.1f);


            count++;
        }
    }





    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log("AAAAAAAAAAAA");
            StartCoroutine(ShowReady());
            StartCoroutine(DEDD());
        }
       
    }

  

}
