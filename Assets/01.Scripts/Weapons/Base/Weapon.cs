using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : PoolableComponent
{
    public float damage = 1f;
    public int maxBullet = 5;

    public float fireRate = 0f;
    public float collectionRate = 0f;

    public bool isAuto = false;

    public bool isPlayer = false;

    public WeaponType weaponType;

    public Transform shootPos;

    public abstract void Shoot(Vector3 shootDir);

    public override void Despawned()
    { 

    }

    public override void Spawned()
    {
        
    }
}
