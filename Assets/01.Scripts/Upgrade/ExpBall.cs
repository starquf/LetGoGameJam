using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour, IPoolableComponent
{
    public int expPoint;
    private Rigidbody2D rb = null;

    public bool isFollowPlayer = false;
    public float followSpeed = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Spawned()
    {
        isFollowPlayer = false;
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

        rb.velocity = dir * followSpeed;
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);

        gameObject.SetActive(false);
    }
}
