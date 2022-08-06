using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Illusion : ChoiceInfo
{
    public override void SetChoice()
    {
        PlayerAttack pa = uh.playerStat.GetComponentInChildren<PlayerAttack>();

        pa.SetIllusion();

        pa.maxillusionCount -= 10;

        choiceData.level++;
    }
}
