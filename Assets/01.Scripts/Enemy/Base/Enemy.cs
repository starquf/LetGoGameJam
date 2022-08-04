using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity, IPoolableComponent
{
    public float attackRange = 10f;

    public Vector2 expRange = Vector2.zero;

    public List<WeaponType> canHaveWeaponList = new List<WeaponType>();

    [SerializeField]protected EnemyAttack enemyAttack;
    [HideInInspector] public Transform playerTrm;
    [HideInInspector]public Rigidbody2D rigid;
    [HideInInspector] public SpriteRenderer sr;

    private Weapon weapon = null;
    private SpriteRenderer weaponSr = null;

    public void Despawned()
    {

    }

    public void Spawned()
    {
        playerTrm = GameManager.Instance.playerTrm;
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemyAttack.targetPos = playerTrm;

        weapon = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Weapons/Weapon_" +
            canHaveWeaponList[Random.Range(0, canHaveWeaponList.Count)].ToString(),
            enemyAttack.transform).GetComponent<Weapon>();

        weaponSr = weapon.GetComponent<SpriteRenderer>();
        weapon.transform.SetParent(enemyAttack.transform);
        weapon.transform.localPosition = Vector3.right;
        SetWeapon(weapon);
        AttackStop();
    }

    public void SetWeapon(Weapon weapon)
    {
        enemyAttack.Init(weapon);
    }

    public virtual void AttackStart()
    {
        weaponSr.color = Color.white;
        enemyAttack.isAttacking = true;
        enemyAttack.shootStartTime = Time.time;
    }
    public virtual void AttackStop()
    {
        enemyAttack.isAttacking = false;
        weaponSr.color = Color.clear;
    }

    public override void SetHPUI()
    {

    }
}
