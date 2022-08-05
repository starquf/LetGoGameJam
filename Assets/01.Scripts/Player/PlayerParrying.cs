using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private bool canParrying = true;

    public void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        parryingCol = GetComponent<CircleCollider2D>();
        parryingCol.enabled = false;
        StartCoroutine(Parrying());
        StartCoroutine(CoolTimeTimer());
        parryingCol.transform.DORotate(new Vector3(0,0,-10), 1f).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);

        EventManager<string>.AddEvent("LevelUp",() => SetCanParrying(true));
    }

    public void StartParryingEffect()
    {
        if(!isEffectStart)
        {
            GameManager.Instance.soundHandler.Play("Parring");
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

    public void SetCanParrying(bool canParrying)
    {
        this.canParrying = canParrying;
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
                parryingCol.GetComponent<SpriteRenderer>().DOFade(.3f, .3f);
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
                if(isCoolTime || !canParrying)
                {
                    yield return null;
                }
                else
                {
                    GameManager.Instance.soundHandler.Play("MeleeAttack");
                    isCoolTime = true;
                    parryingCol.enabled = true;
                    parryingCol.GetComponent<SpriteRenderer>().DOFade(1, .2f);
                    parryingCol.transform.DORotate(new Vector3(0, 0, parryingCol.transform.rotation.z + 180), .2f);
                    yield return new WaitForSeconds(parryingTime);
                    parryingCol.transform.DORotate(new Vector3(0, 0, 0), .2f);
                    parryingCol.GetComponent<SpriteRenderer>().DOFade(0f, .2f);
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
