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

    public override void SetOwner(bool isEnemy)
    {
        base.SetOwner(isEnemy);
        print(isEnemyBullet);
        anim.SetBool("isEnemyBullet", isEnemyBullet);
    }
    public override void Despawned()
    {
        bulletCol.enabled = false;
        ChangeState(BulletState.MoveForward);
    }

    public override void Spawned()
    {
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
        GameManager.Instance.soundHandler.Play(Shot_SFX_NAME);
    }


}
