using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : UIBase
{
    [SerializeField]
    private Text killCounterText;

    public override void Init()
    {
        mydataType = UIDataType.Killcount;
    }

    public override void SetData(string data)
    {
        killCounterText.text = data;
    }

}
