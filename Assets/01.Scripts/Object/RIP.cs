using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RIP : LivingEntity, IPoolableComponent
{
    private const string DUST_PATH = "Prefabs/Effect/RIPDustEffect";
    private const string DESTROY_PATH = "Prefabs/Effect/RIPDestroyEffect";

    private const float DROP_CORRECTION = 1f;
    private const float TWEEN_DURATION = 0.3f;

    private SpriteRenderer sr;

    private Sequence seq;

    [SerializeField]
    private Sprite dropSprite;
    [SerializeField]
    private Sprite ripSprite;

    private Collider2D coll;

    private WeaponType dropWeaponType = WeaponType.M1911;

    private bool hasEliteWeapon = false;
    private Vector2 defaultScale;
    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        defaultScale = transform.localScale;
    }

    public override void SetHPUI()
    {
        
    }

    public void SetPosition(Vector2 pos)
    {
        sr.sprite = dropSprite;

        transform.position = new Vector2(pos.x, pos.y + DROP_CORRECTION);

        Effect ripDustEffect = GameObjectPoolManager.Instance.GetGameObject(DUST_PATH, null).GetComponent<Effect>();
        ripDustEffect.SetPosition(pos);
        ripDustEffect.Play();

        if (seq != null)
        {
            seq.Kill();
        }

        seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(pos.y, TWEEN_DURATION).SetEase(Ease.InExpo));
        seq.AppendCallback(() => {
            sr.sprite = ripSprite;

            coll.enabled = true;
            GameManager.Instance.soundHandler.Play("RIPDrop");
        });
    }
    protected override void Die()
    {
        base.Die();


        SetDisable();
    }

    public void Despawned()
    {
    }

    public void Spawned()
    {
        GameManager.Instance.allItemListAdd(this);
        Init();
        transform.localScale = defaultScale;

        coll.enabled = false;
        hasEliteWeapon = false;
    }

    public void SetDisable()
    {
        GameManager.Instance.allItemListRemove(this);
        seq.Kill();
        Effect ripExplosionEffect = GameObjectPoolManager.Instance.GetGameObject(DESTROY_PATH, null).GetComponent<Effect>();
        ripExplosionEffect.SetPosition(new Vector2(transform.position.x, transform.position.y));
        ripExplosionEffect.SetRotation(new Vector3(-90f, 0, 0));
        ripExplosionEffect.Play();


        if (!dropWeaponType.Equals(WeaponType.M1911))
        {
            Weapon wp = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Weapons/Weapon_" + dropWeaponType.ToString(), null).GetComponent<Weapon>();
            GameManager.Instance.allItemListAdd(wp);
            if (hasEliteWeapon)
            {
                wp.transform.localScale *= 2;
            }
            wp.transform.position = transform.position;
            wp.SetDestoryTimer(30);
            wp.isGround = true;

            wp.sr.material.SetInt("_IsActive", 1);

            //Debug.Log(dropWeaponType.ToString() + "드롭됨");
        }

        GameManager.Instance.soundHandler.Play("RIPDestroy");
        GameObjectPoolManager.Instance.UnusedGameObject(gameObject);
    }

    public RIP SetDreopWeapon(WeaponType weaponType, bool isElite)
    {
        dropWeaponType = weaponType;
        if(isElite)
        {
            hp = 4;
            transform.localScale *= 2;
            hasEliteWeapon = true;
        }

        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            SetDisable();
        }
    }
}
