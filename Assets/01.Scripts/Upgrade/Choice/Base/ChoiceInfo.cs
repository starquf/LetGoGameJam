using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChoiceInfo : MonoBehaviour
{
    public ChoiceData choiceData;

    public int maxLevel;
    public bool isInfinityLevel = false;

    [HideInInspector]
    public UpgradeHandler uh;

    public virtual bool CanChoice()
    {
        return (maxLevel > choiceData.level) || isInfinityLevel;
    }

    public abstract void SetChoice();
}
