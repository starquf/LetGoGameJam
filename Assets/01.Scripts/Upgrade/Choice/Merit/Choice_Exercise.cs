using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Exercise : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.player.AddMaxHp();

        choiceData.level++;
    }
}
