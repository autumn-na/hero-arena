using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMHPlayerStats : MonoBehaviour
{
    bool[] bIsRunning;

    private void Start()
    {
        bIsRunning = new bool[3];

        bIsRunning[0] = false;
        bIsRunning[1] = false;
        bIsRunning[2] = false;
    }

    void Update ()
    {
		if( (bIsRunning[0] == false) && (NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).GetHP() < 50) )
        {
            bIsRunning[0] = true;
            GetComponent<Animation>().Play("anim_player_1_warning_hp");
        }

        if ((bIsRunning[1] == false) && (NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetHP() < 50))
        {
            bIsRunning[1] = true;
            GetComponent<Animation>().Play("anim_player_2_warning_hp");
        }

        if ((bIsRunning[2] == false) && (NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.ENEMY).GetHP() < 50) && (NMHGameMng.instance.GetHeroByType(NMHUnit.UnitType.PLAYER).GetHP() < 50) )
        {
            bIsRunning[2] = true;
            GetComponent<Animation>().Play("anim_player_all_warning_hp");
        }

    }
}
