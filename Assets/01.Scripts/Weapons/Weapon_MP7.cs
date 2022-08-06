using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_MP7 : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public override void Shoot(Vector3 shootDir)
    {
        GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bulletObj.transform.position = shootPos.position;
        bullet.ChangeDir(shootDir.normalized);

        float coll = collectionRate / 2f;

        if (isPlayer)
        {
            coll += coll * GameManager.Instance.playerTrm.GetComponent<PlayerStat>().collectionRate;
        }

        bullet.bulletData = bulletData;
        if (isPlayer)
        {
            bullet.transform.localScale *= transform.lossyScale.x;
        }
        else
        {
            bullet.transform.localScale *= transform.lossyScale.x / transform.localScale.x;
        }
        bullet.ChangeDir(shootDir.normalized);
        bullet.RotateAngle(Random.Range(-coll, coll));
        bullet.ChangeSpeed(Random.Range(13f, 15f));
        bullet.SetOwner(!isPlayer, weaponType);
        bullet.AddBulletIron(bulletIron);
        bullet.SetDamage(damage);
        //print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);

        PlayMuzzleEffect();
        PlayCatridgeEffect();
        PlayBounceEffect();
    }
}
