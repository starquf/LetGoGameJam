using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerParrying : MonoBehaviour
{
    [SerializeField]
    private float coolTime;
    [SerializeField]
    private float parryingTime;
    [SerializeField]
    private Animator effectAnimator;

    private PlayerInput playerInput;
    private PlayerStat playerStat;

    private Collider2D parryingCol;
    private bool isCoolTime;
    private bool isEffectStart = false;

    private bool isStaticCoolTime = false;

    public bool isReflectMode = false;

    private Tween timeStopTween = null;

    public void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        playerStat = GetComponentInParent<PlayerStat>();

        parryingCol = GetComponent<CircleCollider2D>();
        parryingCol.enabled = false;
        StartCoroutine(Parrying());
        StartCoroutine(CoolTimeTimer());
        parryingCol.transform.DORotate(new Vector3(0,0,-20), 1f).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);

        EventManager<string>.AddEvent("LevelUp",() =>
        {
            timeStopTween.Kill();
            //print("트윈 없어짐");

            SetStaticParryingCool(false);
        });
    }

    public void StartParryingEffect(Bullet bullet)
    {
        if(!isEffectStart)
        {
            GameManager.Instance.soundHandler.Play("Parring");
            StartCoroutine(StartAnimation());

            if (GameManager.Instance.timeScale > 0f)
            {
                timeStopTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, .4f + (1f * GameManager.Instance.doubleSpeed), .1f).SetEase(Ease.InQuint).OnComplete(() =>
                {
                    DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f + (1f * GameManager.Instance.doubleSpeed), .5f).SetUpdate(true);
                }).SetUpdate(true);
            }


            if (isReflectMode)
            {
                CinemachineVirtualCamera cam = GameManager.Instance.cmPerlinObject;

                float originSize = cam.m_Lens.OrthographicSize;

                DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x, originSize - 2.0f, .4f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x, originSize, .2f).SetUpdate(true);
                }).SetUpdate(true);
            }
        }

        IEnumerator StartAnimation()
        {
            isEffectStart = true;
            effectAnimator.SetBool("IsParrying", true);
            yield return new WaitForSeconds(.5f);
            effectAnimator.SetBool("IsParrying", false);
        }

        if (isReflectMode)
        {
            Vector2 moveDir = bullet.rb.velocity.normalized;

            bullet.ChangeDir(-moveDir);
            bullet.SetOwner(false, WeaponType.Parrying);
            GameManager.Instance.addUsedWeaponInfo(WeaponType.Parrying, 1);
        }
        else
        {
            bullet.SetDisable();
        }
    }

    public void SetStaticParryingCool(bool canParrying)
    {
        this.isStaticCoolTime = canParrying;
    }

    private IEnumerator CoolTimeTimer()
    {
        while(true)
        {
            if(isCoolTime)
            {
                yield return new WaitForSeconds(isStaticCoolTime ? 4.5f : coolTime - (coolTime * playerStat.parryingCoolDown));
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
                if(isCoolTime)
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
