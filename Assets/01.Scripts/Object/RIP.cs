using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RIP : LivingEntity, IPoolableComponent
{
    private const string DUST_PATH = "Prefabs/Effect/RIPDustEffect";

    private const float DROP_CORRECTION = 1f;
    private const float TWEEN_DURATION = 0.3f;

    private SpriteRenderer sr;

    private Sequence seq;

    [SerializeField]
    private Sprite dropSprite;
    [SerializeField]
    private Sprite ripSprite;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void SetHPUI()
    {
        
    }

    public void SetPosition(Vector2 pos)
    {
        sr.sprite = dropSprite;

        transform.position = new Vector2(pos.x, pos.y + DROP_CORRECTION);

        RIPDustEffect effect = GameObjectPoolManager.Instance.GetGameObject(DUST_PATH, null).GetComponent<RIPDustEffect>();
        effect.SetPosition(pos);
        effect.Play();

        if (seq != null)
        {
            seq.Kill();
        }

        seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(pos.y, TWEEN_DURATION).SetEase(Ease.InExpo));
        seq.AppendCallback(() => {
            sr.sprite = ripSprite;

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
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(gameObject);
    }
}
