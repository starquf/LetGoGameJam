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
        GetComponent<BossAI>().SetDie();

        SetDisable();
    }

    public override void SetHPUI()
    {
        
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);

        GameManager.Instance.effectHandler.SetEffect(EffectType.EnemyHit, sr, Color.white);

        int rand = Random.Range(0, 100);

        if(rand >=0 && rand < 80)
        {
            
        }
        else if(rand >= 80 && rand < 84)
        {
            SpawnWeapon(WeaponType.Ak47);
        }
        else if(rand >= 84 && rand < 87)
        {
            SpawnWeapon(WeaponType.MagicBar);
        }
        else if(rand >= 87 && rand < 89)
        {
            SpawnWeapon(WeaponType.MP7);
        }
        else if(rand >= 89 && rand < 92)
        {
            SpawnWeapon(WeaponType.AWM);
        }
        else if(rand >= 92 && rand < 94)
        {
            SpawnWeapon(WeaponType.RazerPistol);
        }
        else if(rand >= 94 && rand < 96)
        {
            SpawnWeapon(WeaponType.M870);
        }
        else if(rand >= 96 && rand <= 97)
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

        wp.transform.position = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad) * weaponDropRange + transform.position.x, Mathf.Sin(degree * Mathf.Deg2Rad) * weaponDropRange + transform.position.y + weaponDropCorrectionY);
        wp.SetDestoryTimer(30);
        wp.isGround = true;

        wp.sr.material.SetInt("_IsActive", 1);
    }

    public void Spawned()
    {
        isDie = false;

        hp = maxHp;

        SetHPUI();

        GameManager.Instance.effectHandler.SetEffect(EffectType.EnemyBounce, sr, Color.white);
        GameManager.Instance.effectHandler.SetEffect(EffectType.EnemySallangSallang, sr, Color.white);
    }

    public void SetDisable()
    {
        ExpBall expBall = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Exp/ExpBall_BOSS", null).GetComponent<ExpBall>();
        expBall.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + weaponDropCorrectionY), weaponDropRange);
    }
}
