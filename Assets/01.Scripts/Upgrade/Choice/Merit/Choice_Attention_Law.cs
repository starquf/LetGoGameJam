using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Attention_Law : ChoiceInfo
{
    public override void SetChoice()
    {
        GameManager.Instance.isShowRange = true;
        choiceData.level++;
    }

    public override bool CanChoice()
    {
        return base.CanChoice() && !GameManager.Instance.isShowRange;
    }
}
