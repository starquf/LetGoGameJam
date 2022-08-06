using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPurseState : EnemyState
{
    public EnemyPurseState(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
              : base(obj, livingEntity, anim, targetTrm)
    {
        stateName = eState.PURSUE;
    }

    public override void Enter()
    {
        //myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        if (GameManager.Instance.timeScale <= 0f)
        {
            myLivingEntity.rigid.velocity = Vector2.zero;
            return;
        }

        if (myLivingEntity.IsDie)
        {
            nextState = new Dead(myObj, myLivingEntity, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else
        {
            // 추적 로직
            //LookPlayer();
            PurseMove();
            if (PlayerAvoidInRange())
            {
                nextState = new EnemyAvoidState(myObj, myLivingEntity, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
            else if (CanAttackPlayer())
            {
                nextState = GetAttackEnemyState(myLivingEntity.enemyAttackType);
                curEvent = eEvent.EXIT;
            }
        }
    }

    private void PurseMove()
    {
        Vector2 dir = (playerTrm.position - myObj.transform.position).normalized;
        myLivingEntity.rigid.velocity = dir * myLivingEntity.speed * GameManager.Instance.timeScale;
        myLivingEntity.sr.flipX = dir.x < 0;

        //Debug.LogWarning("움직임 구현좀");
    }

    public override void Exit()
    {
        myLivingEntity.rigid.velocity = Vector2.zero;
        //myAnim.ResetTrigger("isRunning");
        base.Exit();
    }
}