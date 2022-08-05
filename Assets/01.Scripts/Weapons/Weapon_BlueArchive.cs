using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BlueArchive : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_BlueArchive";
    private readonly string EFFECT_PATH = "Prefabs/Effect/LaserEffect";

    private Bullet_BlueArchive bullet;
    private GameObject effectObj;


    public override void Despawned()
    {
        if (isPlayer)
        {
            if (bullet != null)
            {
                print(1);
                bullet.SetDisable();
                bullet = null;

            }
            if (effectObj != null)
            {
                print(2);
                GameObjectPoolManager.Instance.UnusedGameObject(effectObj);
                effectObj.SetActive(false);
                effectObj = null;
            }
        }
    }

    private void Update()
    {
        if (isPlayer)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (bullet != null)
                {
                    print(3);
                    bullet.SetDisable();
                    bullet = null;

                }
                if (effectObj != null)
                {
                    print(4);
                    GameObjectPoolManager.Instance.UnusedGameObject(effectObj);
                    effectObj.SetActive(false);
                    effectObj = null;
                }
            }
        }
    }

    public void EnemyShootStop()
    {
        if (bullet != null)
        {
            //print(3);
            bullet.SetDisable();
            bullet = null;

        }
        if (effectObj != null)
        {
            //print(4);
            GameObjectPoolManager.Instance.UnusedGameObject(effectObj);
            effectObj.SetActive(false);
            effectObj = null;
        }
    }

    public override void Shoot(Vector3 shootDir)
    {
        if (isPlayer)
        {
            if (bullet == null)
            {
                bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet_BlueArchive>();
                bullet.bulletData = bulletData;
                bullet.SetDamage(damage);
            }

            bullet.SetRenderer(shootPos.position);
            bullet.SetOwner(!isPlayer);
            bullet.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (effectObj == null)
            {
                effectObj = GameObjectPoolManager.Instance.GetGameObject(EFFECT_PATH, null);
                effectObj.GetComponent<ParticleSystem>().Play();
            }

            effectObj.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);



            GameManager.Instance.soundHandler.Play(shotSFXName);

            PlayBounceEffect();
        }
        else
        {
            if (bullet == null)
            {
                bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet_BlueArchive>();
                bullet.bulletData = bulletData;

            }

            bullet.SetOwner(!isPlayer);
            bullet.SetTarget(transform.position+ shootDir);
            bullet.SetRenderer(shootPos.position);

            if (effectObj == null)
            {
                effectObj = GameObjectPoolManager.Instance.GetGameObject(EFFECT_PATH, null);
                effectObj.GetComponent<ParticleSystem>().Play();
            }

            effectObj.transform.position = transform.position+ shootDir;



            GameManager.Instance.soundHandler.Play(shotSFXName);

            PlayBounceEffect();
        }
    }

   
}
