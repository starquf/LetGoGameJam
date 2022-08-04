using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    protected Weapon baseWeapon;
    public Weapon currentWeapon;

    protected SpriteRenderer weaponRenderer;
    protected float lookAngle;

    protected WaitForSeconds weaponShootWait = new WaitForSeconds(1f);

    public virtual void Init(Weapon baseWeapon)
    {
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        this.baseWeapon = baseWeapon;
        currentWeapon = baseWeapon;

        weaponShootWait = new WaitForSeconds(baseWeapon.fireRate);

        StartCoroutine(Shooting());
    }

    public virtual void ChangeWeapon(Weapon weapon)
    {
        currentWeapon = weapon;

        weaponShootWait = new WaitForSeconds(weapon.fireRate);
    }

    public virtual void LookDirection(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookAngle = angle;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (lookAngle > 90f || lookAngle < -90f)
        {
            weaponRenderer.flipY = true;
        }
        else
        {
            weaponRenderer.flipY = false;
        }
    }

    protected abstract IEnumerator Shooting();
}
