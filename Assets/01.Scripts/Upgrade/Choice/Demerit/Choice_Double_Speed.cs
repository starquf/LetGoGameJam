using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Double_Speed : ChoiceInfo
{
    public override void SetChoice()
    {
        GameManager.Instance.doubleSpeed += 0.05f;

        Time.timeScale = 1f + (1f * GameManager.Instance.doubleSpeed);

        choiceData.level++;
    }
}
