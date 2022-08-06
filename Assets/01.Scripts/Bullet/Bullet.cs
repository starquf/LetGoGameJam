using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolableComponent
{
    private const string HIT_EFFECT_PATH = "Prefabs/Effect/HitEffect";

    [HideInInspector]
    public Rigidbody2D rb = null;
    protected SpriteRenderer sr = null;

    [SerializeField]
    private TrailRenderer tr;

    private BulletState currentState = BulletState.MoveForward;

    public Sprite playerBulletSpr;
    public Sprite enemyBulletSpr;

    protected float bulletDamage = 1f;

    public float bulletSpeed = 30f;
    public float curSpeed = 0f;

    protected int bulletIron = 0;
    public int originBulletIron = 0;

    public float lifeTime = 3f;

    public BulletSO bulletData;

    public Vector3 bulletDir;

    // 적의 총알인가?
    public bool isEnemyBullet = true;

    private GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

        //curSpeed = bulletSpeed;
    }

    public virtual void SetOwner(bool isEnemy)
    {
        //print("tlqkaaaaaa");
        isEnemyBullet = isEnemy;
        sr.sprite = isEnemyBullet ? enemyBulletSpr : playerBulletSpr;
        curSpeed = isEnemyBullet ? curSpeed * 0.5f : curSpeed;
        if(tr != null)
        {
            alphaKey[0].alpha = isEnemyBullet ? 0.7f : 1f;
            alphaKey[0].time = 0f;
            alphaKey[1].alpha = isEnemyBullet ? 0.7f : 1f;
            alphaKey[1].time = 1f;
            tr.colorGradient.SetKeys(tr.colorGradient.colorKeys, alphaKey);
        }
        ChangeState(BulletState.MoveForward);
        
    }

    public virtual void Despawned()
    {
        ChangeState(BulletState.MoveForward);
        if(tr != null)
        {
            tr.Clear();
        }
    }

    public virtual void Spawned()
    {
        curSpeed = bulletSpeed;
        bulletIron = originBulletIron;
        
        StartCoroutine(BulletLifetime());
    }

    protected void FixedUpdate()
    {
        CheckState();
        CheckTransform();
    }

    protected virtual void BulletMove()
    {
        rb.velocity = transform.right * curSpeed * GameManager.Instance.timeScale;
    }

    protected virtual void CheckTransform()     // 화면 밖으로 나갔는가?
    {
        /*
        if (transform.position.x > GameManager.Instance.maxSize.x + 5f || transform.position.x < GameManager.Instance.minSize.x - 5f)
        {
            SetDisable();
        }

        if (transform.position.y > GameManager.Instance.maxSize.y + 2f || transform.position.y < GameManager.Instance.minSize.y - 2f)
        {
            SetDisable();
        }
        */
    }

    protected virtual void ChangeState(BulletState state)
    {
        currentState = state;
    }


    protected virtual void CheckState()
    {
        switch (currentState)
        {
            case BulletState.MoveForward:
                BulletMove();
                break;

            case BulletState.Stop:

                break;
        }
    }

    protected IEnumerator BulletLifetime()
    {
        float t = 0;

        while (true)
        {
            t += Time.deltaTime * GameManager.Instance.timeScale;

            yield return null;

            if (t > lifeTime)
                break;
        }

        SetDisable();
    }

    public void AddBulletIron(int value)
    {
        bulletIron += value;
    }

    public void SetDamage(float value)
    {
        bulletDamage = value;
    }

    #region ChangeDirections
    public virtual void ChangeDir(Vector3 dir)      // dir방향으로 회전
    {
        bulletDir = dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void ChangeDir(Vector3 dir, float t)     // 시간차로 dir방향으로 회전
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        StartCoroutine(Timer(() => transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward), t));
    }

    /*
    public virtual void ChangeDirToPlayer()     // 플레이어 방향으로 회전
    {
        Vector3 dir = GameManager.Instance.playerPos.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void ChangeDirToPlayer(float t)     // 시간차로 플레이어 방향으로 회전
    {
        StartCoroutine(Timer(() =>
        {
            Vector3 dir = GameManager.Instance.playerPos.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        , t));
    }
    */

    public virtual void RotateAngle(float angle)    // angle만큼 z축 회전
    {
        transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + angle, Vector3.forward);
    }

    public virtual void RotateAngle(float angle, float t)   // 시간차로 angle만큼 z축 회전
    {
        StartCoroutine(Timer(() => transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + angle, Vector3.forward), t));
    }

    public virtual void ChangeSpeed(float value)
    {
        curSpeed = value;
    }

    public virtual void ChangeSpeed(float value, float t)
    {
        StartCoroutine(Timer(() => curSpeed = value, t));
    }

    public virtual void PlusSpeed(float value)
    {
        curSpeed += value;
    }

    public virtual void PlusSpeed(float value, float t)
    {
        StartCoroutine(Timer(() => curSpeed += value, t));
    }
    #endregion

    protected IEnumerator Timer(Action action, float t)
    {
        yield return new WaitForSeconds(t);
        action.Invoke();
    }

    public void SetDisable()
    {
        StopAllCoroutines();
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    protected virtual void Hit(LivingEntity hitEntity)
    {
        PlayHitEffect(hitEntity);

        hitEntity.GetDamage(bulletDamage);
        hitEntity.KnockBack(bulletDir, bulletData.knockBackPower, bulletData.knockBackTime);

        if (bulletIron <= 0)
        {
            //print("수명 끝남");
            GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
        }
        else 
        {
            //print("관통함");
            bulletIron--;
        }
    }

    protected void PlayHitEffect(LivingEntity hitEntity, bool isCenterPlay = false)
    {
        if (hitEntity is Enemy)
        {
            Enemy enemy = hitEntity as Enemy;

            ColorEffect hitEffect = GameObjectPoolManager.Instance.GetGameObject(HIT_EFFECT_PATH, null).GetComponent<ColorEffect>();
            hitEffect.SetColor(enemy.identityColor1, enemy.identityColor2);

            hitEffect.SetPosition(isCenterPlay ? hitEntity.transform.position : transform.position);
            hitEffect.Play();

            GameManager.Instance.soundHandler.Play("EnemyHit");
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!isEnemyBullet && (collision.gameObject.layer == LayerMask.NameToLayer("RIP") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))))// || (isEnemyBullet &&collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            LivingEntity livingEntity = collision.GetComponent<LivingEntity>();
            Hit(livingEntity);
        }

        if(isEnemyBullet && (collision.gameObject.layer == LayerMask.NameToLayer("Parrying")))
        {
            GameManager.Instance.soundHandler.Play("MeleeAttackHit");
            GameManager.Instance.playerTrm.GetComponentInChildren<PlayerParrying>().StartParryingEffect(this);
        }

        if (isEnemyBullet && (collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            LivingEntity livingEntity = collision.GetComponent<LivingEntity>();
            Hit(livingEntity);
        }
    }
}
