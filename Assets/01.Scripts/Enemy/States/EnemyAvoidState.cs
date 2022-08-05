using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoidState : EnemyState
{
    public EnemyAvoidState(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
              : base(obj, livingEntity, anim, targetTrm)
    {
        stateName = eState.AVOID;
        //shootEff = obj.GetComponent<AudioSource>();
    }
    public override void Enter()
    {
        //myAnim.SetTrigger("isShooting");
        //shootEff.Play();
        base.Enter();
    }
    public override void Update()
    {
        if (!PlayerAvoidInRange())
        {
            if (CanAttackPlayer())
            {
                nextState = GetAttackEnemyState(myLivingEntity.enemyAttackType);
                curEvent = eEvent.EXIT;
            }
            else
            {
                nextState = new EnemyPurseState(myObj, myLivingEntity, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
        }
        else
        {
            Vector2 dir = (myObj.transform.position - playerTrm.position).normalized;
            myLivingEntity.rigid.velocity = dir * myLivingEntity.attakMoveSpeed;

            myLivingEntity.sr.flipX = dir.x > 0;
        }
    }
    public override void Exit()
    {
        myLivingEntity.rigid.velocity = Vector2.zero;
        //myAnim.ResetTrigger("isShooting");
        //shootEff.Stop();
        base.Exit();
    }
}