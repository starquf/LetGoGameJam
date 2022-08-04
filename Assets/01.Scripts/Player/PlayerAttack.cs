using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
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
        bool isShoot = false;

        while (true)
        {
            yield return null;

            if (currentWeapon == null)
                continue;

            if (playerInput.isAttack || autoFire)
            {
                print("마우스 누름");

                currentWeapon.Shoot(Input.mousePosition);

                if (currentWeapon.isAuto)
                {
                    isShoot = true;
                }
                else
                {
                    autoFire = false;
                }

                yield return weaponShootWait;
            }

            if (playerInput.isAttack)
            {
                print("마우스 떔");
                autoFire = false;
            }
        }
    }
}
