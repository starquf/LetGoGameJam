using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUIPanel : MonoBehaviour
{
    public ChoicePanelInfo meritPanel;
    public ChoicePanelInfo demeritPanel;

    [HideInInspector]
    public Button btn;

    private Tween bounceTween;
    private Tween highlightTween;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void SetPanel(ChoiceData merit, ChoiceData demerit)
    {
        meritPanel.icon.sprite = merit.icon;
        meritPanel.name.text = merit.name;
        meritPanel.des.text = merit.des;
        meritPanel.level.text = $"레벨 : {merit.level.ToString()}";

        demeritPanel.icon.sprite = demerit.icon;
        demeritPanel.name.text = demerit.name;
        demeritPanel.des.text = demerit.des;
        demeritPanel.level.text = $"레벨 : {demerit.level.ToString()}";
    }

    public void ShowHighlight(Action onEndHighlight = null)
    {
        highlightTween.Kill();
        bounceTween.Kill();

        highlightTween = transform.DOScale(Vector3.one, 0.3f).From(Vector3.one * 0.5f)
            .OnComplete(() => 
            {
                bounceTween = transform.DOScale(Vector3.one * 1.04f, 0.7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetUpdate(true);
                onEndHighlight?.Invoke();
            })
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }
}

[System.Serializable]
public struct ChoiceData
{
    public Sprite icon;
    public string name;
    public string des;
    public int level;
}

[System.Serializable]
public struct ChoicePanelInfo
{
    public Image icon;
    public Text name;
    public Text des;
    public Text level;
}
