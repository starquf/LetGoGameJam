using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : UIBase
{
    [SerializeField]
    private Text killCounterText;
    private int killCount = 0;

    public override void Init()
    {
        mydataType = UIDataType.Killcount;
    }

    public override void SetData(string data)
    {
        killCount += int.Parse(data.Trim());
        killCounterText.text = "" + killCount;
    }

}
