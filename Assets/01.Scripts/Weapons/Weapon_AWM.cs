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
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.transform.position = shootPos.position;

        float coll = collectionRate / 2f;

        bullet.ChangeDir(shootDir.normalized);
        bullet.RotateAngle(Random.Range(-coll, coll));
        bullet.ChangeSpeed(Random.Range(10f, 13f));

        print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);
    }
}