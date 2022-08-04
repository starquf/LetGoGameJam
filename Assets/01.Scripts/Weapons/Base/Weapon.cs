using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected int maxBullet;

    [SerializeField]
    protected float fireInterval;
    [SerializeField]
    protected float collectionRate;

    [SerializeField]
    protected bool isAuto = false;

    [SerializeField]
    protected bool isPlayer = false;

    public void Init(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    public abstract void Shoot();
}
