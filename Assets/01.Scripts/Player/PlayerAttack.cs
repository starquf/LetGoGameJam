using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    protected int currentBullet;

    public PlayerInput playerInput;
    public PlayerStat playerStat;

    private void Start()
    {
        EventManager<string>.AddEvent("OnUpgrade", SetPlayerStat);
    }

    public override void Init(Weapon baseWeapon)
    {
        base.Init(baseWeapon);
        baseWeapon.isPlayer = true;

        SetPlayerStat();
    }

    public override void ChangeWeapon(Weapon weapon)
    {
        base.ChangeWeapon(weapon);

        weapon.isPlayer = true;
        SetPlayerStat();
    }

    public void SetPlayerStat()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.bulletIron = playerStat.bulletIronclad;
    }

    public override void LookDirection(Vector3 pos)
    {
        base.LookDirection(pos);
    }

    private void Update()
    {
        if (Time.timeScale <= 0)
            return;

        if (playerInput.isDie)
        {
            gameObject.SetActive(false);
        }
        LookDirection(playerInput.mousePos);
    }

    protected override IEnumerator Shooting()
    {
        bool isShootOnce = true;

        while (true)
        {
            yield return null;

            if (Time.timeScale <= 0)
                continue;

            if (currentWeapon == null)
                continue;

            if (playerInput.isAttack)
            {
                if (currentWeapon.isAuto)
                {
                    Vector3 dir = playerInput.mousePos - transform.position;

                    currentWeapon.Shoot(dir);

                    //print("오또");
                    yield return weaponShootWait;
                }
                else if(isShootOnce)
                {
                    isShootOnce = false;

                    Vector3 dir = playerInput.mousePos - transform.position;

                    //print("원스");
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
