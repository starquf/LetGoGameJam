using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttackState : EnemyState
{
    float moveStartTime = 0;
    Vector2 dir = Vector2.zero;

    public EnemyShootAttackState(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
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
            // 내 각도를 플레이어 방향으로 틀어줘야 함(feat. 스무스하게 돌려줘)
            //LookPlayer();
            AttackMove();
        }
    }

    private void AttackMove()
    {
        float curTime = Time.time;
        if (!(Random.Range(1f, 3f) > curTime - moveStartTime))
        {
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
            else
            {
                if (Random.Range(0, 5) > 0)
                {
                    dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                    //Debug.Log(dir + "tlqkf");
                }
                else
                {
                    dir = Vector2.zero;
                    //Debug.Log(dir + "tl");
                }
            }
            moveStartTime = Time.time;
        }
        myLivingEntity.rigid.velocity = dir * myLivingEntity.speed;

        myLivingEntity.sr.flipX = playerTrm.position.x - myObj.transform.position.x < 0;
        //Debug.LogWarning("움직임 구현좀");
    }

    public override void Exit()
    {
        myLivingEntity.rigid.velocity = Vector2.zero;
        //myAnim.ResetTrigger("isShooting");
        //shootEff.Stop();
        myLivingEntity.AttackStop();
        base.Exit();
    }
}