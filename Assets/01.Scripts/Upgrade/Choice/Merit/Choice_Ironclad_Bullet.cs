using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Ironclad_Bullet : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.bulletIronclad += 1;

        choiceData.level++;
    }
}
