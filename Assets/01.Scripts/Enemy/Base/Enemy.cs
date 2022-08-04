using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity, IPoolableComponent
{
    private const string DEAD_EFFECT_PATH = "Prefabs/Effect/DeadEffect";

    public Color identityColor1;
    public Color identityColor2;

    public float attackRange = 10f;

    public Vector2 expRange = Vector2.zero;

    public List<WeaponType> canHaveWeaponList = new List<WeaponType>();

    [SerializeField]protected EnemyAttack enemyAttack;
    [HideInInspector] public Transform playerTrm;
    [HideInInspector]public Rigidbody2D rigid;
    [HideInInspector] public SpriteRenderer sr;

    private EnemyAI enemyAI = null;
    private Weapon weapon = null;

    public void Despawned()
    {
        enemyAI.SetActive(false);
    }

    public virtual void Spawned()
    {
        Init();
        playerTrm = GameManager.Instance.playerTrm;
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
        enemyAttack.targetPos = playerTrm;

        weapon = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Weapons/Weapon_" +
            canHaveWeaponList[Random.Range(0, canHaveWeaponList.Count)].ToString(),
            enemyAttack.transform).GetComponent<Weapon>();
        weapon.transform.SetParent(enemyAttack.transform);
        weapon.transform.localPosition = Vector3.right;
        if(enemyAI == null)
            enemyAI = GetComponent<EnemyAI>();
        enemyAI.InitAI(this);

        SetWeapon(weapon);
        AttackStop();
    }

    public void SetWeapon(Weapon weapon)
    {
        weapon.isPlayer = false;
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

    public void SetDisable()
    {
        DeadEffect effect = GameObjectPoolManager.Instance.GetGameObject(DEAD_EFFECT_PATH, null).GetComponent<DeadEffect>();

        effect.SetPosition(transform.position);
        effect.SetColor(identityColor1, identityColor2);

        effect.Play();

        GameManager.Instance.soundHandler.Play("EnemyDead");

        GameObjectPoolManager.Instance.UnusedGameObject(weapon.gameObject);
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Bullet b = collision.GetComponent<Bullet>();
            if(!b.isEnemyBullet)
            {
                GetDamage(b.bulletDamage);
                if(--b.bulletPenetrate <= 0)
                    b.SetDisable();
            }
        }
    }
}
