using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJJCtrlCamara : MonoBehaviour
{
    public Camera MainCamera;

    private Transform tr;
    float fZoomtime;
    float fZoomsize;

    int i = 0;

    private float startSize;
    float cameraX;
    float cameraY;
    float asdf;
    float zoomout;
    string direction;
    float cameraDirect;
    bool check1 = false;
    bool check2 = false;
    bool check3 = false;
    bool check4 = false;

    bool testchet1 = false;
    bool testchet2 = false;
    bool testchet3 = false;
    bool testchet4 = false;
    Vector3 heroPosion;

    int num = 0;
    void Start()
    {
        startSize = MainCamera.orthographicSize;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
        }
        if (i == 1)
        {
            //    float dist = (10.0f - fZoomtime);
            //    float fracJourney = dist / asdf;
            MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, fZoomsize, (asdf / fZoomtime) / (asdf * 4));
            if (check1 == true)
            {
                if (cameraX > 0)
                {
                    MainCamera.transform.Translate(Vector3.right * 3.0f);
                }
            }
            if (check2 == true)
            {
                if (cameraX < 0)
                {
                    MainCamera.transform.Translate(Vector3.left * 3.0f);
                }
            }
            if (check3 == true)
            {
                if (cameraY > 0)
                {
                    MainCamera.transform.Translate(Vector3.up * 3.0f);
                }
            }
            if (check4 == true)
            {
                if (cameraY < 0)
                {
                    MainCamera.transform.Translate(Vector3.down * 3.0f);
                }
            }


            if (MainCamera.transform.position.x >= cameraX)
            {
                check1 = false;
            }
            if (MainCamera.transform.position.x <= cameraX)
            {
                check2 = false;
            }
            if (MainCamera.transform.position.y >= cameraY)
            {
                check3 = false;
            }
            if (MainCamera.transform.position.y <= cameraY)
            {
                check4 = false;
            }
            zoomout = asdf;
        }
        else if (i == 2)
        {
           
            //    float dist = (10.0f - fZoomtime);
            //    float fracJourney = dist / zoomout;
            MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, startSize, (zoomout / fZoomtime) / (zoomout * 4));
            if (testchet1 == true)
            {
                if (MainCamera.transform.position.x > 0)
                {
                    MainCamera.transform.Translate(Vector3.left * 3.0f);
                }
            }
            if (testchet2 == true)
            {
                if (MainCamera.transform.position.x < 0)
                {
                    MainCamera.transform.Translate(Vector3.right * 3.0f);
                }
            }
            if (testchet3 == true)
            {
                if (MainCamera.transform.position.y > 0)
                {
                    MainCamera.transform.Translate(Vector3.down * 3.0f);
                } 
            }
            if (testchet4 == true)
            {
                if (MainCamera.transform.position.y < 0)
                {
                    MainCamera.transform.Translate(Vector3.up * 3.0f);
                }
            }


            if (MainCamera.transform.position.x <= 0)
            {
                testchet1 = false;
            }
            if (MainCamera.transform.position.x >= 0)
            {
                testchet2 = false;
            }
            if (MainCamera.transform.position.x <= 0)
            {
                testchet3 = false;
            }
            if (MainCamera.transform.position.x >= 0)
            {
                testchet4 = false;
            }
        }
    }
    public void CameraZoom(string ppap)
    {
        string[] result = ppap.Split(',');
        fZoomtime = System.Convert.ToSingle(result[0]);
        fZoomsize = System.Convert.ToSingle(result[1]);
        cameraX = System.Convert.ToSingle(result[2]);
        cameraY = System.Convert.ToSingle(result[3]);
        asdf = fZoomsize - MainCamera.orthographicSize;
        
        if (asdf < 0)
        {
            asdf = asdf * -1;
        }
        if (i == 0 || i == 2)
        {
            check1 = true;
            check2 = true;
            check3 = true;
            check4 = true;
            i = 1;
        }
        else if (i == 1)
        {
            testchet1 = true;
            testchet2 = true;
            testchet3 = true;
            testchet4 = true;
            i = 2;
        }
    }
}