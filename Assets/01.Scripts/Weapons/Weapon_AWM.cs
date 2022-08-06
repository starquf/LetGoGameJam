using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AWM : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_AWM";
 

    public override void Shoot(Vector3 shootDir)
    {
        GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);


        Bullet bullet = bulletObj.GetComponent<Bullet>();
        
        bullet.bulletData = bulletData;

        bullet.SetOwner(!isPlayer, weaponType);
        bulletObj.transform.position = shootPos.position;

        float coll = collectionRate / 2f;

        if (isPlayer)
        {
            coll += GameManager.Instance.playerTrm.GetComponent<PlayerStat>().collectionRate / 2f;
        }

        bullet.ChangeDir(shootDir.normalized);
        bullet.RotateAngle(Random.Range(-coll, coll));
        bullet.SetDamage(damage);


        //print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);

        PlayBounceEffect();
    }
}
