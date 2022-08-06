using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Hallucinations : ChoiceInfo
{

    public override void SetChoice()
    {
        PlayerAttack pa = GameManager.Instance.playerTrm.GetComponentInChildren<PlayerAttack>();
        int p = pa.GetHallucination();
        pa.hallucinationPercent = p == 0 ? 2 : p * 2;
        choiceData.level++;

        choiceData.des = $"총알에 맞은 적이 {pa.hallucinationPercent}% 확률로 랜덤 한 위치로 이동합니다.";
    }
}
