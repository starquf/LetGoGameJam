using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator anim;
    private SpriteRenderer myRenderer;
    private Vector2 moveVec;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdatePlayerAnimation();
    }

    private void UpdatePlayerAnimation()
    {
        moveVec = playerInput.moveDir.normalized;
        anim.SetFloat("MoveX", moveVec.x);
        anim.SetFloat("MoveY", moveVec.y);
        anim.SetBool("isAttack", playerInput.isAttack);
        if (playerInput.mousePos.x < 0)
        {
            myRenderer.flipX = true;
        }
        else
        {
            myRenderer.flipX = false;
        }

    }
}
