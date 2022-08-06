using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour, IPoolableComponent
{
    public int expPoint;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr;

    public bool isFollowPlayer = false;

    private float followAcc = 0f;
    public float followSpeed = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Spawned()
    {
        sr.material.SetInt("_IsActive", 1);
        isFollowPlayer = false;
        followAcc = 0f;
    }

    public void Despawned()
    {

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
        followAcc += Time.deltaTime * 2f;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);

        gameObject.SetActive(false);
    }
}
