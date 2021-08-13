using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHMeteo : MonoBehaviour
{
    // Use this for initialization

    Vector3 vec3;

    private void Awake()
    {
        vec3 = transform.localPosition;
        Debug.Log(vec3);

        transform.localPosition = transform.localPosition + new Vector3(-500, 500, 0);
    }

    void Start ()
    {

    }
	
	void Update ()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, vec3, 1750.0f * Time.deltaTime); 
	}
}
