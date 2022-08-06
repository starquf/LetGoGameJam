using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Weakening : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.collectionRate += 10f;
    }
}
