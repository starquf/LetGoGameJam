using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Ddak : Weapon
{
    private readonly string BULLET_PATH = "Assets/05.Prefabs/Bullets/Bullet.prefab";

    public override void Shoot(Vector2 shootDir)
    {
        GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);

        print($"총알 발싸 히히히히히 데미지 : {damage} ");
    }
}
