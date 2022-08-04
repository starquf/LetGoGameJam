using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected Weapon baseWeapon;
    public Weapon currentWeapon;

    protected WaitForSeconds weaponShootWait = new WaitForSeconds(1f);

    public virtual void Init(Weapon baseWeapon)
    {
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

    protected abstract IEnumerator Shooting();
}
