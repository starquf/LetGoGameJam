using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AWM : Bullet
{
    private Collider2D bulletCol;

    private readonly string Shot_SFX_NAME = "SniperShot";

    protected void Start()
    {
        bulletCol = GetComponent<Collider2D>();
    }

    public override void Despawned()
    {
        bulletCol.enabled = false;
        ChangeState(BulletState.MoveForward);
    }

    public override void Spawned()
    {
        curSpeed = bulletSpeed;

        StartCoroutine(BulletLifetime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("AWM 충돌");
        }
    }


    public void ColliderOn()
    {
        bulletCol.enabled = true;
        GameManager.Instance.soundHandler.Play(Shot_SFX_NAME);
    }


}
