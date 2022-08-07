using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossState_Sonic : BossState
{
    private Transform playerTrans = null;
    private string SONIC_PATH = "Prefabs/Enemy/Sonic";

    public float moveSpeed = 3f;

    private int isMove;

    private Coroutine moveCor;

    private void Start()
    {
        playerTrans = GameManager.Instance.playerTrm;

        isMove = Animator.StringToHash("IsMove");
    }

    public override void Play(Action onEndState = null)
    {
        StartCoroutine(Logic(onEndState));
    }

    private IEnumerator Logic(Action onEndState)
    {
        WaitForSeconds oneSecWait = new WaitForSeconds(1f);

        moveCor = StartCoroutine(MoveToPlayer());

        yield return oneSecWait;

        Vector2 playerPos = GameManager.Instance.playerTrm.position;
        CreateSonic(playerPos + Vector2.right * 6f + Vector2.up * Random.Range(-6f, 6f));
        CreateSonic(playerPos + Vector2.left * 6f + Vector2.up * Random.Range(-6f, 6f));

        yield return oneSecWait;
        yield return oneSecWait;

        playerPos = GameManager.Instance.playerTrm.position;
        CreateSonic(playerPos + Vector2.right * Random.Range(-6f, 7f) + Vector2.up * 6f);
        CreateSonic(playerPos + Vector2.left * Random.Range(-6f, 7f) + Vector2.down * 6f);

        yield return oneSecWait;
        yield return oneSecWait;

        playerPos = GameManager.Instance.playerTrm.position;
        CreateSonic(playerPos + Vector2.right * 7f + Vector2.up * Random.Range(-7f, 7f));
        CreateSonic(playerPos + Vector2.left * 7f + Vector2.up * Random.Range(-7f, 7f));

        yield return oneSecWait;
        yield return oneSecWait;

        StopMove();

        onEndState?.Invoke();
    }

    private IEnumerator MoveToPlayer()
    {
        while (true)
        {
            Vector2 dir = (playerTrans.position - transform.position).normalized;

            rb.velocity = dir * moveSpeed;

            anim.SetBool(isMove, true);

            yield return null;
        }
    }

    private void CreateSonic(Vector2 createPos)
    {
        SonicScript sonic = GameObjectPoolManager.Instance.GetGameObject(SONIC_PATH, null).GetComponent<SonicScript>();
        sonic.transform.position = createPos;

        sonic.Play();
    }

    private void StopMove()
    {
        StopCoroutine(moveCor);

        rb.velocity = Vector2.zero;
        anim.SetBool(isMove, false);
    }
}
