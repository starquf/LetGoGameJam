using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet_Allah : Bullet
{
    public float explosionRange;

    public override void Spawned()
    {
        base.Spawned();

    }

    public override void Despawned()
    {
        base.Despawned();
        rb.velocity = Vector3.zero;
    }

    protected override void BulletMove()
    {
        rb.velocity = transform.right * curSpeed * GameManager.Instance.timeScale;
        curSpeed += (rb.velocity.magnitude * 0.8f * GameManager.Instance.timeScale * Time.deltaTime);
    }

    protected override void Hit(LivingEntity hitEntity)
    {
        GameManager.Instance.vCamScript.Shake(bulletData);
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, explosionRange, isEnemyBullet ? 1 << 6 | 1 << 12 : 1 << 7 | 1 << 12);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            PlayHitEffect(collider2Ds[i].GetComponent<LivingEntity>(), true);
            collider2Ds[i].GetComponent<LivingEntity>().GetDamage(bulletDamage);
            if (!isEnemyBullet)
            {
                if (Random.Range(0, 100) < hallucinationPercent && hitEntity.GetComponent<Enemy>() != null)
                {
                    hitEntity.MoveRandomPos();
                }
                GameManager.Instance.addUsedWeaponDamageInfo(shotWeaponType, bulletDamage);
            }
            collider2Ds[i].GetComponent<LivingEntity>().KnockBack(bulletDir, bulletData.knockBackPower, bulletData.knockBackTime);
        }
        Effect explosionEffect = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Effect/ExplosionEffect", null).GetComponent<Effect>();
        explosionEffect.SetPosition(transform.position);
        explosionEffect.Play();
        GameManager.Instance.soundHandler.Play("RPGExplosion");


        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
