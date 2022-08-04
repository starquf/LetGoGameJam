using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_M1911 : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public override void Shoot(Vector3 shootDir)
    {
        GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bulletObj.transform.position = shootPos.position;

        float coll = collectionRate / 2f;

        bullet.ChangeDir(shootDir.normalized);
        bullet.RotateAngle(Random.Range(-coll, coll));
        bullet.ChangeSpeed(Random.Range(13f, 15f));

        //print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);
    }
}