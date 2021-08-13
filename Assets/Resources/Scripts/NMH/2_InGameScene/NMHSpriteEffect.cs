using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHSpriteEffect : MonoBehaviour
{
    public void DeleteSpriteEffect()
    {
        Destroy(this.gameObject);
    }

    public void RunSprEffect(string _strName)
    {
        NMHSpriteEffectMng.instance.CreateEffect(_strName, new Vector2(this.transform.position.x, this.transform.position.y));
    }

    public void RunSoundByClip(AudioClip ac)
    {
        JSCSoundMng.instance.RunSoundByClip(ac);
    }

    public void RunSprEffectByObj(GameObject _obj)
    {
        NMHSpriteEffectMng.instance.CreateEffect(_obj, _obj.transform.position);
    }
}
