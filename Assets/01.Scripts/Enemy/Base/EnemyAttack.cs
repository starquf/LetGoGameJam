using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackBase
{
    public bool isAttacking = false;

    public bool isWaitting = false;

    private float timer = 0f;
    [HideInInspector]
    public bool isFirst = true;
    
    public float attackDuration = 5f;
    public float waitAttackDuration = 10f;
    public float waitAttackDurationFirst = 5f;

    private Vector3 attackDir = Vector3.zero;

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
    public void Init()
    {
        isWaitting = true;
        timer = waitAttackDurationFirst;
        isAttacking = true;

        Vector3 dir = attackDir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookAngle = angle;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void LookDirection(Vector3 pos)
    {
        Vector3 dir = attackDir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookAngle = angle;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.AngleAxis(angle, Vector3.forward),
            Time.deltaTime * 5);

        if (lookAngle > 90f || lookAngle < -90f)
        {
            weaponRenderer.flipY = true;
        }
        else
        {
            weaponRenderer.flipY = false;
        }
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
                if (isWaitting)
                {
                    attackDir = targetPos.position - transform.position;
                    timer -= Time.deltaTime;
                    if (timer < 0f)
                    {
                        isWaitting = false;
                        timer = attackDuration;
                    }
                }
                else
                {
                    Weapon_BlueArchive blue = currentWeapon.GetComponent<Weapon_BlueArchive>();
                    if (blue != null)
                    {
                        currentWeapon.Shoot(attackDir);
                        yield return new WaitForSeconds(0.005f);
                    }
                    else
                    {
                        currentWeapon.Shoot(attackDir);
                        if (!currentWeapon.isNoShakeWeapon)
                        {
                            GameManager.Instance.vCamScript.Shake(currentWeapon.bulletData);
                            yield return weaponShootWait;
                        }
                    }
                    timer -= Time.deltaTime;
                    if(timer < 0f)
                    {
                        isWaitting = true;
                        timer = waitAttackDuration;
                    }
                }
            }
            else
            {
                weaponRenderer.color = Color.clear;
            }
        }
    }
}
