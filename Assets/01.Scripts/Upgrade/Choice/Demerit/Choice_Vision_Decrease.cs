using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Vision_Decrease : ChoiceInfo
{
    private float orgarnizeSize = 6.5f;
    public override void SetChoice()
    {
        GameManager.Instance.cmPerlinObject.m_Lens.OrthographicSize = orgarnizeSize;
        choiceData.level++;
    }
}
