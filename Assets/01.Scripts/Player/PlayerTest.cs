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
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(AWM_PATH, pa.transform).GetComponent<Weapon>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(MP7_PATH, pa.transform).GetComponent<Weapon>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(AK47_PATH, pa.transform).GetComponent<Weapon>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(M1911_PATH, pa.transform).GetComponent<Weapon>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(MAGICBAR_PATH, pa.transform).GetComponent<Weapon>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(BLUEARCHIVE_PATH, pa.transform).GetComponent<Weapon>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ClearBaseWeapon();
            baseWeapon = GameObjectPoolManager.Instance.GetGameObject(RAZERPISTOL_PATH, pa.transform).GetComponent<Weapon>();
        }

        pa.Init(baseWeapon);

    }

    public void ClearBaseWeapon()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(baseWeapon.gameObject);
    }

}
