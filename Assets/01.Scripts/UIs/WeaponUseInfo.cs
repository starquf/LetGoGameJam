using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUseInfo : MonoBehaviour, IPoolableComponent
{
    [SerializeField]
    private Image weaponIcon;
    [SerializeField]
    private Text damagedAmountTxt;
    [SerializeField]
    private Text usedBulletAmountTxt;

    public void Despawned()
    {

    }

    public void SetUI(Sprite sprite, string damagedAmount, string usedBulletAmount)
    {
        weaponIcon.sprite = sprite;
        damagedAmountTxt.text = damagedAmount;
        usedBulletAmountTxt.text = usedBulletAmount;
    }

    public void Spawned()
    {
        weaponIcon.sprite = null;
        damagedAmountTxt.text = "";
        usedBulletAmountTxt.text = "";
    }
    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(gameObject);
    }
}
