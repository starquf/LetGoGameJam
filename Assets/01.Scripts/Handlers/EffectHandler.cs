using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EffectHandler : Handler
{
    private Sequence sequence;

    public override void OnAwake()
    {
        GameManager.Instance.effectHandler = this;
    }

    public override void OnStart()
    {
        DOTween.SetTweensCapacity(2000, 10);

        sequence = DOTween.Sequence();
    }

    public void SetEffect(EffectType type, SpriteRenderer sprite, bool isPlayer = true)
    {

        switch (type)
        {
            case EffectType.BounceHorizontal:
                EffectHorizontal(sprite, isPlayer);
                break;
            case EffectType.EnemyBounce:
                EffectEnemy(sprite);
                break;
            case EffectType.EnemySallangSallang:
                EffectSallangSallang(sprite);
                break;
        }
    }



    private void EffectHorizontal(SpriteRenderer sprite, bool isPlayer)
    {
        sprite.transform.DOScaleX(.7f, .2f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            sprite.transform.DOScaleX(1f, .2f);
        });


        if (!isPlayer)
        {
            sprite.DOColor(Color.red, .2f).OnComplete(() =>
            {
                sprite.DOColor(Color.white, .3f);
            });
        }
    }

    private void EffectSallangSallang(SpriteRenderer sprite)
    {
        sprite.transform.DORotate(new Vector3(0, 0, 15f), .5f).OnComplete(() =>
        {
            sprite.transform.DORotate(new Vector3(0, 0, -15f), .5f).OnComplete(() =>
            {
                EffectSallangSallang(sprite);
            });
        });

    }

    private void EffectEnemy(SpriteRenderer sprite)
    {
        sprite.transform.DOScaleX(.75f, .25f).OnComplete(() =>
        {
            sprite.transform.DOScaleX(1f, .25f).OnComplete(() =>
            {
                EffectEnemy(sprite);
            });
        });

        sprite.transform.DOScaleY(1.2f, .25f).OnComplete(() =>
        {
            sprite.transform.DOScaleY(1f, .25f);
        });
    }
}

[System.Serializable]
public enum EffectType
{
    BounceHorizontal,
    BounceVertical,
    EnemyBounce,
    EnemySallangSallang
}
