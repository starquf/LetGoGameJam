using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class WeaponInfo
{
    public WeaponType type;
    public int dropPersent;
}

public class Enemy : LivingEntity, IPoolableComponent
{
    public List<int> maxHPForPlayerLevel = new List<int>();

    private const string DEAD_EFFECT_PATH = "Prefabs/Effect/DeadEffect";
    private const string RIP_PREFAB_PATH = "Prefabs/Object/RIP";

    public Color identityColor1;
    public Color identityColor2;

    public float attackRange = 10f;
    public float avoidRange = 3f;

    public ExpInfo dropExpInfo = null;
    public enemyAttackType enemyAttackType = enemyAttackType.RANGED;

    [DrawIf("enemyAttackType", enemyAttackType.MELEE)]
    public float rushSpeed = 10f;

    public List<WeaponInfo> canHaveWeaponList = new List<WeaponInfo>();
    public float dropGunPersent = 25f;
    public float dropHearPersent = 3f;
    public float dropAmmoPersent = 10f;

    public EnemyAttack enemyAttack;
    [HideInInspector] public Transform playerTrm;


    public SpriteRenderer sr;

    private EnemyAI enemyAI = null;
    [HideInInspector] public Weapon weapon = null;

    private bool isElite = false;

    public void Despawned()
    {
        enemyAI.SetActive(false);
    }
    public override void Init()
    {
        isDie = false;
        isElite = false;

        hp = maxHPForPlayerLevel[GameManager.Instance.playerTrm.GetComponentInChildren<PlayerUpgrade>().CurrentLevel / 10];

        SetHPUI();
    }

    public virtual void Spawned()
    {
        Init();
        playerTrm = GameManager.Instance.playerTrm;
     
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        /*if (sr == null)
            sr = GetComponent<SpriteRenderer>();*/
        enemyAttack.targetPos = playerTrm;
        if(enemyAttackType.Equals(enemyAttackType.RANGED))
        {
            StageHandler stageHandler = GameManager.Instance.stageHandler;
            int idx = 0;
            List<int> canRand = new List<int>();
            for (int i = 0; i < canHaveWeaponList.Count; i++)
            {
                if(stageHandler.CanGetWeapon(canHaveWeaponList[i].type))
                {
                    canRand.Add(i);
                }
            }

            if (canRand.Count != 0)
            {
                idx = 0;
                int count = 0;
                for (int i = 0; i < canRand.Count; i++)
                {
                    count += canHaveWeaponList[canRand[i]].dropPersent;
                }
                int rand = Random.Range(0, count);
                for (int i = 0; i < canRand.Count; i++)
                {
                    idx += canHaveWeaponList[canRand[i]].dropPersent;
                    if (rand < idx)
                    {
                        idx = i;
                        break;
                    }
                }
            }
            else
            {
                idx = 0;
            }
            weapon = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Weapons/Weapon_" +
                canHaveWeaponList[idx].type.ToString(),
                enemyAttack.transform).GetComponent<Weapon>();
            //print(canHaveWeaponList[idx].type.ToString());
            weapon.transform.SetParent(enemyAttack.transform);
            weapon.transform.localPosition = Vector3.right;

            if (enemyAI == null)
                enemyAI = GetComponent<EnemyAI>();
            enemyAI.InitAI(this);

            SetWeapon(weapon);
            GameManager.Instance.effectHandler.SetEffect(EffectType.EnemyBounce, sr);
            GameManager.Instance.effectHandler.SetEffect(EffectType.EnemySallangSallang, sr);
            AttackStop();
        }
        else
        {
            if (enemyAI == null)
                enemyAI = GetComponent<EnemyAI>();
            enemyAI.InitAI(this);

            enemyAttack.enabled = false;
        }
    }

    public void SetElite()
    {
        isElite = true;
    }

    public void SetWeapon(Weapon weapon)
    {
        weapon.isPlayer = false;
        enemyAttack.Init(weapon);
    }

    public virtual void AttackStart()
    {
        enemyAttack.Init();
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
        ColorEffect effect = GameObjectPoolManager.Instance.GetGameObject(DEAD_EFFECT_PATH, null).GetComponent<ColorEffect>();
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Killcount, "1");

        effect.SetPosition(transform.position);
        effect.SetRotation(new Vector3(-90f, 0, 0));
        effect.SetColor(identityColor1, identityColor2);

        effect.Play();

        GameManager.Instance.soundHandler.Play("EnemyDead");

        for (int i = 0; i < dropExpInfo.amount; i++)
        {
            if(dropExpInfo.dropPersent > Random.Range(0, 100))
            {
                ExpBall expBall = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Exp/ExpBall_" + dropExpInfo.type.ToString(), null).GetComponent<ExpBall>();
                expBall.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            }
        }

        if (dropHearPersent > Random.Range(0, 100))
        {
            Heart h = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Object/Heart", null).GetComponent<Heart>();
            h.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            h.SetDestoryTimer(30f);
        }

        if (dropAmmoPersent > Random.Range(0, 100))
        {
            Ammo ammo = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Object/Ammo", null).GetComponent<Ammo>();
            ammo.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            ammo.SetDestoryTimer(30f);
        }

        WeaponType weaponType = WeaponType.M1911;
        if (enemyAttackType.Equals(enemyAttackType.RANGED))
        {
            if (!weapon.weaponType.Equals(WeaponType.M1911))
            {
                if (Random.Range(0, 100) < dropGunPersent)
                {
                    weaponType = weapon.weaponType;
                }
            }
        }
        
        GameObjectPoolManager.Instance.GetGameObject(RIP_PREFAB_PATH, null).GetComponent<RIP>().SetDreopWeapon(weaponType, isElite).SetPosition(transform.position);

        GameManager.Instance.stageHandler.amountEnemy--;

        if (enemyAttackType.Equals(enemyAttackType.RANGED))
        {
            GameManager.Instance.stageHandler.amountWeaponType[weapon.weaponType]--;
            GameObjectPoolManager.Instance.UnusedGameObject(weapon.gameObject);
        }
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
    protected override void Die()
    {
        base.Die();
        //Debug.Log("나죽음");
    }
}
