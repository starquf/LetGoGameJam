using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;


public class PlayerAttack : AttackBase
{
    protected int currentBullet;

    public PlayerInput playerInput;

    private Tween camTween = null;

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

    public override void LookDirection(Vector3 pos)
    {
        base.LookDirection(pos);
    }

    private void Update()
    {
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

            if (currentWeapon == null)
                continue;

            if (playerInput.isAttack)
            {
                if (currentWeapon.isAuto)
                {
                    Vector3 dir = playerInput.mousePos - transform.position;

                    currentWeapon.Shoot(dir);
                    Shake(currentWeapon.bulletData);

                    //print("오또");
                    yield return weaponShootWait;
                }
                else if(isShootOnce)
                {
                    isShootOnce = false;

                    Vector3 dir = playerInput.mousePos - transform.position;

                    //print("원스");
                    currentWeapon.Shoot(dir);
                    Shake(currentWeapon.bulletData);

                    yield return weaponShootWait;
                }
            }
            else
            {
                isShootOnce = true;
            }
        }
    }

    public void Shake(BulletSO bulletData)
    {
        if (camTween != null)
            camTween.Kill();

        CinemachineBasicMultiChannelPerlin perlin = GameManager.Instance.cmPerlinObject.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin != null)
        {
            perlin.m_AmplitudeGain = bulletData.shakeAmount;

            camTween = DOTween.To(() => perlin.m_AmplitudeGain, value => perlin.m_AmplitudeGain = value, 0, bulletData.shakeTime);
        }
    }

    public void KillShake()
    {
        camTween.Kill();
    }

}
