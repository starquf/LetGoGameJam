using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_BulletProof_Suit : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.player.AddHP(true);
        uh.playerStat.player.AddHP(true);
    }
}
