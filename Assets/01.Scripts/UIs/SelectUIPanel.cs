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

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void SetPanel(ChoiceData merit, ChoiceData demerit)
    {
        meritPanel.icon.sprite = merit.icon;
        meritPanel.name.text = merit.name;
        meritPanel.des.text = merit.des;
        meritPanel.level.text = merit.level.ToString();

        demeritPanel.icon.sprite = demerit.icon;
        demeritPanel.name.text = demerit.name;
        demeritPanel.des.text = demerit.des;
        demeritPanel.level.text = demerit.level.ToString();
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
