using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Weapon : MonoBehaviour, IPoolableComponent
{
    private readonly string MUZZLE_EFFECT_PATH = "Prefabs/Effect/MuzzleEffect";

    public string shotSFXName;

    public float damage = 1f;
    public int maxBullet = 5;
    public int bulletIron = 0;

    public bool isInfiniteBullet;

    public float fireRate = 0f;
    public float collectionRate = 0f;

    public bool isAuto = false;

    public bool isPlayer = false;
    public bool isGround = false;

    public float offset = 1f;

    public WeaponType weaponType;

    public Transform shootPos;

    public BulletSO bulletData;

    public SpriteRenderer sr;

    public bool isNoShakeWeapon = false;

    private Effect switchEffect = null;

    public GameObject muzzleFlashEffect;

    public float muzzleFlashTime = 0.07f;
    private WaitForSeconds muzzleWait = new WaitForSeconds(0.07f);
    private Coroutine muzzleCor = null;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        muzzleFlashEffect.SetActive(false);

        muzzleWait = new WaitForSeconds(muzzleFlashTime);
    }

    public abstract void Shoot(Vector3 shootDir);

    public virtual void Despawned()
    {
        muzzleFlashEffect.SetActive(false);
    }

    public void Spawned()
    {
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        sr.color = Color.white;
        sr.flipY = false;
        bulletIron = 0;
    }


    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    public void SetSwichAnim(bool enable)
    {
        if(enable)
        {
            if (switchEffect != null)
                return;
            switchEffect = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Effect/SwitchEffect", null).GetComponent<Effect>();
            switchEffect.SetPosition(transform.position + Vector3.up);
            switchEffect.Play();
        }
        else
        {
            if(switchEffect != null)
                switchEffect.SetDisable();
            switchEffect = null;
        }
    }

    protected void PlayBounceEffect()
    {
        GameManager.Instance.effectHandler.SetEffect(EffectType.BounceHorizontal,sr);
    }

    protected void PlayMuzzleEffect()
    {
        Effect effect = GameObjectPoolManager.Instance.GetGameObject(MUZZLE_EFFECT_PATH, null).GetComponent<Effect>();
        effect.SetPosition(shootPos.position);
        effect.Play();

        if (muzzleCor != null)
        {
            StopCoroutine(muzzleCor);
        }

        muzzleCor = StartCoroutine(MuzzleFlashEffect());
    }

    private IEnumerator MuzzleFlashEffect()
    {
        muzzleFlashEffect.SetActive(true);
        yield return muzzleWait;
        muzzleFlashEffect.SetActive(false);
    }
}
