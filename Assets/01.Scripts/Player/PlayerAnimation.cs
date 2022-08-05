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
        if (playerInput.isDie)
        {
            anim.SetBool("isDie", playerInput.isDie);
            return;
        }

        moveVec = playerInput.moveDir.normalized;
        anim.SetFloat("MoveX", moveVec.x);
        anim.SetFloat("MoveY", moveVec.y);

        Vector3 dir = playerInput.mousePos - transform.position;

        if (dir.x < 0)
        {
            myRenderer.flipX = true;
        }
        else
        {
            myRenderer.flipX = false;
        }

    }
}
