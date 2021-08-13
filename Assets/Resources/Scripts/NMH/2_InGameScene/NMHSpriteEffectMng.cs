using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSpriteEffectMng : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public static NMHSpriteEffectMng instance;

    public enum SprEfType
    {
        GRID_ATTACK,
        GRID_MOVE
    }

    public GameObject objSprEfParent;
    public GameObject[] objSprEf; 

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 Unity 함수

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void CreateEffect(SprEfType _tType, Vector2 _vec2T)
    {
        GameObject objCloneSprEf = Instantiate(objSprEf[(int)_tType], _vec2T, Quaternion.identity, objSprEfParent.transform);
    }

    public void CreateEffect(string _strPath)
    {
        GameObject objCloneSprEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/" + _strPath), Resources.Load<GameObject>("Prefabs/NMH/Effects/" + _strPath).transform.position, Quaternion.identity, objSprEfParent.transform);
    }

    public void CreateEffect(string _strPath, Vector2 _vec2T)
    {
        GameObject objCloneSprEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/" + _strPath), _vec2T, Quaternion.identity, objSprEfParent.transform);
    }

    public void CreateEffect(string _strPath, Vector2 _vec2T, bool _bIsFlip)
    {
        GameObject objCloneSprEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/" + _strPath), _vec2T, Quaternion.identity, objSprEfParent.transform);

        if (_bIsFlip == true)
        {
            objCloneSprEf.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_bIsFlip == false)
        {
            objCloneSprEf.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    
    }

    public void CreateEffect(GameObject _obj, Vector2 _vec2T)
    {
        GameObject objCloneSprEf = Instantiate(_obj, _vec2T, Quaternion.identity, objSprEfParent.transform);
    }
}
