using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    [Header("기본 세팅")]
    public float maxHp;
    public float curHp => hp; // 현재 체력에 쉴드 더한값

    [SerializeField] protected float hp;
    [SerializeField] protected float attackPower;
    [SerializeField] protected float speed;

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

    public virtual void Init()
    {
        isDie = false;

        hp = maxHp;

        SetHPUI();
    }

    public virtual void SetHp(int hp)
    {
        if (IsDie)
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
        if (isDie) //이미 죽었거나 무적 상태라면
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
    }
}
