using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AWM : Bullet
{
    private Collider2D bulletCol;
    private Animator anim;
    private readonly string Shot_SFX_NAME = "SniperShot";

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    protected void Start()
    {
        bulletCol = GetComponent<Collider2D>();
        
    }

    public override void SetOwner(bool isEnemy, WeaponType weaponType)
    {
        base.SetOwner(isEnemy, weaponType);
        anim.SetBool("isEnemyBullet", isEnemyBullet);
    }
    public override void Despawned()
    {
        bulletCol.enabled = false;
        ChangeState(BulletState.MoveForward);
    }

    public override void Spawned()
    {
        base.Spawned();
        curSpeed = bulletSpeed;
      
      
        anim.SetTrigger("onShot");
        StartCoroutine(BulletLifetime());
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }


    public void ColliderOn()
    {
        bulletCol.enabled = true;

        GameManager.Instance.vCamScript.Shake(bulletData);
        GameManager.Instance.soundHandler.Play(Shot_SFX_NAME);
    }

    protected override void Hit(LivingEntity hitEntity)
    {
        PlayHitEffect(hitEntity, true);

        hitEntity.GetDamage(bulletDamage);
        hitEntity.KnockBack(bulletDir, bulletData.knockBackPower, bulletData.knockBackTime);
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
