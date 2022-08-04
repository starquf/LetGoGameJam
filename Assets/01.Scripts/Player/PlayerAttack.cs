using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    protected int currentBullet;

    public PlayerInput playerInput;
    private SpriteRenderer weaponRenderer;

    public override void Init(Weapon baseWeapon)
    {
        base.Init(baseWeapon);
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        baseWeapon.isPlayer = true;
    }

    public override void ChangeWeapon(Weapon weapon)
    {
        base.ChangeWeapon(weapon);

        weapon.isPlayer = true;
    }

    public override void LookDirection(Vector3 pos)
    {
        base.LookDirection(pos);
        if(lookAngle > 90f || lookAngle < -90f)
        {
            weaponRenderer.flipY = true;
        }
        else
        {
            weaponRenderer.flipY = false;
        }
        print(lookAngle);
    }

    private void Update()
    {
        LookDirection(playerInput.mousePos);
    }

    protected override IEnumerator Shooting()
    {
        bool isShootOnce = true;

        while (true)
        {
            yield return null;

            if (currentWeapon == null)
                continue;

            if (playerInput.isAttack)
            {
                if (currentWeapon.isAuto)
                {
                    Vector3 dir = playerInput.mousePos - transform.position;

                    currentWeapon.Shoot(dir);

                    yield return weaponShootWait;
                }
                else if(isShootOnce)
                {
                    isShootOnce = false;

                    Vector3 dir = playerInput.mousePos - transform.position;

                    currentWeapon.Shoot(dir);

                    yield return weaponShootWait;
                }
            }
            else
            {
                isShootOnce = true;
            }
        }
    }
}
