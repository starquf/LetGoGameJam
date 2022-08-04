using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    public enum eState  // 가질 수 있는 상태 나열
    {
        IDLE, PATROL, PURSUE, ATTACK, DEAD, RUNAWAY
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

    float attackDist = 7.0f;

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
        if (dist < attackDist)
        {
            return true;
        }

        return false;
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
        myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        // 추적 로직
        Vector2 dir = (playerTrm.position - myObj.transform.position).normalized;

        if (CanAttackPlayer())
        {
            nextState = new Attack(myObj, myLivingEntity, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }
}

public class Attack : EnemyState
{
    float rotationSpeed = 2.0f;
    //AudioSource shootEff;

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
    }

    public override void Update()
    {
        // 내 각도를 플레이어 방향으로 틀어줘야 함(feat. 스무스하게 돌려줘)
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);
        dir.y = 0;

        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation,
                                                    Quaternion.LookRotation(dir),
                                                    Time.deltaTime * rotationSpeed);

        // 공격범위 밖으로 나갈 시 다시 추격으로 전환
        if (!CanAttackPlayer())
        {
            nextState = new PursueAndAttack(myObj, myLivingEntity, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isShooting");
        //shootEff.Stop();
        base.Exit();
    }
}