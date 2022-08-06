using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weaponimg : UIBase
{
    [SerializeField]
    private Image image;

    public override void Init()
    {
        mydataType = UIDataType.Weapon;
    }

    public override void SetData(string data)
    {
        Vector2 delta = new Vector2();
        float x = 0f;
        float y = 0f;
        RectTransform imagerect = image.GetComponent<RectTransform>();
        image.sprite = Resources.Load<Sprite>($"Sprites/Weapon/{data}");

        switch ((WeaponType)System.Enum.Parse(typeof(WeaponType), data))
        {
            case WeaponType.Ak47:
                delta = new Vector2(25, 12) * 5f;
                x = 0f;
                y = -5f;
                break;
            case WeaponType.AWM:
                delta = new Vector2(55, 14) * 2.5f;
                x = -2f;
                y = -2f;
                break;
            case WeaponType.BlueArchive:
                delta = new Vector2(38, 14) * 4f;
                x = 0f;
                y = -10f;
                break;
            case WeaponType.M1911:
                delta = new Vector2(17, 12) * 4f;
                x = 10f;
                y = 0f;
                break;
            case WeaponType.M870:
                delta = new Vector2(45, 12) * 2.5f;
                x = 0f;
                y = 0f;
                break;
            case WeaponType.MagicBar:
                delta = new Vector2(58, 17) * 2.5f;
                x = 5f;
                y = 0f;
                break;
            case WeaponType.MP7:
                delta = new Vector2(35, 19) * 4f;
                x = 5f;
                y = -5f;
                break;
            case WeaponType.RazerPistol:
                delta = new Vector2(23, 14) * 6f;
                x = 15f;
                y = 5f;
                break;
        }

        imagerect.anchoredPosition3D = new Vector3(x, y);
        imagerect.sizeDelta = delta;
    }
}
