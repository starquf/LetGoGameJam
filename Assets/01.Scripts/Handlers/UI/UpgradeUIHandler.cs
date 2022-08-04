using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIHandler : MonoBehaviour
{
    private CanvasGroup cvs;

    public List<SelectUIPanel> panels = new List<SelectUIPanel>();

    public int showPanelCount = 2;

    private void Start()
    {
        cvs = GetComponent<CanvasGroup>();
        //ShowUpgrade(null);
    }

    public void ShowUpgrade(Action onEndUpgrade)
    {
        Time.timeScale = 0f;

        ShowPanel(true);
        ShowSelectPanel();
    }

    private void ShowSelectPanel()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].gameObject.SetActive(i < showPanelCount);
        }
    }

    private void ShowPanel(bool enable)
    {
        cvs.alpha = enable ? 1f : 0f;
        cvs.interactable = enable;
        cvs.blocksRaycasts = enable;
    }
}
