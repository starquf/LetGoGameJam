using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform titleImage;

    [SerializeField]
    private RectTransform Buttons;

    public void OnEnable()
    {
        titleImage.DOAnchorPos3DY(-414f, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            Buttons.DOAnchorPos3DY(125, 1f);
        });
    }
}
