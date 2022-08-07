using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_PacMan : BossState
{
    public float moveSpeed = 3f;

    private int isMove;

    private string PACMAN_PATH = "Prefabs/Enemy/PacMan";

    private Coroutine moveCor;

    private void Start()
    {
        isMove = Animator.StringToHash("IsMove");
    }

    public override void Play(Action onEndState = null)
    {
        StartCoroutine(Logic(onEndState));
    }

    private IEnumerator Logic(Action onEndState)
    {
        WaitForSeconds oneSecWait = new WaitForSeconds(1f);

        yield return oneSecWait;
        yield return oneSecWait;

        int rand = UnityEngine.Random.Range(0, 2);

        Vector2 startDir = Vector2.left;

        switch (rand)
        {
            case 0:
                startDir = Vector2.left;
                break;

            case 1:
                startDir = Vector2.up;
                break;
        }

        CreatePacMan(startDir, transform.position + Vector3.left * 2f);
        CreatePacMan(-startDir, transform.position + Vector3.right * 2f);

        yield return oneSecWait;
        yield return oneSecWait;
        yield return oneSecWait;

        moveCor = StartCoroutine(Move());

        for (int i = 0; i < 15; i++)
        {
            yield return oneSecWait;
        }

        StopMove();

        onEndState?.Invoke();
    }

    private void CreatePacMan(Vector2 startDir, Vector2 createPos)
    {
        PacmanScript pacMan = GameObjectPoolManager.Instance.GetGameObject(PACMAN_PATH, null).GetComponent<PacmanScript>();
        pacMan.transform.position = createPos;

        pacMan.SetPacMan(startDir);
    }

    private IEnumerator Move()
    {
        WaitForSeconds waitSec = new WaitForSeconds(3f);

        while (true)
        {
            Vector2 dir = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

            rb.velocity = dir * moveSpeed;

            anim.SetBool(isMove, true);

            yield return waitSec;
        }
    }

    private void StopMove()
    {
        StopCoroutine(moveCor);

        rb.velocity = Vector2.zero;
        anim.SetBool(isMove, false);
    }
}
