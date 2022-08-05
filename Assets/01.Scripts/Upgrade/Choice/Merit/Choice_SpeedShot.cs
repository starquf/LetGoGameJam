using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_SpeedShot : ChoiceInfo
{
    public override void SetChoice()
    {
        print("히히 빨라졌다");

        uh.playerStat.atkRate += 10f;

        EventManager<string>.Invoke("OnUpgrade");

        choiceData.level++;
    }
}
