using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : UIBase
{
    private Slider expSlider;

    public override void Init()
    {
        mydataType = UIDataType.Exp;
        expSlider = GetComponent<Slider>();
    }

    public override void SetData(string data)
    {
        expSlider.value = float.Parse(data.Trim());
    }
}

