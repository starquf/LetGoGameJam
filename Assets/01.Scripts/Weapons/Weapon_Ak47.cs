using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Ak47 : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public override void Shoot(Vector3 shootDir)
    {
        GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.isEnemyBullet = !isPlayer;

        bulletObj.transform.position = shootPos.position;

        bullet.ChangeDir(shootDir.normalized);

        print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);
    }
}
