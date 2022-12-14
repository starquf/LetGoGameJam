using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    private const string RIP_PREFAB_PATH = "Prefabs/Object/RIP";

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public SpriteRenderer sr;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.2f);

    private float hitCool = 0.5f;
    private float lastHitTime = 0f;
    private bool isHitted = false;

    [SerializeField]
    private HeartInfo heartInfo;

    public override float curHp => heartInfo.heart + heartInfo.extraHeart;

    private void Start()
    {
        heartInfo = new HeartInfo();

        Init();

        if (rigid == null)
            rigid = transform.parent.GetComponent<Rigidbody2D>();
        if (sr == null)
            sr = transform.parent.GetComponent<SpriteRenderer>();
        if (playerInput == null)
            playerInput = transform.parent.GetComponent<PlayerInput>();
    }

    public override void Init()
    {
        heartInfo.maxHeartCnt = 3;
        heartInfo.heart = heartInfo.maxHeartCnt;
        heartInfo.maxExtraHeartCnt = 2;
        heartInfo.extraHeart = 0;
        base.Init();
        //ShowDialog(1f);
        GetComponent<Collider2D>().enabled = true;
    }

    private void Update()
    {
        if(Time.time - lastHitTime > hitCool)
        {
            isHitted = false;
        } 
    }

    public override void SetHPUI()
    {
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Hp, JsonUtility.ToJson(heartInfo));
    }

    public void AddMaxHp()
    {
        heartInfo.AddMaxHeartCnt();
        heartInfo.AddHeart();
        SetHPUI();
    }

    public void AddHP(bool isExtra = false)
    {
        if(isExtra)
        {
            heartInfo.AddExtraHeart();
        }
        else
        {
            heartInfo.AddHeart();
        }

        SetHPUI();
    }

    public override void GetDamage(float damage)
    {
        if (isDie || isHitted) //?????? ???????????? ?????? ????????????
        {
            return;
        }
        
        isHitted = true;
        lastHitTime = Time.time;

        GameManager.Instance.soundHandler.Play("PlayerHit");

        heartInfo.RemoveHeart();

        StartCoroutine(Blinking());
        if (curHp <= 0)
        {
            Die();
        }
        SetHPUI();
    }

    public override void KnockBack(Vector2 direction, float power, float duration)
    {
        if (isDie) //?????? ???????????? ?????? ????????????
        {
            return;
        }
        base.KnockBack(direction, power, duration);
        playerInput.isKnockBack = isKnockBack;
    }

    protected override void ResetKnockBackParam()
    {
        base.ResetKnockBackParam();
        playerInput.isKnockBack = isKnockBack;
    }

    private IEnumerator Blinking()
    {
        while (true)
        {

            if (isHitted == false)
            {
                yield break;
            }

            yield return colorWait;
            sr.color = color_Trans;
            yield return colorWait;
            sr.color = Color.white;


        }
    }

    protected override void Die()
    {
        base.Die();
        //GameObjectPoolManager.Instance.GetGameObject(RIP_PREFAB_PATH, null).GetComponent<RIP>().SetPosition(transform.position);
        //gameObject.SetActive(false);
        GameManager.Instance.soundHandler.Play("PlayerDead");
        StartCoroutine(DieRoutine());
        GetComponent<Collider2D>().enabled = false;

        playerInput.isDie = true;
    }

    IEnumerator DieRoutine()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        rigid.velocity = !sr.flipX ? new Vector2(-2f, 2f) : new Vector2(2f, 2f);
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = !sr.flipX ? Vector2.left * 1f: Vector2.right * 1f;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = !sr.flipX ? Vector2.left * 0.5f: Vector2.right * 0.5f;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector2.zero;

        float a = 1f;
        while (true)
        {
            if(a <= 0f)
            {
                break;
            }
            a -= 0.01f;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
            yield return new WaitForSeconds(0.01f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Heart heart = collision.GetComponent<Heart>();

        // ???????????? ????????? ???
        if (heart != null)
        {
            heart.SetDisable();

            AddHP();
            GameManager.Instance.soundHandler.Play("Heal");

            GameManager.Instance.soundHandler.Play("GetExp");
        }
    }
}
