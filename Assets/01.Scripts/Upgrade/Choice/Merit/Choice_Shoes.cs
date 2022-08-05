using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Shoes : ChoiceInfo
{
    public override void SetChoice()
    {
        print("움직임 빨라짐");

        uh.playerStat.moveSpeed += 1f;

        choiceData.level++;
    }
}
