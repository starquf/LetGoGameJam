using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;


public class PlayerAttack : AttackBase
{
    protected int currentBullet;

    public PlayerInput playerInput;
    public PlayerStat playerStat;

    private readonly string BASE_WEAPON = "Prefabs/Weapons/Weapon_M1911";
    private readonly string USED_EFFECT = "Prefabs/Effect/UsedGun";

    private void Start()
    {
        EventManager<string>.AddEvent("OnUpgrade", SetPlayerStat);
    }

    public override void Init(Weapon baseWeapon)
    {
        base.Init(baseWeapon);
        baseWeapon.isPlayer = true;
        baseWeapon.sr.color = Color.white;

        currentBullet = Mathf.RoundToInt(baseWeapon.maxBullet * (1f + playerStat.bulletCapacity));

        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Ammo, currentBullet.ToString());

        SetPlayerStat();
    }

    public override void ChangeWeapon(Weapon weapon)
    {
        CreateUsedEffect(currentWeapon);

        base.ChangeWeapon(weapon);

        weapon.isPlayer = true;
        currentBullet = Mathf.RoundToInt(weapon.maxBullet * (1f + playerStat.bulletCapacity));
        weapon.sr.color = Color.white;

        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Ammo, currentBullet.ToString());

        SetPlayerStat();
    }

    private void CreateUsedEffect(Weapon weapon)
    {
        UsedGun effect = GameObjectPoolManager.Instance.GetGameObject(USED_EFFECT, null).GetComponent<UsedGun>();

        effect.transform.localScale = weapon.transform.localScale;
        effect.SetSprite(weapon.sr.sprite);
        effect.ShowEffect(weapon.transform.position);
    }

    public void SetPlayerStat()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.bulletIron = playerStat.bulletIronclad;

        float curRate = currentWeapon.fireRate;
        print(curRate + ", " + (curRate - ((playerStat.atkRate * curRate) / 100)));
        weaponShootWait = new WaitForSeconds(curRate - ((playerStat.atkRate * curRate) / 100));
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

                    Weapon_BlueArchive blue = currentWeapon.GetComponent<Weapon_BlueArchive>();
                    if (blue != null)
                    {
                        currentWeapon.Shoot(dir);
                        if (!currentWeapon.isNoShakeWeapon)
                        {
                            GameManager.Instance.vCamScript.Shake(currentWeapon.bulletData);
                        }
                        yield return new WaitForSeconds(0.005f);
                    }
                    else
                    {
                        currentWeapon.Shoot(dir);
                        if (!currentWeapon.isNoShakeWeapon)
                        {
                            GameManager.Instance.vCamScript.Shake(currentWeapon.bulletData);
                            yield return weaponShootWait;
                        }
                    }

                    UseBullet();

                    //print("오또");
                    
                }
                else if(isShootOnce)
                {
                    isShootOnce = false;

                    Vector3 dir = playerInput.mousePos - transform.position;

                    //print("원스");
                    currentWeapon.Shoot(dir);

                    if (!currentWeapon.isNoShakeWeapon)
                    {
                        GameManager.Instance.vCamScript.Shake(currentWeapon.bulletData);
                    }

                    UseBullet();

                    yield return weaponShootWait;
                }
            }
            else
            {
                isShootOnce = true;
            }
        }
    }

    protected void UseBullet()
    {
        if (currentWeapon.isInfiniteBullet)
        {
            GameManager.Instance.inGameUIHandler.SendData(UIDataType.Ammo, "inf");
            return;
        }

        currentBullet--;

        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Ammo, currentBullet.ToString());

        // 총알 다 쓰면
        if (currentBullet <= 0)
        {
            GameManager.Instance.soundHandler.Play("DropWeapon");

            Weapon weapon = GameObjectPoolManager.Instance.GetGameObject(BASE_WEAPON, transform).GetComponent<Weapon>();
            ChangeWeapon(weapon);
        }
    }
}
