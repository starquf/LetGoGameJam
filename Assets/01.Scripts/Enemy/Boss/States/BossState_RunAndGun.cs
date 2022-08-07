using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossState_RunAndGun : BossState
{
    private Transform playerTrans = null;

    public float moveSpeed = 3f;

    private int isMove;

    private string BULLET_PATH = "Prefabs/Bullets/Bullet";

    public BulletSO bulletData;

    private Coroutine moveCor;

    private void Start()
    {
        playerTrans = GameManager.Instance.playerTrm;

        isMove = Animator.StringToHash("IsMove");
    }

    public override void Play(Action onEndState = null)
    {
        StartCoroutine(Logic(onEndState));
    }

    private IEnumerator Logic(Action onEndState)
    {
        WaitForSeconds oneSecWait = new WaitForSeconds(1f);

        yield return oneSecWait;
        yield return oneSecWait;

        moveCor = StartCoroutine(MoveToPlayer());

        yield return oneSecWait;

        ShootShotGunToPlayer(10, 15f);
        yield return oneSecWait;
        ShootShotGunToPlayer(10, 15f);
        yield return oneSecWait;
        ShootShotGunToPlayer(7, 20f);
        yield return oneSecWait;

        StopMove();

        onEndState?.Invoke();
    }

    private IEnumerator MoveToPlayer()
    {
        while (true)
        {
            Vector2 dir = (playerTrans.position - transform.position).normalized;

            rb.velocity = dir * moveSpeed;

            anim.SetBool(isMove, true);

            yield return null;
        }
    }

    private void ShootShotGunToPlayer(int count, float speed)
    {
        float a = 45f;

        Vector3 shootDir = (playerTrans.position - transform.position).normalized;
        float rotate = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
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

    private void StopMove()
    {
        StopCoroutine(moveCor);

        rb.velocity = Vector2.zero;
        anim.SetBool(isMove, false);
    }
}
