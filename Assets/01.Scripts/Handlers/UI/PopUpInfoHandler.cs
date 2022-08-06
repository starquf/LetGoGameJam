using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInfoHandler : Handler
{
    [SerializeField]
    private List<GameObject> uiItems;

    private List<ChoiceInfoScripts> merits = new List<ChoiceInfoScripts>();
    private List<ChoiceInfoScripts> Demerits = new List<ChoiceInfoScripts>();
    public override void OnAwake()
    {
        GameManager.Instance.popUpInfoHandler = this;

    }

    public override void OnStart()
    {

    }

    public void SendData(UIChoiceType dataType, ChoiceInfo data)
    {
        ChoiceInfoScripts a;
        switch(dataType)
        {
            case UIChoiceType.Merit:
                foreach (var merit in merits)
                {
                    if (merit.name.Equals(data.choiceData.name))
                    {
                        merit.LevelUp(data.choiceData.level.ToString(),data.isInfinityLevel);
                        return;
                    }
                }
                a = GameObjectPoolManager.Instance.GetGameObject("Prefabs/UI/MeritInfo", uiItems[0].transform).GetComponent<ChoiceInfoScripts>();
                a.SetData(data.choiceData.icon, data.choiceData.level.ToString(), data.isInfinityLevel);
                a.name = data.choiceData.name;
                merits.Add(a);
                break;
            case UIChoiceType.DeMerit:
                foreach (var demerit in Demerits)
                {
                    if (demerit.name.Equals(data.choiceData.name))
                    {
                        demerit.LevelUp(data.choiceData.level.ToString(), data.isInfinityLevel);
                        return;
                    }
                }

                a = GameObjectPoolManager.Instance.GetGameObject("Prefabs/UI/DemeritInfo", uiItems[1].transform).GetComponent<ChoiceInfoScripts>();
                a.SetData(data.choiceData.icon, data.choiceData.level.ToString(), data.isInfinityLevel);
                a.name = data.choiceData.name;
                Demerits.Add(a);
                break;
        }
    }
}

[System.Serializable]
public enum UIChoiceType
{
    Merit,
    DeMerit
}