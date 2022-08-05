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

    private void Update()
    {
        if(Time.time - lastHitTime > hitCool)
        {
            isHitted = false;
        } 
    }

    public override void SetHPUI()
    {
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Hp, hp.ToString());
    }

    public override void GetDamage(float damage)
    {
        if (isDie || isHitted) //이미 죽었거나 무적 상태라면
        {
            return;
        }
        
        isHitted = true;
        lastHitTime = Time.time;

        hp -= 1f;

        StartCoroutine(Blinking());
        if (hp <= 0)
        {
            Die();
        }
        SetHPUI();
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
        StartCoroutine(DieRoutine());

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



}
