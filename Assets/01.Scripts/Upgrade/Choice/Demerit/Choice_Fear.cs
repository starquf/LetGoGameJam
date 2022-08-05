using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Fear : ChoiceInfo
{
    private readonly string INVINCIBLE_ENEMY = "Prefabs/Enemy/Ghost_Invincible";

    public override void SetChoice()
    {
        StageHandler sh = GameManager.Instance.stageHandler;

        GameObject enemy = GameObjectPoolManager.Instance.GetGameObject(INVINCIBLE_ENEMY, sh.transform);
        enemy.transform.position = sh.GetRandomSpawnPosition();

        choiceData.level++;
    }
}
