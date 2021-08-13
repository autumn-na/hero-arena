//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    public GameObject[] MainCamera;

//    bool bZoom = true;
//    public GameObject navfo;
//    public GameObject a;
//    bool bCoolTimeCheck = true;
//    float fSpeed;
//    float fSize;
//    int nTime;
//    string sDirection;


//    void Start()
//    {

//    }

//    void Update()
//    {
//    }
//    public void testButton1(string testtest)
//    {
//        string[] result = testtest.Split(',');
//        nTime = System.Convert.ToInt32(result[0]);
//        fSpeed = System.Convert.ToSingle(result[1]);
//        fSize = System.Convert.ToSingle(result[2]);
//        sDirection = result[3];
//        StartCoroutine("CameraZoom");
//        Invoke("StopCamera", nTime);
//    }
//    IEnumerator CameraZoom()
//    {
//        Debug.Log("CameraZoomIn");
//        while (true)
//        {
//            if (bZoom == true)
//            {
//                a.transform.localScale += new Vector3(fSize, fSize, fSize);
//                navfo.transform.localScale += new Vector3(fSize, fSize, fSize);
//                if (sDirection == "right")
//                {
//                    for(int i = 1; i < 3; i++)
//                    {
//                        navfo.transform.Translate(Vector3.right * 10);
//                        MainCamera[i].transform.Translate(Vector3.right * 10);
//                    }
//                }
//                //if (sDirection == "left")
//                //    MainCamera.transform.Translate(Vector3.left * fSpeed);
//                //if (sDirection == "up")
//                //    MainCamera.transform.Translate(Vector3.up * 30);
//                //if (sDirection == "down")
//                //    MainCamera.transform.Translate(Vector3.down * fSpeed);
//            }
//            else
//            {
//                if (sDirection == "right")
//                {
//                    for (int i = 1; i < 3; i++)
//                    {
//                        navfo.transform.Translate(Vector3.left * 10);
//                        MainCamera[i].transform.Translate(Vector3.left * 10);
//                    }
//                }
//                a.transform.localScale -= new Vector3(fSize, fSize, fSize);
//                navfo.transform.localScale -= new Vector3(fSize, fSize, fSize);           

//                if (a.transform.lossyScale.x != 1)
//                {
//                    Invoke("WhenNot", 0f);
//                }
//            }
//            yield return 0;
//            // yield return new WaitForSecondsRealtime(0.1f);
//        }
//    }
//    void StopCamera()
//    {
//        StopCoroutine("CameraZoom");
//        if (bZoom == true)
//        {
//            bZoom = false;
//        }
//        else
//        {
//            bZoom = true;
//        }
//    }
//    void WhenNot()
//    {
//        if(bZoom == true)
//        a.transform.localScale = new Vector3(1, 1, 1);
//    }
//}
