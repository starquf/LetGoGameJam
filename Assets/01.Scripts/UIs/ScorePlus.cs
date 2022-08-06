using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScorePlus : MonoBehaviour , IPoolableComponent
{
    private RectTransform rectTransform;
    private Text text;

    public void Despawned()
    {
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    public void Spawned()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();

        text.color = new Color(1, 1, 1, 0);
        rectTransform.anchoredPosition3D = new Vector3(315, 60, 0);
        rectTransform.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetText(string _text)
    {
        text.text = "+" + _text;
        DOTween.Kill(rectTransform);
        DOTween.Kill(text);

        rectTransform.DOAnchorPos3DY(0, .2f).OnComplete(() =>
        {
            rectTransform.DOAnchorPos3DX(0, .2f).SetEase(Ease.InExpo).SetUpdate(true);
        });

        text.DOFade(1f, .2f).OnComplete(() =>
        {
            text.DOFade(0, .2f).OnComplete(() =>
            {
                SetDisable();
            });
        });
    }
}
