using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSprite
{
    public WeaponType type;
    public Sprite sprite;
}

public class WeaponSpriteContainer : Handler
{
    public List<WeaponSprite> weaponSprites = new List<WeaponSprite>();
    public Dictionary<WeaponType, Sprite> weaponSpriteDic = new Dictionary<WeaponType, Sprite>();

    public override void OnAwake()
    {
        GameManager.Instance.weaponSpriteContainer = this;
        for (int i = 0; i < weaponSprites.Count; i++)
        {
            weaponSpriteDic.Add(weaponSprites[i].type, weaponSprites[i].sprite);
        }
    }

    public override void OnStart()
    {

    }
}
