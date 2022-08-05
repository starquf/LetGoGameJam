using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerState playerState;

    public Vector3 mousePos;
    public Vector2 moveDir;

    public bool isAttack;

    public bool isSwitchWeapon;
    public bool isDie;
    public bool isKnockBack;
    public bool isParrying;

    private void Update()
    {
        if (Time.deltaTime <= 0)
            return;

        if (isDie)
        {
            moveDir = Vector2.zero;
            isAttack = false;
            return;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
            case PlayerState.Attack:
                moveDir.x = Input.GetAxisRaw("Horizontal");
                moveDir.y = Input.GetAxisRaw("Vertical");
                isSwitchWeapon = Input.GetButton("Switching");
                isAttack = Input.GetButton("Fire1");
                isParrying = Input.GetButton("Fire2");
                break;
            case PlayerState.Die:
                break;
        }

        if(moveDir != Vector2.zero)
        {
            GameManager.Instance.soundHandler.PlayLoopSFX("PlayerWalk", "PlayerWalk");
        }
        else
        {
            GameManager.Instance.soundHandler.StopLoopSFX("PlayerWalk");
        }
    }
}
