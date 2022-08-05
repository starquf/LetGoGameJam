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
        PURSUE, ATTACK, DEAD, AVOID
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
    public bool PlayerAvoidInRange()
    {
        if (myLivingEntity.enemyAttackType.Equals(enemyAttackType.MELEE))
            return false;
        float dist = Vector2.Distance(playerTrm.position, myObj.transform.position);
        if (dist < myLivingEntity.avoidRange)
        {
            return true;
        }

        return false;
    }

    public EnemyState GetAttackEnemyState(enemyAttackType enemyAttackType)
    {
        switch (enemyAttackType)
        {
            case enemyAttackType.MELEE:
                return new EnemyRushAttackState(myObj, myLivingEntity, myAnim, playerTrm);
            case enemyAttackType.RANGED:
                return new EnemyShootAttackState(myObj, myLivingEntity, myAnim, playerTrm);
            default:
                Debug.LogError("그딴 공격 타입은 없다");
                return null;
        }
    }

    public void LookPlayer()
    {
        float angle = Mathf.Atan2(playerTrm.position.y - myObj.transform.position.y, playerTrm.position.x - myObj.transform.position.x) * Mathf.Rad2Deg;
        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation,
                Quaternion.AngleAxis(angle - 90, Vector3.forward),
                Time.deltaTime * rotationSpeed);
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