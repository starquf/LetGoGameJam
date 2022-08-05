using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Best_Attack : ChoiceInfo
{
    public PlayerParrying playerParring;

    public override void SetChoice()
    {
        playerParring.isReflectMode = true;
        choiceData.level++;
    }
}
