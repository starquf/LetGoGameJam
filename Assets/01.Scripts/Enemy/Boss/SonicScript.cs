using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SonicScript : MonoBehaviour, IPoolableComponent
{
    private Animator anim;

    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sr;

    private Vector3 attackPos;

    private int isAttack;

    public float moveSpeed = 10f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        isAttack = Animator.StringToHash("IsAttack");
    }

    public void Play()
    {
        sr.DOFade(1f, 0.5f).SetEase(Ease.Linear).From(0f)
            .OnComplete(() =>
            {
                StartCoroutine(Logic());
            });
    }

    private IEnumerator Logic()
    {
        WaitForSeconds pFiveSecWait = new WaitForSeconds(0.5f);
        WaitForSeconds pOneSecWait = new WaitForSeconds(0.1f);

        yield return pFiveSecWait;

        anim.SetBool(isAttack, true);

        coll.enabled = true;
        GameManager.Instance.soundHandler.Play("SonicDrill");

        yield return pFiveSecWait;

        attackPos = GameManager.Instance.playerTrm.position;

        yield return pFiveSecWait;

        MoveToTarget();

        yield return pFiveSecWait;
        yield return pFiveSecWait;
        yield return pFiveSecWait;
        yield return pFiveSecWait;
        yield return pFiveSecWait;

        rb.velocity = Vector2.zero;
        anim.SetBool(isAttack, false);

        StopSonic();
    }

    public void MoveToTarget()
    {
        Vector3 pos = (attackPos - transform.position).normalized;

        rb.velocity = pos * moveSpeed;
    }

    public void StopSonic()
    {
        sr.DOFade(0f, 0.5f).SetEase(Ease.Linear).From(1f).OnComplete(() =>
        {
            anim.SetBool(isAttack, false);
            SetDisable();
        });
    }

    public void Despawned()
    {

    }

    public void Spawned()
    {
        coll.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LivingEntity livingEntity = collision.GetComponent<LivingEntity>();
            Hit(livingEntity);
        }
    }

    private void Hit(LivingEntity hitEntity)
    {
        hitEntity.GetDamage(1);
    }

    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(this.gameObject);
    }
}
