using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : AttackBase
{
    public bool isAttacking = false;

    public bool isWaitting = false;

    private float timer = 0f;
    [HideInInspector]
    public bool isFirst = true;
    

    public float attackDuration = 5f;
    public float waitAttackDuration = 10f;
    public float waitAttackDurationFirst = 5f;
    private CircularSectorMeshRenderer cr;

    private Vector3 attackDir = Vector3.zero;
    private bool isBlue = false;
    private bool isOnceCalled = false;
    private WaitForSeconds enemyShootWait = new WaitForSeconds(1f);

    Sequence sequence = null;
    
    [HideInInspector]public Transform targetPos = null;

    private void Update()
    {
        if(targetPos != null)
            LookDirection(targetPos.position);
    }

    public override void Init(Weapon baseWeapon)
    {
        enemyShootWait = new WaitForSeconds(waitAttackDuration);
        cr = GetComponentInChildren<CircularSectorMeshRenderer>();
        cr.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.clear);
        base.Init(baseWeapon);

        Weapon_BlueArchive blue = currentWeapon.GetComponent<Weapon_BlueArchive>();
        isBlue = blue != null;
    }
    public void Init()
    {
        isWaitting = true;
        timer = waitAttackDurationFirst;
        isAttacking = true;
        isOnceCalled = false;

        if (!isBlue)
        {
            Vector3 dir = attackDir.normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            lookAngle = angle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public override void LookDirection(Vector3 pos)
    {
        Vector3 dir = attackDir.normalized;
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookAngle = angle;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.AngleAxis(angle, Vector3.forward),
            Time.deltaTime * (isBlue ? 2 : 5));

        if (lookAngle > 90f || lookAngle < -90f)
        {
            weaponRenderer.flipY = true;
        }
        else
        {
            weaponRenderer.flipY = false;
        }
    }

    protected override IEnumerator Shooting()
    {
        while (true)
        {
            yield return null;

            if (GameManager.Instance.timeScale <= 0f)
            {
                continue;
            }

            if (currentWeapon == null)
                continue;

            if (isAttacking)
            {
                weaponRenderer.color = Color.white;

                if (GameManager.Instance.isShowRange)
                {
                    cr.GetComponent<MeshRenderer>().material.SetFloat("_Alpha",.5f);
                }

                if (isWaitting)
                {
                    if(!isOnceCalled)
                    {
                        if (isBlue)
                        {
                            Weapon_BlueArchive blue = currentWeapon.GetComponent<Weapon_BlueArchive>();
                            blue.EnemyShootStop();
                        }

                        if (GameManager.Instance.isShowRange)
                        {
                            ShowRange();
                        }

                        isOnceCalled = true;
                    }
                    attackDir = targetPos.position - transform.position;
                    timer -= Time.deltaTime;
                    if (timer < 0f)
                    {
                        isWaitting = false;
                        isOnceCalled = false;
                        timer = attackDuration;
                    }
                }
                else
                {
                    if (isBlue)
                    {
                        currentWeapon.Shoot(transform.right*attackDir.magnitude);
                        yield return weaponShootWait;
                    }
                    else
                    {
                        currentWeapon.Shoot(transform.right);
                        if (!currentWeapon.isNoShakeWeapon)
                        {
                            GameManager.Instance.vCamScript.Shake(currentWeapon.bulletData);
                            yield return weaponShootWait;
                        }
                    }
                    timer -= Time.deltaTime;
                    if(timer < 0f)
                    {
                        isWaitting = true;
                        timer = waitAttackDuration;
                    }
                }
            }
            else
            {
                if (isBlue)
                {
                    Weapon_BlueArchive blue = currentWeapon.GetComponent<Weapon_BlueArchive>();
                    blue.EnemyShootStop();
                }
                weaponRenderer.color = Color.clear;
                cr.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", 0f);
                sequence.Kill();
            }
        }
    }

    protected void ShowRange()
    {
        MeshRenderer crMesh = cr.GetComponent<MeshRenderer>();

        cr.degree = 60f;
        cr.beginOffsetDegree = -30f;

        //factor = 7f / intensity;
        crMesh.material.SetColor("_Color", new Color(1.72f, 1.46f, 0.23f));
        crMesh.material.SetFloat("_Alpha", .5f);

        //factor = 15f / intensity;
        sequence = DOTween.Sequence()
            .Append(DOTween.To(() => crMesh.material.GetColor("_Color"), c => crMesh.material.SetColor("_Color", c), new Color(1f, 0.04f, 0.04f), attackDuration))
            .Join(DOTween.To(() => cr.degree, x => cr.degree = x, 0, timer))
            .Join(DOTween.To(() => cr.degree, x => cr.degree = x, 0, timer))
            .Join(DOTween.To(() => cr.beginOffsetDegree, x => cr.beginOffsetDegree = x, 0, timer));
    }
}
