using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRushAttackState : EnemyState
{
    bool isTimer = true;
    bool isRushDelay = true;
    float rushDelayStartTime = 0;
    float rushStartTime = 0;
    Vector2 dir = Vector2.zero;

    public EnemyRushAttackState(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
              : base(obj, livingEntity, anim, targetTrm)
    {
        stateName = eState.ATTACK;
        //shootEff = obj.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        //myAnim.SetTrigger("isShooting");
        //shootEff.Play();
        base.Enter();
        myLivingEntity.AttackStart();
    }

    public override void Update()
    {
        if (myLivingEntity.IsDie)
        {
            nextState = new Dead(myObj, myLivingEntity, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else
        {
            AttackMove();
        }
    }

    private void AttackMove()
    {
        if(isTimer)
        {
            isTimer = false;
            rushDelayStartTime = Time.time;
            dir = (playerTrm.position - myObj.transform.position).normalized;
        }

        float curTime = Time.time;
        if (0.8f < curTime - rushDelayStartTime)
        {
            isRushDelay = false;
            rushStartTime = Time.time;
            
        }
        if(!isRushDelay)
        {
            if (2f > curTime - rushStartTime)
            {
                //Debug.Log("돌진중" + dir);
                if (CanMove())
                    myLivingEntity.rigid.velocity = dir * myLivingEntity.rushSpeed;
                myLivingEntity.sr.flipX = dir.x < 0;
                rushDelayStartTime = Time.time;
            }
            else
            {
                //Debug.Log("돌진멈춰!");
                if (CanMove())
                    myLivingEntity.rigid.velocity = Vector2.zero;
                isRushDelay = true;
                isTimer = true;

                // 공격범위 밖으로 나갈 시 다시 추격으로 전환
                if (!CanAttackPlayer())
                {
                    nextState = new EnemyPurseState(myObj, myLivingEntity, myAnim, playerTrm);
                    curEvent = eEvent.EXIT;
                }
                else if (PlayerAvoidInRange())
                {
                    nextState = new EnemyAvoidState(myObj, myLivingEntity, myAnim, playerTrm);
                    curEvent = eEvent.EXIT;
                }
            }
        }
    }

    public override void Exit()
    {
        if (CanMove())
            myLivingEntity.rigid.velocity = Vector2.zero;
        //myAnim.ResetTrigger("isShooting");
        //shootEff.Stop();
        myLivingEntity.AttackStop();
        base.Exit();
    }
}
