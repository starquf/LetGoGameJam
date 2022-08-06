using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Medicine : ChoiceInfo
{
    public override void SetChoice()
    {
        uh.playerStat.GetComponent<PlayerInput>().ClearConfusion();
        transform.parent.parent.Find("Demerit").Find("Choice_Confusion").GetComponent<ChoiceInfo>().choiceData.level--;
    }
}
