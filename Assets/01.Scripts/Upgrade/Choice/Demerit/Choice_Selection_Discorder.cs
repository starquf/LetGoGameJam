using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Selection_Discorder : ChoiceInfo
{
    public override void SetChoice()
    {
        GameManager.Instance.upgradeUIHandler.showPanelCount--;
    }

    public override bool CanChoice()
    {
        return base.CanChoice() && GameManager.Instance.upgradeUIHandler.showPanelCount > 1;
    }
}
