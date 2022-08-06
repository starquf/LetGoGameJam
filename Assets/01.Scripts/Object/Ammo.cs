using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IPoolableComponent
{
    public float addAmmoPersent = 5f;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;

    private bool isTimer = false;

    private float defaultDestroyTimer = 30f;
    private float destoryTimer = 0;
    private float fadeVal = 0;

    public bool isFollowPlayer = false;

    private float followAcc = 0f;
    public float followSpeed = 2f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Despawned()
    {

    }

    public void Spawned()
    {
        fadeVal = 100;
        destoryTimer = 100;
        isTimer = false;
        sr.color = Color.white;
        followAcc = 0f;

        sr.material.SetInt("_IsActive", 1);

        isFollowPlayer = false;

        rb.velocity = Vector3.zero;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);

        gameObject.SetActive(false);
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
