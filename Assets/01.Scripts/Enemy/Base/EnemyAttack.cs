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

    public override void Init(Weapon baseWeapon)
    {
        enemyShootWait = new WaitForSeconds(waitAttackDuration);
        base.Init(baseWeapon);
    }

    protected override IEnumerator Shooting()
    {
        bool isShootOnce = true;

        while (true)
        {
            yield return null;

            if (currentWeapon == null)
                continue;

            if (isAttacking)
            {
                float curTime = Time.time;
                if(attackDuration > curTime - shootStartTime)
                {
                    if (currentWeapon.isAuto)
                    {
                        currentWeapon.Shoot(transform.up);

                        yield return weaponShootWait;
                    }
                    else if (isShootOnce)
                    {
                        isShootOnce = false;

                        currentWeapon.Shoot(transform.up);

                        yield return weaponShootWait;
                    }
                }
                else
                {
                    yield return enemyShootWait;
                    shootStartTime = Time.time;
                }
            }
            else
            {
                isShootOnce = true;
            }
        }
    }
}
