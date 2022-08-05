using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_M870 : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public override void Shoot(Vector3 shootDir)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);

            bulletObj.transform.position = shootPos.position;
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            float coll = collectionRate / 2f;

            bullet.ChangeDir(shootDir.normalized);
            bullet.RotateAngle(Random.Range(-coll, coll));
            bullet.ChangeSpeed(Random.Range(13f, 15f));
            bullet.SetOwner(!isPlayer);

        }

        print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);
    }
}
