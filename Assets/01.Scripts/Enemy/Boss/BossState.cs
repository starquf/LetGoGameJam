using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    public void Init(Rigidbody2D rb, Animator anim)
    {
        this.rb = rb;
        this.anim = anim;
    }

    public abstract void Play(Action onEndState);
}
