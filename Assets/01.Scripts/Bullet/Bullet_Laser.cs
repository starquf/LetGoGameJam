using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Laser : Bullet
{

    public override void Spawned()
    {
        base.Spawned();
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
