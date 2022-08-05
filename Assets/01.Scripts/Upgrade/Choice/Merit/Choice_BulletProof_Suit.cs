using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_BulletProof_Suit : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.GetComponent<Player>().AddHP(true);
        uh.playerStat.GetComponent<Player>().AddHP(true);
    }
}
