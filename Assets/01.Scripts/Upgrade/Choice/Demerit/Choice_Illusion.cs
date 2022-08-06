using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Illusion : ChoiceInfo
{
    public override void SetChoice()
    {
        PlayerAttack pa = uh.playerStat.GetComponentInChildren<PlayerAttack>();

        pa.maxillusionCount -= 5;

        pa.SetIllusion();

        choiceData.des = $"총을 {pa.maxillusionCount - 5f}번 획득할 때마다 무기가 M1911로 변경됩니다.";

        choiceData.level++;
    }
}
