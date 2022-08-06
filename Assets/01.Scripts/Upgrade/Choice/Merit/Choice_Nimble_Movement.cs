using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Nimble_Movement : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.parryingCoolDown += 0.1f;
        choiceData.level++;
    }
}
