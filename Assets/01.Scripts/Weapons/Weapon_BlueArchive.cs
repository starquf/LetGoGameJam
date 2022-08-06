using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BlueArchive : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_BlueArchive";
    private readonly string EFFECT_PATH = "Prefabs/Effect/LaserEffect";
    private readonly string AFTER_EFFECT_PATH = "Prefabs/Effect/LaserAfterEffect";

    private Bullet_BlueArchive bullet;
    private GameObject effectObj;
    private GameObject afterEffectObj;


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
            if (afterEffectObj != null)
            {
                //print(4);
                GameObjectPoolManager.Instance.UnusedGameObject(afterEffectObj);
                afterEffectObj.SetActive(false);
                afterEffectObj = null;
            }
        }
    }

    protected override void Update()
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
                if (afterEffectObj != null)
                {
                    //print(4);
                    GameObjectPoolManager.Instance.UnusedGameObject(afterEffectObj);
                    afterEffectObj.SetActive(false);
                    afterEffectObj = null;
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
        if (afterEffectObj != null)
        {
            //print(4);
            GameObjectPoolManager.Instance.UnusedGameObject(afterEffectObj);
            afterEffectObj.SetActive(false);
            afterEffectObj = null;
        }
    }

    public override void Shoot(Vector3 shootDir)
    {
        if (isPlayer)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           

            if (bullet == null)
            {
                bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet_BlueArchive>();
                bullet.bulletData = bulletData;
                bullet.SetDamage(damage);
            }

            bullet.SetOwner(!isPlayer, weaponType);
            bullet.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            bullet.SetRenderer(shootPos.position, transform.lossyScale.x / transform.localScale.x / 2);

            if (effectObj == null)
            {
                effectObj = GameObjectPoolManager.Instance.GetGameObject(EFFECT_PATH, null);
                effectObj.GetComponent<ParticleSystem>().Play();
            }

            effectObj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            if (afterEffectObj == null)
            {
                afterEffectObj = GameObjectPoolManager.Instance.GetGameObject(AFTER_EFFECT_PATH, null);
                afterEffectObj.GetComponent<ParticleSystem>().Play();
            }

            effectObj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);



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

            bullet.SetOwner(!isPlayer, weaponType);
            bullet.SetTarget(transform.position+ shootDir);
            bullet.SetRenderer(shootPos.position, transform.lossyScale.x / transform.localScale.x);


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
