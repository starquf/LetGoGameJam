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
        float rotate = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        float angle = collectionRate / count;

        Vector2 dir = Vector2.zero;

        for (int i = 0; i < count; i++)
        {
            Bullet bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet>();
            bullet.transform.position = shootPos.transform.position;

            dir.x = Mathf.Cos((angle * i + rotate + angle / 2 - a / 2) * Mathf.Deg2Rad);
            dir.y = Mathf.Sin((angle * i + rotate + angle / 2 - a / 2) * Mathf.Deg2Rad);

            bullet.ChangeSpeed(Random.Range(13f, 15f));
            bullet.ChangeDir(dir);
            bullet.bulletData = bulletData;
        }

        print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);

        PlayMuzzleEffect();
        PlayBounceEffect();
    }
}
