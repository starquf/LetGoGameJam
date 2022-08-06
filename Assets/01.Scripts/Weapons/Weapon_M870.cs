using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_M870 : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public override void Shoot(Vector3 shootDir)
    {
        int count = 4;
        float a = collectionRate;

        if (isPlayer)
        {
            a += a * GameManager.Instance.playerTrm.GetComponent<PlayerStat>().collectionRate;
        }

        if (isPlayer)
        {
            count = 7;
            a = a - 10f;
        }

        float rotate = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        float angle = a / count;

        Vector2 dir = Vector2.zero;

        for (int i = 0; i < count; i++)
        {
            Bullet bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet>();
            bullet.transform.position = shootPos.transform.position;
            if(isPlayer)
            {
                bullet.transform.localScale *= transform.lossyScale.x;
            }
            else
            {
                bullet.transform.localScale *= transform.lossyScale.x / transform.localScale.x;
            }

            dir.x = Mathf.Cos((angle * i + rotate + angle / 2 - a / 2) * Mathf.Deg2Rad);
            dir.y = Mathf.Sin((angle * i + rotate + angle / 2 - a / 2) * Mathf.Deg2Rad);

            bullet.ChangeSpeed(Random.Range(13f, 15f));
            bullet.ChangeDir(dir);
            bullet.SetOwner(!isPlayer, weaponType);
            bullet.AddBulletIron(bulletIron);
            bullet.SetDamage(damage);

            bullet.bulletData = bulletData;
        }

        //print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);

        PlayMuzzleEffect();
        PlayCatridgeEffect();
        PlayBounceEffect();
    }
}
