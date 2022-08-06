using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    public Weapon currentWeapon;

    protected SpriteRenderer weaponRenderer;
    protected float lookAngle;

    protected WaitForSeconds weaponShootWait = new WaitForSeconds(1f);

    public virtual void Init(Weapon baseWeapon)
    {
        baseWeapon.isGround = false;
        baseWeapon.isPlayer = false;

        baseWeapon.transform.SetParent(this.transform);
        baseWeapon.transform.localPosition = Vector3.right * baseWeapon.offset;
        baseWeapon.transform.localRotation = Quaternion.identity;

        currentWeapon = baseWeapon;

        weaponRenderer = baseWeapon.sr;
        weaponRenderer.material.SetInt("_IsActive", 0);

        weaponShootWait = new WaitForSeconds(baseWeapon.fireRate);

        StartCoroutine(Shooting());
    }

    public virtual void ChangeWeapon(Weapon weapon)
    {
        currentWeapon.SetDisable();

        currentWeapon = weapon;

        weapon.transform.SetParent(this.transform);
        weapon.transform.localPosition = Vector3.right * weapon.offset;
        weapon.transform.localRotation = Quaternion.identity;

        weapon.isPlayer = false;
        weapon.isGround = false;

        weaponRenderer = weapon.sr;
        weaponRenderer.material.SetInt("_IsActive", 0);

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
