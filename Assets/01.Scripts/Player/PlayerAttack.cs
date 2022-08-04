using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    protected int currentBullet;

    private PlayerInput playerInput;

    public override void Init(Weapon baseWeapon)
    {
        base.Init(baseWeapon);

        baseWeapon.isPlayer = true;
    }

    public override void ChangeWeapon(Weapon weapon)
    {
        base.ChangeWeapon(weapon);

        weapon.isPlayer = true;
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
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
                    currentWeapon.Shoot(playerInput.mousePos);

                    yield return weaponShootWait;
                }
                else if(isShootOnce)
                {
                    isShootOnce = false;

                    currentWeapon.Shoot(playerInput.mousePos);

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
