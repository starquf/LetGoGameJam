using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    [Header("기본 세팅")]
    public float maxHp;
    public virtual float curHp => hp; // 현재 체력에 쉴드 더한값

    [SerializeField] protected float hp;
    [SerializeField] protected float attackPower;

    [HideInInspector]
    public Rigidbody2D rigid;

    public float speed;
    public float attakMoveSpeed;

    public Transform dialogTrm;
    public DialogSO dialogData;

    public bool isKnockBack = false;

    protected Coroutine knockBackCo = null;


    public bool isInvincible = false;

    public float AttackPower
    {
        get
        {
            float atkPower = 0;
            atkPower = attackPower;// + weponAttackPower;

            return atkPower;
        }
    }

    public bool IsDie => isDie;
    protected bool isDie = false;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void Init()
    {
        isDie = false;

        hp = maxHp;

        SetHPUI();
    }

    public virtual void SetHp(int hp)
    {
        if (isDie)
        {
            return;
        }

        this.hp = hp;

        SetHPUI();

        if (hp <= 0)
        {
            Die();
        }
    }

    protected void ShowDialog(float size)
    {
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Dialog, JsonUtility.ToJson(new DialogInfo() { teller = transform, text = dialogData.dialogList[Random.Range(0, dialogData.dialogList.Count - 1)], size = size }));
    }

    public virtual void UpgradeHP(int value) //레벨업 보상에 쓸예정
    {
        maxHp += value;
        hp += value;
        SetHPUI();
    }

    public virtual void UpgradeAttackPower(int value) //레벨업 보상에 쓸예정
    {
        attackPower += value;
    }

    public virtual void GetDamage(float damage)
    {
        if (isDie || isInvincible) //이미 죽었거나 무적 상태라면
        {
            return;
        }

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }

        SetHPUI();
    }


    public virtual void KnockBack(Vector2 direction, float power, float duration)
    {
        if (isDie)
        {
            return;
        }
        if (!isKnockBack)
        {
            isKnockBack = true;
            knockBackCo = StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

    protected IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        float startTime = Time.time;
        float curTime = Time.time;
        while (duration > curTime - startTime)
        {
            if (isDie)
            {
                ResetKnockBackParam();
                yield break;
            }
            rigid.velocity = direction.normalized * power;
            yield return null;
            curTime = Time.time;
        }
        ResetKnockBackParam();
    }

    protected virtual void ResetKnockBackParam()
    {
        rigid.velocity = Vector2.zero;
        isKnockBack = false;
    }


    public virtual void Heal(int value) //value 만큼 회복합니다.
    {
        hp += value;
        hp = Mathf.Clamp(hp, 0, maxHp);

        SetHPUI();
    }

    public abstract void SetHPUI();

    public float GetHpRatio()
    {
        return hp / maxHp;
    }
    protected virtual void Die()
    {
        hp = 0;
        isDie = true;
        StopAllCoroutines();
    }

    public virtual void MoveRandomPos()
    {
        transform.position = new Vector2(Random.Range(GameManager.Instance.mapMin.position.x, GameManager.Instance.mapMax.position.x), Random.Range(GameManager.Instance.mapMin.position.y, GameManager.Instance.mapMax.position.y));  
    }
}
