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
                weapon.isGround = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            Weapon wp = collision.GetComponent<Weapon>();
            if (wp.isGround)
            {
                if(!nearWeaponList.Contains(wp))
                    nearWeaponList.Add(wp);
            }
        }
        Ammo ammo = collision.GetComponent<Ammo>();

        // 경험치에 닿았을 때
        if (ammo != null)
        {
            ammo.SetDisable();

            float count = pa.currentWeapon.maxBullet * (ammo.addAmmoPersent / 100f);
            int rCont = Mathf.CeilToInt(count);
            pa.AddBullet(rCont);
            //print(rCont +","+ count + "총알 추가됨");

            GameManager.Instance.soundHandler.Play("GetExp");
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
