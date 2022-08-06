using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIHandler : Handler
{
    [SerializeField]
    private List<UIBase> uiItems;

    public override void OnAwake()
    {
        GameManager.Instance.inGameUIHandler = this;

        foreach (var ui in uiItems)
        {
            ui.Init();
        }
    }

    public override void OnStart()
    { 

    }

    public void SendData(UIDataType dataType, string data)
    {
        foreach (var ui in uiItems)
        {
            if(ui.mydataType.Equals(dataType))
            {
                ui.SetData(data);
            }
        }
    }
}

[System.Serializable]
public enum UIDataType
{
    Exp,
    Level,
    Hp,
    Ammo,
    Weapon,
    Score,
    Killcount,
    Enemy,
    Dialog
}