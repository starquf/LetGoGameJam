using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class EnemyState
{
    public enum eState  // 가질 수 있는 상태 나열
    {
        PURSUE, ATTACK, DEAD
    };

    public enum eEvent  // 이벤트 나열
    {
        ENTER, UPDATE, EXIT
    };

    public eState stateName;

    protected eEvent curEvent;

    protected GameObject myObj;
    protected Enemy myLivingEntity;
    protected Animator myAnim;
    protected Transform playerTrm;  // 타겟팅 할 플레이어의 트랜스폼

    protected EnemyState nextState;  // 다음 상태를 나타냄

    float rotationSpeed = 2.0f;

    public EnemyState(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
    {
        myObj = obj;
        myLivingEntity = livingEntity;
        myAnim = anim;
        playerTrm = targetTrm;

        // 최초 이벤트를 엔터로
        curEvent = eEvent.ENTER;
    }

    public virtual void Enter() { curEvent = eEvent.UPDATE; }
    public virtual void Update() { curEvent = eEvent.UPDATE; }
    public virtual void Exit() { curEvent = eEvent.EXIT; }

    public EnemyState Process()
    {
        if (curEvent == eEvent.ENTER) Enter();
        if (curEvent == eEvent.UPDATE) Update();
        if (curEvent == eEvent.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    // 공격 범위 체크 로직
    public bool CanAttackPlayer()
    {
        float dist = Vector2.Distance(playerTrm.position, myObj.transform.position);
        if (dist < myLivingEntity.attackRange)
        {
            return true;
        }

        return false;
    }

    public void LookPlayer()
    {
        float angle = Mathf.Atan2(playerTrm.position.y - myObj.transform.position.y, playerTrm.position.x - myObj.transform.position.x) * Mathf.Rad2Deg;
        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation,
                Quaternion.AngleAxis(angle - 90, Vector3.forward),
                Time.deltaTime * rotationSpeed);
    }
}

public class PursueAndAttack : EnemyState
{
    public PursueAndAttack(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
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
        if(myLivingEntity.IsDie)
        {
            nextState = new Dead(myObj, myLivingEntity, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else
        {
            // 추적 로직
            //LookPlayer();
            PurseMove();

            if (CanAttackPlayer())
            {
                nextState = new Attack(myObj, myLivingEntity, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
        }
    }

    private void PurseMove()
    {
        Vector2 dir = (playerTrm.position - myObj.transform.position).normalized;
        myLivingEntity.rigid.velocity = dir * myLivingEntity.speed;
        myLivingEntity.sr.flipX = playerTrm.position.x - myObj.transform.position.x < 0;

        //Debug.LogWarning("움직임 구현좀");
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isRunning");
        base.Exit();
    }
}

public class Attack : EnemyState
{
    float moveStartTime = 0;
    Vector2 dir = Vector2.zero;

    public Attack(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
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
                nextState = new PursueAndAttack(myObj, myLivingEntity, myAnim, playerTrm);
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
        myLivingEntity.rigid.velocity = dir * myLivingEntity.attakMoveSpeed;

        myLivingEntity.sr.flipX = playerTrm.position.x - myObj.transform.position.x < 0;
        //Debug.LogWarning("움직임 구현좀");
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isShooting");
        //shootEff.Stop();
        myLivingEntity.AttackStop();
        base.Exit();
    }
}

public class Dead : EnemyState
{
    public Dead(GameObject obj, Enemy livingEntity, Animator anim, Transform targetTrm)
              : base(obj, livingEntity, anim, targetTrm)
    {
        stateName = eState.DEAD;
        //shootEff = obj.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        //myAnim.SetTrigger("isShooting");
        //shootEff.Play();
        base.Enter();
        curEvent = eEvent.EXIT;
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        //myAnim.ResetTrigger("isShooting");
        //shootEff.Stop();
        base.Exit();
        myLivingEntity.SetDisable();
    }
}