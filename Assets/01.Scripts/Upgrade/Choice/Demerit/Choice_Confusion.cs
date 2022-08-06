using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Confusion : ChoiceInfo
{
    [SerializeField] private float confusion = 40f;

    public override void SetChoice()
    {
        uh.playerStat.GetComponent<PlayerInput>().SetConfusion(confusion);

        confusion -= 8f;
        choiceData.level++;

        choiceData.des = $"{confusion}초마다 1초 동안 이동 방향이 반대가 됩니다.";
    }
}
