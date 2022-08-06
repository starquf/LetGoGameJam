using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Weapon : MonoBehaviour, IPoolableComponent
{
    private readonly string MUZZLE_EFFECT_PATH = "Prefabs/Effect/MuzzleEffect";
    private readonly string CATRIDGE_EFFECT_PATH = "Prefabs/Effect/CatridgeEffect";

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
    public Transform catridgeTrm;

    public BulletSO bulletData;

    public SpriteRenderer sr;

    public bool isNoShakeWeapon = false;

    private Effect switchEffect = null;

    public GameObject muzzleFlashEffect;

    public float muzzleFlashTime = 0.07f;
    private WaitForSeconds muzzleWait = new WaitForSeconds(0.07f);
    private Coroutine muzzleCor = null;

    private float defaultDestroyTimer = 30f;
    private float destoryTimer = 0;
    private float fadeVal = 0;

    private Tween weaponUpTween;

    protected Vector2 defaultScale;
    protected Vector2 defaultlossyScale;

    protected virtual void Awake()
    {
        defaultScale = transform.localScale;
        defaultlossyScale = transform.lossyScale;
       // sr = GetComponent<SpriteRenderer>();

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
        transform.localScale = defaultScale;
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        sr.material.SetInt("_IsActive", 0);
        sr.color = Color.white;
        sr.GetComponentInChildren<SpriteOutline>().outlineSize = 0;
        sr.flipY = false;
        bulletIron = 0;
        fadeVal = 100;
        destoryTimer = 100;
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
        GameManager.Instance.effectHandler.SetEffect(EffectType.BounceHorizontal,sr,isPlayer);
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

    protected void PlayCatridgeEffect()
    {
        Effect effect = GameObjectPoolManager.Instance.GetGameObject(CATRIDGE_EFFECT_PATH, null).GetComponent<Effect>();
        effect.SetPosition(catridgeTrm.position);
        if(sr.flipY)
        {
            effect.SetRotation(new Vector3(0f, 0f, GameManager.Instance.playerTrm.Find("WeaponHolder").transform.rotation.eulerAngles.z - 180));
        }
        else
        {
            effect.SetRotation(new Vector3(0f, 0f, GameManager.Instance.playerTrm.Find("WeaponHolder").transform.rotation.eulerAngles.z));
        }
        effect.Play();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.timeScale <= 0f) 
            return;

        if (isGround)
        {
            destoryTimer -= Time.deltaTime;
            if (destoryTimer < 0f)
            {
                SetDisable();
            }
            else if (destoryTimer < 5)
            {
                float speed = Mathf.Clamp((50 / destoryTimer), 10, 30);
                fadeVal += Time.deltaTime * speed;
                sr.color = new Color(1, 1, 1, Mathf.Cos(fadeVal));
            }
        }
    }

    public void SetDestoryTimer(float time)
    {
        sr.GetComponentInChildren<SpriteOutline>().outlineSize = 1;

        defaultDestroyTimer = time;
        destoryTimer = defaultDestroyTimer;
        fadeVal = 0;
    }

    private IEnumerator MuzzleFlashEffect()
    {
        muzzleFlashEffect.SetActive(true);
        yield return muzzleWait;
        muzzleFlashEffect.SetActive(false);
    }
}
