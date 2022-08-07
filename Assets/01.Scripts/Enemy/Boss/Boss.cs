using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : LivingEntity, IPoolableComponent
{
    public SpriteRenderer sr;

    public float weaponDropRange;
    public float weaponDropCorrectionY;

    protected override void Die()
    {
        base.Die();
    }

    public override void SetHPUI()
    {
        
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);

        int rand = Random.Range(0, 100);

        if(rand >=0 && rand < 60)
        {
            
        }
        else if(rand >= 60 && rand < 70)
        {
            SpawnWeapon(WeaponType.Ak47);
        }
        else if(rand >= 70 && rand < 75)
        {
            SpawnWeapon(WeaponType.MagicBar);
        }
        else if(rand >= 75 && rand < 79)
        {
            SpawnWeapon(WeaponType.MP7);
        }
        else if(rand >= 79 && rand < 84)
        {
            SpawnWeapon(WeaponType.AWM);
        }
        else if(rand >= 84 && rand < 85)
        {
            SpawnWeapon(WeaponType.BlueArchive);
        }
        else if(rand >= 85 && rand < 87)
        {
            SpawnWeapon(WeaponType.RazerPistol);
        }
        else if(rand >= 87 && rand < 93)
        {
            SpawnWeapon(WeaponType.M870);
        }
        else if(rand >= 93 && rand <= 96)
        {
            int degree = Random.Range(0, 361);

            Ammo ammo = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Object/Ammo", null).GetComponent<Ammo>();
            ammo.transform.position = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad) * weaponDropRange, Mathf.Sin(degree * Mathf.Deg2Rad) * weaponDropRange + weaponDropCorrectionY);
            ammo.SetDestoryTimer(30f);
        }
        else
        {
            int degree = Random.Range(0, 361);

            Heart h = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Object/Heart", null).GetComponent<Heart>();
            h.transform.position = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad) * weaponDropRange, Mathf.Sin(degree * Mathf.Deg2Rad) * weaponDropRange + weaponDropCorrectionY);
            h.SetDestoryTimer(30f);
        }
    }

    public void Despawned()
    {
        
    }

    private void SpawnWeapon(WeaponType weapon)
    {
        int degree = Random.Range(0, 361);

        Weapon wp = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Weapons/Weapon_" + weapon.ToString(), null).GetComponent<Weapon>();
        GameManager.Instance.allItemListAdd(wp);

        wp.transform.position = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad) * weaponDropRange, Mathf.Sin(degree * Mathf.Deg2Rad) * weaponDropRange + weaponDropCorrectionY);
        wp.SetDestoryTimer(30);
        wp.isGround = true;

        wp.sr.material.SetInt("_IsActive", 1);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + weaponDropCorrectionY), weaponDropRange);
    }
}
