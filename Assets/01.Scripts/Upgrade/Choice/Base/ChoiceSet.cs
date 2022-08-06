using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceSet
{
    public ChoiceInfo merit;
    public ChoiceInfo demerit;

    public ChoiceSet(ChoiceInfo merit, ChoiceInfo demerit)
    {
        this.merit = merit;
        this.demerit = demerit;
    }
}
