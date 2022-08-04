using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableComponent
{
    private Rigidbody2D rb = null;

    private BulletState currentState = BulletState.MoveForward;

    public float bulletDamage = 1f;

    public float bulletSpeed = 30f;
    private float currSpeed = 0f;

    // 적의 총알인가?
    public bool isEnemyBullet = true;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currSpeed = bulletSpeed;
    }

    public override void Despawned()
    {
        ChangeState(BulletState.MoveForward);
    }

    public override void Spawned()
    {
        currSpeed = bulletSpeed;
    }

    protected void FixedUpdate()
    {
        CheckState();
        CheckTransform();
    }

    protected virtual void BulletMove()
    {
        rb.velocity = transform.right * currSpeed;
    }

    protected virtual void CheckTransform()     // 화면 밖으로 나갔는가?
    {
        /*
        if (transform.position.x > GameManager.Instance.maxSize.x + 5f || transform.position.x < GameManager.Instance.minSize.x - 5f)
        {
            SetDisable();
        }

        if (transform.position.y > GameManager.Instance.maxSize.y + 2f || transform.position.y < GameManager.Instance.minSize.y - 2f)
        {
            SetDisable();
        }
        */
    }

    protected virtual void ChangeState(BulletState state)
    {
        currentState = state;
    }

    protected virtual void CheckState()
    {
        switch (currentState)
        {
            case BulletState.MoveForward:
                BulletMove();
                break;

            case BulletState.Stop:

                break;
        }
    }

    #region ChangeDirections
    public virtual void ChangeDir(Vector3 dir)      // dir방향으로 회전
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void ChangeDir(Vector3 dir, float t)     // 시간차로 dir방향으로 회전
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        StartCoroutine(Timer(() => transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward), t));
    }

    /*
    public virtual void ChangeDirToPlayer()     // 플레이어 방향으로 회전
    {
        Vector3 dir = GameManager.Instance.playerPos.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void ChangeDirToPlayer(float t)     // 시간차로 플레이어 방향으로 회전
    {
        StartCoroutine(Timer(() =>
        {
            Vector3 dir = GameManager.Instance.playerPos.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        , t));
    }
    */

    public virtual void RotateAngle(float angle)    // angle만큼 z축 회전
    {
        transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + angle, Vector3.forward);
    }

    public virtual void RotateAngle(float angle, float t)   // 시간차로 angle만큼 z축 회전
    {
        StartCoroutine(Timer(() => transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + angle, Vector3.forward), t));
    }

    public virtual void ChangeSpeed(float value)
    {
        currSpeed = value;
    }

    public virtual void ChangeSpeed(float value, float t)
    {
        StartCoroutine(Timer(() => currSpeed = value, t));
    }

    public virtual void PlusSpeed(float value)
    {
        currSpeed += value;
    }

    public virtual void PlusSpeed(float value, float t)
    {
        StartCoroutine(Timer(() => currSpeed += value, t));
    }
    #endregion

    protected IEnumerator Timer(Action action, float t)
    {
        yield return new WaitForSeconds(t);
        action.Invoke();
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }
}