using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private PlayerAttack pa;

    public Weapon baseWeapon;


    private readonly string AWM_PATH = "Prefabs/Weapons/Weapon_AWM";
    private readonly string MP7_PATH = "Prefabs/Weapons/Weapon_MP7";
    private readonly string AK47_PATH = "Prefabs/Weapons/Weapon_Ak47";
    private readonly string M1911_PATH = "Prefabs/Weapons/Weapon_M1911";
    private readonly string MAGICBAR_PATH = "Prefabs/Weapons/Weapon_MagicBar";
    private readonly string BLUEARCHIVE_PATH = "Prefabs/Weapons/Weapon_BlueArchive";
    private readonly string RAZERPISTOL_PATH = "Prefabs/Weapons/Weapon_RazerPistol";


    private void Start()
    {
        pa = GetComponentInChildren<PlayerAttack>();
        pa.Init(baseWeapon);
    }

 
    private void Update()
    {



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBaseWeapon(AWM_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeBaseWeapon(MP7_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeBaseWeapon(AK47_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeBaseWeapon(M1911_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeBaseWeapon(MAGICBAR_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeBaseWeapon(BLUEARCHIVE_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeBaseWeapon(RAZERPISTOL_PATH);
        }

       

    }


    public void ChangeBaseWeapon(string path)
    {
        GameObjectPoolManager.Instance.UnusedGameObject(baseWeapon.gameObject);
        baseWeapon.gameObject.SetActive(false);
        baseWeapon = GameObjectPoolManager.Instance.GetGameObject(path, pa.transform).GetComponent<Weapon>();
        baseWeapon.transform.position = transform.position;
        pa.ChangeWeapon(baseWeapon);
    }

}
