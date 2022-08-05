using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BlueArchive : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_BlueArchive";
    private Bullet_BlueArchive bullet;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && bullet != null)
        {
            bullet.SetDisable();
        }
    }

    public override void Shoot(Vector3 shootDir)
    {
        if(bullet != null)
        {
            bullet.SetDisable();
        }
        bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet_BlueArchive>();
        bullet.bulletData = bulletData;
        //bullet.transform.position = shootPos.position;

        bullet.SetRenderer(shootPos.position);
        bullet.SetOwner(!isPlayer);
        

<<<<<<< HEAD
=======
        //print($"총알 발싸 히히히히히 데미지 : {damage} ");
>>>>>>> c13a2966cee157ab595c867e967111d467713a37

        GameManager.Instance.soundHandler.Play(shotSFXName);
    }
}
