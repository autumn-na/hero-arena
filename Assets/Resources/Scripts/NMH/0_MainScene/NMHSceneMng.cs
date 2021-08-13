using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSceneMng : MonoBehaviour
{
    public static NMHSceneMng instance;

    public enum SceneType
    {
        NULL = -99,
        Main = 0,
        Pick,
        InGame,
        Shop,
        Lobby
    }

    private SceneType tCurSceneType = SceneType.NULL;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////

    private void Awake()
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

    private void Start ()
    {
        InitSceneMng();
    }

    private void InitSceneMng()
    {
        tCurSceneType = (SceneType)Application.loadedLevel;
    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////

    public SceneType GetCurScene()
    {
        return tCurSceneType;
    }

    private void SetCurScene(SceneType _sn)
    {
        tCurSceneType = _sn;
    }

    public void LoadScene(SceneType _sn)
    {
        Application.LoadLevel((int)_sn);

        SetCurScene(_sn);
    }
}
