using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpBar : UIBase
{
    [SerializeField]
    private RectTransform realFill;
    [SerializeField]
    private RectTransform fill;
    private Slider expSlider;

    public override void Init()
    {
        mydataType = UIDataType.Exp;
        expSlider = GetComponent<Slider>();
    }

    public override void SetData(string data)
    {
        DOTween.Kill(expSlider);
        DOTween.Kill(realFill);

        expSlider.DOValue(float.Parse(data.Trim()),.2f).SetUpdate(true).OnComplete(()=>
        {
            realFill.DOSizeDelta(new Vector2(fill.rect.width, realFill.rect.height), .2f).SetEase(Ease.InQuart).SetUpdate(true);
        });
    }
}

