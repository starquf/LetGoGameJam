using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    private PlayerInput playerInput;




    public override void SetHPUI()
    {

    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    protected override void Die()
    {
        base.Die();

        playerInput.isDie = true;
    }
}
