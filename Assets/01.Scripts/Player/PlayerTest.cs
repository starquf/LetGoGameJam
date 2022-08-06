using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private PlayerAttack pa;

    private readonly string M1911_PATH = "Prefabs/Weapons/Weapon_M1911";

    private void Start()
    {
        pa = transform.parent.GetComponentInChildren<PlayerAttack>();
        pa.Init(GameObjectPoolManager.Instance.GetGameObject(M1911_PATH, pa.transform).GetComponent<Weapon>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ammo ammo = collision.GetComponent<Ammo>();

        // 경험치에 닿았을 때
        if (ammo != null)
        {
            ammo.SetDisable();

            float count = pa.currentWeapon.maxBullet * (ammo.addAmmoPersent / 100f);
            int rCont = Mathf.Clamp(Mathf.FloorToInt(count), 1, 10000);
            pa.AddBullet(rCont);
            //print(rCont +","+ count + "총알 추가됨");

            GameManager.Instance.soundHandler.Play("GetExp");
        }
    }
}
