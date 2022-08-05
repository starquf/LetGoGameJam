using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerAttack pa;

    private readonly string M1911_PATH = "Prefabs/Weapons/Weapon_M1911";

    private List<Weapon> nearWeaponList = new List<Weapon>();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        pa = GetComponentInChildren<PlayerAttack>();
        nearWeaponList = new List<Weapon>();
        pa.Init(GameObjectPoolManager.Instance.GetGameObject(M1911_PATH, pa.transform).GetComponent<Weapon>());
    }
 
    private void Update()
    {
        Weapon weapon = GetNearWeapon();
        if(weapon != null)
        {
            weapon.SetSwichAnim(true);
            if(Input.GetKeyDown(KeyCode.F))
            {
                nearWeaponList.Remove(weapon);
                weapon.SetSwichAnim(false);
                GameManager.Instance.soundHandler.Play("GetWeapon");
                ChangeWeapon(weapon);
            }
        }
    }


    public void ChangeWeapon(Weapon weapon)
    {
        weapon.transform.position = transform.position;
        pa.ChangeWeapon(weapon);
    }

    public Weapon GetNearWeapon()
    {
        if(nearWeaponList.Count>0)
        {
            Weapon nearWeapon = nearWeaponList[0];
            float curDist = Vector2.Distance(transform.position, nearWeaponList[0].transform.position);
            for (int i = 1; i < nearWeaponList.Count; i++)
            {
                nearWeapon = nearWeaponList[i];
                float dist = Vector2.Distance(transform.position, nearWeapon.transform.position);
                if (dist < curDist)
                {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            Weapon wp = collision.GetComponent<Weapon>();
            if (wp.isGround)
            {
                nearWeaponList.Add(wp);
            }
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
