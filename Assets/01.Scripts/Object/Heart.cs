using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IPoolableComponent
{
    public int healVal = 1;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;

    public bool isFollowPlayer = false;

    private float followAcc = 0f;
    public float followSpeed = 2f;

    private bool isTimer = false;

    private float defaultDestroyTimer = 30f;
    private float destoryTimer = 0;
    private float fadeVal = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Despawned()
    {

        GameManager.Instance.allItemListRemove(this);
    }

    public void Spawned()
    {
        GameManager.Instance.allItemListAdd(this);
        fadeVal = 100;
        destoryTimer = 100;
        isTimer = false;
        sr.color = Color.white;
        followAcc = 0f;

        sr.material.SetInt("_IsActive", 1);

        isFollowPlayer = false;
    }

    public void SetDisable()
    {
        GameManager.Instance.allItemListRemove(this);
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }

    private void Update()
    {
        if (GameManager.Instance.timeScale <= 0f)
            return;

        if (isTimer)
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

    private void FixedUpdate()
    {
        if (GameManager.Instance.timeScale <= 0f)
            return;

        if (isFollowPlayer)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 dir = (GameManager.Instance.playerTrm.position - transform.position).normalized;

        rb.velocity = dir * followSpeed * followAcc;
        followAcc += Time.deltaTime;
    }

    public void SetDestoryTimer(float time)
    {
        defaultDestroyTimer = time;
        destoryTimer = defaultDestroyTimer;
        fadeVal = 0;
        isTimer = true;
    }
}
