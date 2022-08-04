using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 playerAxis;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        playerAxis = playerInput.moveDir.normalized;
        playerInput.playerState = PlayerState.Move;
        if (playerInput.moveDir == Vector2.zero)
        {
            playerInput.playerState = PlayerState.Idle;
            playerAxis = Vector2.zero;
        }

        OnMove(playerAxis, 5f);
    }

    public void OnMove(Vector2 dir, float speed)
    {
        rigid.velocity = dir * speed;
    }
}
