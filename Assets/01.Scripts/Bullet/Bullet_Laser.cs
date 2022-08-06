using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Laser : Bullet
{
    public Vector3 playerBulletSize;
    public Vector3 enemyBulletSize;

    public override void Spawned()
    {
        base.Spawned();
        transform.localScale = isEnemyBullet ? enemyBulletSize : playerBulletSize;
    }

    public override void Despawned()
    {
        base.Despawned();
    }

    protected override void BulletMove()
    {
        base.BulletMove();
    }

    public void SetColor(Color c)
    {
        sr.color = c;
    }
   
}
