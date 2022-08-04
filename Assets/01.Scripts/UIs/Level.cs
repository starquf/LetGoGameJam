using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : UIBase
{
    private Text levelText;

    public override void Init()
    {
        mydataType = UIDataType.Level;
        levelText = GetComponent<Text>();
    }

    public override void SetData(string data)
    {
        levelText.text = data.Trim();
    }
}
