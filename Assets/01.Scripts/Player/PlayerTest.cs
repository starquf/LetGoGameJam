using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private PlayerAttack pa;

    public Weapon baseWeapon;

    private void Start()
    {
        pa = GetComponent<PlayerAttack>();
        pa.Init(baseWeapon);
    }
}
