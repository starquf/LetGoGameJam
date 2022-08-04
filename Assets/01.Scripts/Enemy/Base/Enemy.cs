using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public float attackRange = 10f;

    public Vector2 expRange = Vector2.zero;
    public Vector2 enterScoreRange = Vector2.zero;

    public List<Weapon> canHaveWeapon = new List<Weapon>();

    [SerializeField]private EnemyAttack enemyAttack;
    [HideInInspector]public Rigidbody2D rigid;
    [HideInInspector] public SpriteRenderer sr;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetWeapon(Weapon weapon)
    {
        enemyAttack.Init(weapon);
    }

    public virtual void AttackStart()
    {
        enemyAttack.isAttacking = true;
        enemyAttack.shootStartTime = Time.time;
    }
    public virtual void AttackStop()
    {
        enemyAttack.isAttacking = false;
    }

    public override void SetHPUI()
    {

    }
}
