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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
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
        Init();

        coll.enabled = false;
    }

    public void SetDisable()
    {
        seq.Kill();
        Effect ripExplosionEffect = GameObjectPoolManager.Instance.GetGameObject(DESTROY_PATH, null).GetComponent<Effect>();
        ripExplosionEffect.SetPosition(new Vector2(transform.position.x, transform.position.y));
        ripExplosionEffect.SetRotation(new Vector3(-90f, 0, 0));
        ripExplosionEffect.Play();
        GameObjectPoolManager.Instance.UnusedGameObject(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            SetDisable();
        }
    }
}
