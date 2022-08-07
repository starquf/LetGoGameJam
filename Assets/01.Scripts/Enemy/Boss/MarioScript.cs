using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class MarioScript : MonoBehaviour, IPoolableComponent
{
    private Animator anim;
    private SpriteRenderer sr;

    private string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public BulletSO bulletData;

    private int onAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        onAttack = Animator.StringToHash("OnAttack");
    }

    public void Play(Action onEndPlay = null)
    {
        sr.DOFade(1f, 0.5f).SetEase(Ease.Linear).From(0f)
            .OnComplete(() =>
            {
                StartCoroutine(Logic(onEndPlay));
            });
    }

    private IEnumerator Logic(Action onEndPlay)
    {
        WaitForSeconds pFiveSecWait = new WaitForSeconds(0.5f);

        yield return pFiveSecWait;
        yield return pFiveSecWait;
        yield return pFiveSecWait;

        ShootRoundShotGun(3, 15f, 30f + Random.Range(-5f, 5f));
        yield return pFiveSecWait;
        ShootRoundShotGun(3, 15f, 45f + Random.Range(-5f, 5f));
        yield return pFiveSecWait;
        ShootRoundShotGun(3, 15f, 55f + Random.Range(-5f, 5f));
        yield return pFiveSecWait;
        ShootRoundShotGun(3, 15f, 45f + Random.Range(-5f, 5f));
        yield return pFiveSecWait;
        ShootRoundShotGun(3, 15f, 30f + Random.Range(-5f, 5f));
        yield return pFiveSecWait;

        StopMario();

        onEndPlay?.Invoke();
    }

    public void StopMario()
    {
        sr.DOFade(0f, 0.5f).SetEase(Ease.Linear).From(1f).OnComplete(() =>
        {
            SetDisable();
        });
    }

    private void ShootRoundShotGun(int count, float speed, float rotate)
    {
        float angle = 360f / count;
        Vector2 dir = Vector2.zero;

        for (int i = 0; i < count; i++)
        {
            ShootShotGun(10, speed, (angle * i) + rotate);
        }

        anim.SetTrigger(onAttack);
    }

    private void ShootShotGun(int count, float speed, float rotate)
    {
        float a = 100f;
        float angle = a / count;

        Vector2 dir = Vector2.zero;

        for (int i = 0; i < count; i++)
        {
            Bullet bullet = GameObjectPoolManager.Instance.GetGameObject(BULLET_PATH, null).GetComponent<Bullet>();
            bullet.transform.position = transform.position;

            dir.x = Mathf.Cos((angle * i + rotate + angle / 2 - a / 2) * Mathf.Deg2Rad);
            dir.y = Mathf.Sin((angle * i + rotate + angle / 2 - a / 2) * Mathf.Deg2Rad);

            bullet.SetScale(2.0f);

            bullet.ChangeSpeed(speed);
            bullet.ChangeDir(dir);
            bullet.SetOwner(true, WeaponType.M870);
            bullet.AddBulletIron(0);
            bullet.SetLifeTime(6f);
            bullet.SetDamage(1);

            bullet.bulletData = bulletData;
        }
    }

    public void Despawned()
    {
        
    }

    public void Spawned()
    {
        
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
