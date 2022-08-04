using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private PlayerAttack pa;

    private readonly string AWM_PATH = "Prefabs/Weapons/Weapon_AWM";
    private readonly string MP7_PATH = "Prefabs/Weapons/Weapon_MP7";
    private readonly string AK47_PATH = "Prefabs/Weapons/Weapon_Ak47";
    private readonly string M1911_PATH = "Prefabs/Weapons/Weapon_M1911";
    private readonly string MAGICBAR_PATH = "Prefabs/Weapons/Weapon_MagicBar";
    private readonly string BLUEARCHIVE_PATH = "Prefabs/Weapons/Weapon_BlueArchive";
    private readonly string RAZERPISTOL_PATH = "Prefabs/Weapons/Weapon_RazerPistol";
    private readonly string M870_PATH = "Prefabs/Weapons/Weapon_M870";


    private void Start()
    {
        pa = GetComponentInChildren<PlayerAttack>();
        pa.Init(GameObjectPoolManager.Instance.GetGameObject(M1911_PATH, pa.transform).GetComponent<Weapon>());
    }
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(AWM_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(MP7_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(AK47_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(M1911_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeWeapon(MAGICBAR_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeWeapon(BLUEARCHIVE_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeWeapon(RAZERPISTOL_PATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeWeapon(M870_PATH);
        }
    }


    public void ChangeWeapon(string path)
    {
        Weapon weapon = GameObjectPoolManager.Instance.GetGameObject(path, pa.transform).GetComponent<Weapon>();
        weapon.transform.position = transform.position;
        pa.ChangeWeapon(weapon);
    }

}
