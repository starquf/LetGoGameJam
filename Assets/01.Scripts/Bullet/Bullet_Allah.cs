using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Allah : Bullet
{
    public float explosionRange;
    private float spawnedTime = 0f;
    private bool isHit = false;

    public override void Spawned()
    {
        base.Spawned();
        spawnedTime = Time.time;
    }

    public override void Despawned()
    {
        base.Despawned();
        spawnedTime = 0f;
        rb.velocity = Vector3.zero;
        isHit = false;
    }

    protected override void BulletMove()
    {
        base.BulletMove();
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (isHit)
                break;
            rb.velocity *= Time.time - spawnedTime + 0.05f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override void Hit(LivingEntity hitEntity)
    {
        isHit = true;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, explosionRange, isEnemyBullet ? 1 << 6 | 1 << 12 : 1 << 7 | 1 << 12);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            collider2Ds[i].GetComponent<LivingEntity>().GetDamage(bulletDamage);
        }
        Effect explosionEffect = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Effect/ExplosionEffect", null).GetComponent<Effect>();
        explosionEffect.SetPosition(transform.position);
        explosionEffect.Play();
        GameManager.Instance.soundHandler.Play("RPGExplosion");


        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }


}
