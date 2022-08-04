using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Allah : Bullet
{
    public override void Despawned()
    {
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
            print("아무튼 폭발");
            SetDisable();
        }
    }


}
