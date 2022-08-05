using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Bullet/BulletSO")]
public class BulletSO : ScriptableObject
{
    public float damage = 1;

    [Range(1, 20)] public float knockBackPower = 5;
    [Range(0.01f, 1f)] public float knockBackTime = 0.1f;
}
