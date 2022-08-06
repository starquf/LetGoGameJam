using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private List<Weapon> nearWeaponList = new List<Weapon>();
    private PlayerAttack pa;

    private void Start()
    {
        pa = transform.parent.GetComponentInChildren<PlayerAttack>();
        nearWeaponList = new List<Weapon>();
    }

    private void Update()
    {
        Weapon weapon = GetNearWeapon();
        if (weapon != null)
        {
            weapon.SetSwichAnim(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                weapon.isGround = false;
                nearWeaponList.Remove(weapon);
                weapon.SetSwichAnim(false);
                GameManager.Instance.soundHandler.Play("GetWeapon");
                ChangeWeapon(weapon);
            }
        }
    }

    public Weapon GetNearWeapon()
    {
        if (nearWeaponList.Count > 0)
        {
            Weapon nearWeapon = null;
            float curDist = 1000;
            for (int i = 0; i < nearWeaponList.Count; i++)
            {
                float dist = Vector2.Distance(transform.position, nearWeaponList[i].transform.position);
                if (dist < curDist)
                {
                    nearWeapon = nearWeaponList[i];
                    curDist = dist;
                }
            }

            return nearWeapon;
        }
        else
        {
            return null;
        }
    }

    public void ChangeWeapon(Weapon weapon)
    {
        weapon.transform.position = transform.position;
        pa.ChangeWeapon(weapon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            Weapon wp = collision.GetComponent<Weapon>();
            if (wp.isGround)
            {
                if (!nearWeaponList.Contains(wp))
                    nearWeaponList.Add(wp);
            }
        }

        Ammo ammo = collision.GetComponent<Ammo>();

        if (ammo != null)
        {
            ammo.isFollowPlayer = true;
        }

        ExpBall expBall = collision.GetComponent<ExpBall>();

        // 경험치에 닿았을 때
        if (expBall != null)
        {
            expBall.isFollowPlayer = true;
        }

        Heart heart = collision.GetComponent<Heart>();

        // 경험치에 닿았을 때
        if (heart != null)
        {
            heart.isFollowPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            Weapon wp = collision.GetComponent<Weapon>();
            if (wp.isGround)
            {
                nearWeaponList.Remove(wp);
                wp.SetSwichAnim(false);
            }
        }
    }
}
