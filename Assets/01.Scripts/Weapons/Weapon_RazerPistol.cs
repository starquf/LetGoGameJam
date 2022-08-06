using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_RazerPistol : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_Laser";
    private readonly List<Color> colorList = new List<Color>() { new Color(1f, 0.25f, 0.21f), new Color(1f, 0.64f, 0.21f), new Color(1f, 0.96f, 0.21f), new Color(0.78f, 1f, 0.21f), new Color(0.21f, 0.85f, 1f), new Color(0.28f, 0.21f, 1f), new Color(0.61f, 0.21f, 1f) };
    public int curIndex = 0;

    public override void Shoot(Vector3 shootDir)
    {

        Bullet_Laser bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet_Laser>();

        bullet.transform.position = shootPos.position;

        if (isPlayer)
        {
            bullet.SetColor(colorList[curIndex % 7]);
        }
        else
        {
            bullet.SetColor(Color.white);
        }

        bullet.ChangeDir(shootDir.normalized);
        float coll = collectionRate / 2f;
        bullet.bulletData = bulletData;
        bullet.ChangeDir(shootDir.normalized);
        bullet.RotateAngle(Random.Range(-coll, coll));
        bullet.ChangeSpeed(Random.Range(13f, 15f));
        bullet.SetOwner(!isPlayer);
        bullet.AddBulletIron(bulletIron);
        bullet.SetDamage(damage);
        //print($"총알 발싸 히히히히히 데미지 : {damage} ");

        curIndex++;
        GameManager.Instance.soundHandler.Play(shotSFXName);

        PlayBounceEffect();
    }
}
