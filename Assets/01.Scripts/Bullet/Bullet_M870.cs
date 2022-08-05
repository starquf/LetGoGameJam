using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_M870 : Bullet
{
    private Collider2D bulletCol;

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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("샷건 충돌");
        }
    }


    public void ColliderOn()
    {
        bulletCol.enabled = true;
    }


}
