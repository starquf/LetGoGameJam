using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChoiceInfo : MonoBehaviour
{
    public ChoiceData choiceData;

    public int maxLevel;

    [HideInInspector]
    public UpgradeHandler uh;

    public virtual bool CanChoice()
    {
        return maxLevel > choiceData.level;
    }

    public abstract void SetChoice();
}
