using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JSCmenutxt : MonoBehaviour {
    public Image fadeImage;


     void Start()
    {
        StartCoroutine(FadeOut());
       
    }

    IEnumerator FadeOut()
    {
        while (true)
        {

            fadeImage.color += new Color(0, 0, 0, -Time.deltaTime);

                yield return new WaitForSeconds(0);
            if (fadeImage.color.a < 0.01f)
              
                {
                    StopCoroutine(FadeOut());
                StartCoroutine(FadeIn());
                yield break;
                }
        }
    }
  
    IEnumerator FadeIn()
    {
        while (true)
        {
            fadeImage.color += new Color(0, 0, 0, +Time.deltaTime);

            yield return new WaitForSeconds(0);
            if (fadeImage.color.a > 0.98f)

            {
                StopCoroutine(FadeIn());
                StartCoroutine(FadeOut());
                yield break;
            }
        }
    }

}
