using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon_MagicBarOfAllah : Weapon
{
    private readonly string BULLET_PATH = "Prefabs/Bullets/Bullet_Allah";

    [SerializeField]
    private Sprite reloadedSprite;
    [SerializeField]
    private Sprite firedSprite;

    public override void Shoot(Vector3 shootDir)
    {
        GameObject bulletObj = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetOwner(!isPlayer, weaponType);
        bulletObj.transform.position = shootPos.position;
        bullet.transform.localScale *= transform.lossyScale.x / transform.localScale.x;
        bullet.bulletData = bulletData;
        bullet.ChangeDir(shootDir.normalized);
        bullet.SetDamage(damage);

        //print($"총알 발싸 히히히히히 데미지 : {damage} ");

        GameManager.Instance.soundHandler.Play(shotSFXName);

        PlayMuzzleEffect();
        PlayBounceEffect();
        StartCoroutine(FireSpriteChange());
    }

    public IEnumerator FireSpriteChange()
    {
        sr.sprite = firedSprite;
        yield return new WaitForSeconds(fireRate);
        sr.sprite = reloadedSprite;
    }
}
