using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AWM : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_AWM";

    public override void Shoot(Vector3 shootDir)
    {
        print("슛");
        GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);

        bulletObj.transform.position = shootPos.position;

        bulletObj.GetComponent<Bullet>().ChangeDir(shootDir.normalized);

        //print($"총알 발싸 히히히히히 데미지 : {damage} ");
    }
}
