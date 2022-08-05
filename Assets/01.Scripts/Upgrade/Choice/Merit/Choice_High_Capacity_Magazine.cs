using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_High_Capacity_Magazine : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.bulletCapacity += 10f;

        choiceData.level++;
    }
}
