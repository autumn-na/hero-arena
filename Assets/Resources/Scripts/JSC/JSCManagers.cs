using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSCManagers : MonoBehaviour
{
    public static JSCManagers instance;

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
}
