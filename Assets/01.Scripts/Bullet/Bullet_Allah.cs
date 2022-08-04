using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Allah : Bullet
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            print("아무튼 폭발");
            SetDisable();
        }
    }


}
