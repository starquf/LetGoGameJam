using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : LivingEntity, IPoolableComponent
{
    public SpriteRenderer sr;

    protected override void Die()
    {
        base.Die();
    }

    public override void SetHPUI()
    {
        
    }

    public void Despawned()
    {
        throw new System.NotImplementedException();
    }

    public void Spawned()
    {
        GameManager.Instance.effectHandler.SetEffect(EffectType.EnemyBounce, sr);
        GameManager.Instance.effectHandler.SetEffect(EffectType.EnemySallangSallang, sr);
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
