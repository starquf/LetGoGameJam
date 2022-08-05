using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 playerAxis;
    private Rigidbody2D rigid;

    public float moveSpeed = 5f;

    private void Awake()
    {
        GameManager.Instance.playerTrm = transform;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (playerInput.isDie)
        {
            return;
        }
        playerAxis = playerInput.moveDir.normalized;
        playerInput.playerState = PlayerState.Move;
        if (playerInput.moveDir == Vector2.zero)
        {
            playerInput.playerState = PlayerState.Idle;
            playerAxis = Vector2.zero;
        }

        if (transform.position.x >= 40.5f && playerAxis.x > 0f || transform.position.x <= -8.5f && playerAxis.x < 0f)
        {
            playerAxis.x = 0f;
        }

        if (transform.position.y >= 4f && playerAxis.y > 0f || transform.position.y <= -24.5f && playerAxis.y < 0f)
        {
            playerAxis.y = 0f;
        }
        OnMove(playerAxis, moveSpeed);
    }

    public void OnMove(Vector2 dir, float speed)
    {
        rigid.velocity = dir * speed;
    }
}
