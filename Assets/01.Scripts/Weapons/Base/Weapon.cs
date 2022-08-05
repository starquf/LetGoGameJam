using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPoolableComponent
{
    public string shotSFXName;

    public float damage = 1f;
    public int maxBullet = 5;
    public int bulletIron = 0;

    public float fireRate = 0f;
    public float collectionRate = 0f;

    public bool isAuto = false;

    public bool isPlayer = false;

    public float offset = 1f;

    public WeaponType weaponType;

    public Transform shootPos;

    public BulletSO bulletData;

    public SpriteRenderer sr;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public abstract void Shoot(Vector3 shootDir);

    public void Despawned()
    { 
        
    }

    public void Spawned()
    {
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        bulletIron = 0;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
