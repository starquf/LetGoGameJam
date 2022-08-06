using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public float confusionRate = 40f;

    public Image confusionImg;

    public bool isConfusion = false;
    private bool isReverse = false;

    private void Awake()
    {
        confusionImg.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.timeScale <= 0f)
        {
            moveDir = Vector2.zero;
            isSwitchWeapon = false;
            isAttack = false;
            isParrying = false;

            GameManager.Instance.soundHandler.StopLoopSFX("PlayerWalk");

            return;
        }

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
                moveDir.x = Input.GetAxisRaw("Horizontal") * (isReverse ? -1f : 1f);
                moveDir.y = Input.GetAxisRaw("Vertical") * (isReverse ? -1f : 1f);
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

    public void SetConfusion(float rate)
    {
        if (!isConfusion)
        {
            confusionImg.transform.parent.gameObject.SetActive(true);
            StartCoroutine(Confusion());
        }

        confusionRate = rate;

        isConfusion = true;
    }

    private IEnumerator Confusion()
    {
        WaitForSeconds oneSecWait = new WaitForSeconds(1f);
        Color transColor = new Color(1f, 1f, 1f, 0.7f);

        float t = 0f;

        while (true)
        {
            t += Time.deltaTime;

            confusionImg.fillAmount = t / confusionRate;

            yield return null;

            if (t > confusionRate)
            {
                isReverse = true;

                Tween tween = confusionImg.transform.DOScale(Vector3.one * 1.15f, 0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
                confusionImg.color = Color.white;

                yield return oneSecWait;

                tween.Kill();

                confusionImg.transform.localScale = Vector3.one;
                confusionImg.color = transColor;

                isReverse = false;
                t = 0;
            }
        }
    }
}
