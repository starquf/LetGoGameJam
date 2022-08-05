using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Shackles : ChoiceInfo
{
    public override void SetChoice()
    {
        print("움직임 봉쇄");
        uh.playerStat.moveSpeed -= 0.5f;

        choiceData.level++;
    }
}
