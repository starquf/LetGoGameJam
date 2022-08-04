using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weaponimg : UIBase
{

    private Image image;

    public override void Init()
    {
        mydataType = UIDataType.Weapon;
    }

    public override void SetData(string data)
    {
        image.sprite = Resources.Load<Sprite>(data);
    }
}
