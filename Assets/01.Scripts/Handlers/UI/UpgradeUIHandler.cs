using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIHandler : MonoBehaviour
{
    private CanvasGroup cvs;
    private UpgradeHandler uh;

    public List<SelectUIPanel> panels = new List<SelectUIPanel>();
    public List<ParticleSystem> upgradeParticles = new List<ParticleSystem>();
    public Text selectText;

    public int showPanelCount = 2;

    private Tween selectTextTween;

    private void Awake()
    {
        GameManager.Instance.upgradeUIHandler = this;
    }

    private void Start()
    {
        uh = GameManager.Instance.upgradeHandler;

        cvs = GetComponent<CanvasGroup>();
        //ShowUpgrade(null);

        ShowPanel(false);
    }

    public void ShowUpgrade(Action onEndUpgrade = null)
    {
        GameManager.Instance.timeScale = 0f;

        List<ChoiceSet> choices = uh.GetRandomChoices(showPanelCount);

        ShowPanel(true);
        ShowSelectPanel(choices, onEndUpgrade);
    }

    private void ShowSelectPanel(List<ChoiceSet> choices, Action onEndUpgrade)
    {
        selectText.transform.localScale = Vector3.one;

        selectTextTween.Kill();
        selectTextTween = selectText.transform.DOScale(Vector3.one * 1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].btn.onClick.RemoveAllListeners();
            panels[i].gameObject.SetActive(i < showPanelCount);

            if(i < showPanelCount)
            {
                int a = i;

                ChoiceInfo merit = choices[a].merit;
                ChoiceInfo demerit = choices[a].demerit;

                panels[a].SetPanel(merit.choiceData, demerit.choiceData);
                panels[a].ShowHighlight(() => 
                {
                    panels[a].btn.onClick.AddListener(() =>
                    {
                        merit.SetChoice();
                        demerit.SetChoice();

                        GameManager.Instance.timeScale = 1f;

                        ShowPanel(false);

                        onEndUpgrade?.Invoke();
                    });
                });
            }
        }
    }

    private void ShowPanel(bool enable)
    {
        cvs.alpha = enable ? 1f : 0f;
        cvs.interactable = enable;
        cvs.blocksRaycasts = enable;

        for (int i = 0; i < upgradeParticles.Count; i++)
        {
            if (enable)
            {
                upgradeParticles[i].Play();
            }
            else 
            {
                upgradeParticles[i].Stop();
            }
        }
    }
}
