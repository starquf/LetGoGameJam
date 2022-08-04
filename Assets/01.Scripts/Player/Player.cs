using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public SpriteRenderer sr;

    private void Start()
    {
        Init();

        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    public override void SetHPUI()
    {
        print(hp);
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Hp, hp.ToString());
    }

    public override void GetDamage(float damage)
    {
        if (isDie) //이미 죽었거나 무적 상태라면
        {
            return;
        }

        hp -= 1f;

        if (hp <= 0)
        {
            Die();
        }

        SetHPUI();
    }

    protected override void Die()
    {
        base.Die();

        //playerInput.isDie = true;
    }

   
}
