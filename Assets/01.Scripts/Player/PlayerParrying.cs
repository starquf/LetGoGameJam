using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrying : MonoBehaviour
{
    [SerializeField]
    private float coolTime;
    [SerializeField]
    private float parryingTime;
    [SerializeField]
    private Animator effectAnimator;


    private PlayerInput playerInput;
    private Collider2D parryingCol;
    private bool isCoolTime;
    private bool isEffectStart = false;

    public void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        parryingCol = GetComponent<CircleCollider2D>();
        parryingCol.enabled = false;
        StartCoroutine(Parrying());
        StartCoroutine(CoolTimeTimer());
    }

    public void StartParryingEffect()
    {
        if(!isEffectStart)
        {
            StartCoroutine(StartAnimation());
        }

        IEnumerator StartAnimation()
        {
            isEffectStart = true;
            effectAnimator.SetBool("IsParrying", true);
            yield return new WaitForSeconds(.5f);
            effectAnimator.SetBool("IsParrying", false);
        }
    }

    private IEnumerator CoolTimeTimer()
    {
        while(true)
        {
            if(isCoolTime)
            {
                yield return new WaitForSeconds(coolTime);
                isCoolTime = false;
                isEffectStart = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator Parrying()
    {
        while(true)
        {
            if(playerInput.isParrying)
            {
                if(isCoolTime)
                {
                    yield return null;
                }
                else
                {
                    GameManager.Instance.soundHandler.Play("MeleeAttack");
                    isCoolTime = true;
                    parryingCol.enabled = true;
                    yield return new WaitForSeconds(parryingTime);
                    parryingCol.enabled = false;

                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
