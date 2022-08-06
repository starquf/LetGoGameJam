using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Fetigue : ChoiceInfo
{
    public override void SetChoice()
    {
        GameManager.Instance.playerTrm.GetComponentInChildren<PlayerParrying>().SetStaticParryingCool(true);
        choiceData.level++;
    }
}
