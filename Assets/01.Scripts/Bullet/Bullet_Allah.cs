using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Allah : Bullet
{
    public float explosionRange;

    protected override void Hit(LivingEntity hitEntity)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, explosionRange, isEnemyBullet ? 1 << 6 | 1 << 12 : 1 << 7 | 1 << 12);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            collider2Ds[i].GetComponent<LivingEntity>().GetDamage(bulletDamage);
        }
        ExplosionEffect explosionEffect = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Effect/ExplosionEffect", null).GetComponent<ExplosionEffect>();
        explosionEffect.SetPosition(transform.position);
        explosionEffect.Play();
        GameManager.Instance.soundHandler.Play("RPGExplosion");


        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }


}
