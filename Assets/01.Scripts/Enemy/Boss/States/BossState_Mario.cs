using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Mario : BossState
{
    private string MARIO_PATH = "Prefabs/Enemy/Mario";

    public override void Play(Action onEndState = null)
    {
        StartCoroutine(Logic(onEndState));
    }

    private IEnumerator Logic(Action onEndState)
    {
        WaitForSeconds oneSecWait = new WaitForSeconds(1f);

        yield return oneSecWait;
        yield return oneSecWait;

        CreateMario(transform.position + Vector3.down * 5f);

        yield return oneSecWait;
        yield return oneSecWait;
        yield return oneSecWait;
        yield return oneSecWait;
        yield return oneSecWait;

        onEndState?.Invoke();
    }

    private void CreateMario(Vector2 createPos)
    {
        MarioScript mario = GameObjectPoolManager.Instance.GetGameObject(MARIO_PATH, null).GetComponent<MarioScript>();
        mario.transform.position = createPos;

        mario.Play();
    }
}
