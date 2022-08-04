using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackBase
{
    public bool isAttacking = false;

    [HideInInspector]
    public float shootStartTime = 0f;
    
    public float attackDuration = 5f;
    public float waitAttackDuration = 10f;

    private WaitForSeconds enemyShootWait = new WaitForSeconds(1f);
    [HideInInspector]public Transform targetPos = null;

    private void Update()
    {
        if(targetPos != null)
            LookDirection(targetPos.position);
    }

    public override void Init(Weapon baseWeapon)
    {
        enemyShootWait = new WaitForSeconds(waitAttackDuration);
        base.Init(baseWeapon);
    }

    protected override IEnumerator Shooting()
    {

        while (true)
        {
            yield return null;

            if (currentWeapon == null)
                continue;

            if (isAttacking)
            {
                weaponRenderer.color = Color.white;
                float curTime = Time.time;
                if(attackDuration > curTime - shootStartTime)
                {
                    Vector3 dir = targetPos.position - transform.position;
                    currentWeapon.Shoot(dir);

                    yield return weaponShootWait;
                }
                else
                {
                    yield return enemyShootWait;
                    shootStartTime = Time.time;
                }
            }
            else
            {
                weaponRenderer.color = Color.clear;
            }
        }
    }
}
