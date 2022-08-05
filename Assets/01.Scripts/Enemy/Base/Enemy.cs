using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity, IPoolableComponent
{

    private const string DEAD_EFFECT_PATH = "Prefabs/Effect/DeadEffect";
    private const string RIP_PREFAB_PATH = "Prefabs/Object/RIP";

    public Color identityColor1;
    public Color identityColor2;

    public float attackRange = 10f;
    public float avoidRange = 3f;

    public Vector2 enterExpRange = Vector2.zero;

    public ExpInfo dropExpInfo = null;
    public enemyAttackType enemyAttackType = enemyAttackType.RANGED;

    [DrawIf("enemyAttackType", enemyAttackType.MELEE)]
    public float rushSpeed = 10f;

    public List<WeaponType> canHaveWeaponList = new List<WeaponType>();

    [SerializeField]protected EnemyAttack enemyAttack;
    [HideInInspector] public Transform playerTrm;
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
        if(enemyAttackType.Equals(enemyAttackType.RANGED))
        {
            weapon = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Weapons/Weapon_" +
                canHaveWeaponList[Random.Range(0, canHaveWeaponList.Count)].ToString(),
                enemyAttack.transform).GetComponent<Weapon>();
            weapon.transform.SetParent(enemyAttack.transform);
            weapon.transform.localPosition = Vector3.right;

            SetWeapon(weapon);
            AttackStop();
        }
        else
        {
            enemyAttack.enabled = false;
        }
        if(enemyAI == null)
            enemyAI = GetComponent<EnemyAI>();
        enemyAI.InitAI(this);
    }

    public void SetWeapon(Weapon weapon)
    {
        weapon.isPlayer = false;
        enemyAttack.Init(weapon);
    }

    public virtual void AttackStart()
    {
        enemyAttack.isAttacking = true;
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

        for (int i = 0; i < dropExpInfo.amount; i++)
        {
            ExpBall expBall = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Exp/ExpBall_" + dropExpInfo.type.ToString(), null).GetComponent<ExpBall>();
            expBall.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        }
        GameObjectPoolManager.Instance.GetGameObject(RIP_PREFAB_PATH, null).GetComponent<RIP>().SetPosition(transform.position);

        if(enemyAttackType.Equals(enemyAttackType.RANGED))
            GameObjectPoolManager.Instance.UnusedGameObject(weapon.gameObject);
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
