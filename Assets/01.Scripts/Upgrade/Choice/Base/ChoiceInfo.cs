using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChoiceInfo : MonoBehaviour
{
    public ChoiceData choiceData;

    public int maxLevel;

    public virtual bool CanChoice()
    {
        return maxLevel > choiceData.level;
    }

    public abstract void SetChoice();
}
