using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHUIEffectMng : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 변수

    public static NMHUIEffectMng instance;

    public GameObject objUIEfParent;

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 Unity 함수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 public 함수

    public void CreateEffect(string _strPath)
    {
        GameObject objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/" + _strPath), Resources.Load<GameObject>("Prefabs/NMH/Effects/" + _strPath).transform.position, Quaternion.identity, objUIEfParent.transform);
    }

    public void CreateEffect(string _strPath, Vector2 _vec2T)
    {
        GameObject objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/" + _strPath), _vec2T, Quaternion.identity, objUIEfParent.transform);
    }

    public void CreateEffect(string _strPath, Vector2 _vec2T, bool _bIsFlip)
    {
        GameObject objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/" + _strPath), _vec2T, Quaternion.identity, objUIEfParent.transform);

        if (_bIsFlip == true)
        {
            objCloneUIEf.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_bIsFlip == false)
        {
            objCloneUIEf.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////
    //아래는 effect 함수
    
    public void CreateCompareEffect()
    {
        GameObject objCloneUIEf_1 = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(-430, 250), Quaternion.identity, objUIEfParent.transform);
        GameObject objCloneUIEf_2 = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(430, 250), Quaternion.identity, objUIEfParent.transform);

        objCloneUIEf_1.GetComponent<NMHUIEfNotice>().SetToCompareSkill(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).GetHeroSelCard(NMHGameMng.instance.GetCurPhase()));
        objCloneUIEf_2.GetComponent<NMHUIEfNotice>().SetToCompareSkill(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetHeroSelCard(NMHGameMng.instance.GetCurPhase()));
    }

    public void CreateUseSkillEffect(NMHHero _nHero)
    {
        GameObject objCloneUIEf = new GameObject();

        switch (_nHero.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(-430, 250), Quaternion.identity, objUIEfParent.transform);
                break;

            case NMHUnit.UnitType.ENEMY:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(430, 250), Quaternion.identity, objUIEfParent.transform);
                break;
        }

        objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToUseSkill(NMHGameMng.instance.GetHeroByType(_nHero.GetUnitType()).GetHeroSelCard(NMHGameMng.instance.GetCurPhase()));
    }

    public void CreateDamageEffect(NMHHero _nHeroVictim)
    {
        GameObject objCloneUIEf = new GameObject();

        switch (_nHeroVictim.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(-430, 250), Quaternion.identity, objUIEfParent.transform);
                objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToDamage(NMHGameMng.instance.GetHeroByType(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetUnitType()).GetHeroSelCard(NMHGameMng.instance.GetCurPhase()).GetAD().ToString());
                break;

            case NMHUnit.UnitType.ENEMY:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(430, 250), Quaternion.identity, objUIEfParent.transform);
                objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToDamage(NMHGameMng.instance.GetHeroByType(NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).GetUnitType()).GetHeroSelCard(NMHGameMng.instance.GetCurPhase()).GetAD().ToString());
                break;
        }
    }

    public void CreateProhibitionEffect(NMHHero _nHeroTarget)
    {
        GameObject objCloneUIEf = new GameObject();

        switch (_nHeroTarget.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(-430, 250), Quaternion.identity, objUIEfParent.transform);
                objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToDamage("3");
                break;

            case NMHUnit.UnitType.ENEMY:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(430, 250), Quaternion.identity, objUIEfParent.transform);
                objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToDamage("3");
                break;
        }
    }

    public void CreateEnergyEffect(NMHHero _nHeroTarget)
    {
        GameObject objCloneUIEf = new GameObject();

        switch (_nHeroTarget.GetUnitType())
        {
            case NMHUnit.UnitType.PLAYER:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(-430, 250), Quaternion.identity, objUIEfParent.transform);
                objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToHealEnergy(10);
                break;

            case NMHUnit.UnitType.ENEMY:
                objCloneUIEf = Instantiate(Resources.Load<GameObject>("Prefabs/NMH/Effects/UIEffects/ui_ef_use_skill"), new Vector2(430, 250), Quaternion.identity, objUIEfParent.transform);
                objCloneUIEf.GetComponent<NMHUIEfNotice>().SetToHealEnergy(10);
                break;
        }
    }
}
