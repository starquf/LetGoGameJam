using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectHandler : Handler
{
    private Sequence sequence;

    public override void OnAwake()
    {
        GameManager.Instance.effectHandler = this;
    }

    public override void OnStart()
    {
        sequence = DOTween.Sequence();
    }

    public void SetEffect(EffectType type,SpriteRenderer sprite)
    {
       
        switch (type)
        {
            case EffectType.BounceHorizontal:
                EffectHorizontal(sprite);
                break;
            case EffectType.BounceVertical:
                break;
        }
    }

    private void EffectHorizontal(SpriteRenderer sprite)
    {
        sprite.transform.DOScaleX(.7f, .2f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            sprite.transform.DOScaleX(1f, .2f);
        });
        sprite.DOColor(Color.red, .2f).OnComplete(() =>
        {
            sprite.DOColor(Color.white, .3f);
        });
    }
}

[System.Serializable]
public enum EffectType
{
    BounceHorizontal,
    BounceVertical,
    SallangSallang
}
