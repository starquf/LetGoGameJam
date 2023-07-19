using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUseInfo : MonoBehaviour, IPoolableComponent
{
    public Vector2 spawnPos = new Vector2(50, 0);
    [SerializeField]
    private Text damagedAmountTxt;
    [SerializeField]
    private Text usedBulletAmountTxt;

    public void Despawned()
    {

    }

    public void SetUI(WeaponType weaponType, string damagedAmount, string usedBulletAmount)
    {
        GameObject go = GameObjectPoolManager.Instance.GetGameObject("Prefabs/UI/WeaponIcons/WeaponIcon_" + weaponType.ToString(), transform);
        go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        go.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        go.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        damagedAmountTxt.text = damagedAmount;
        usedBulletAmountTxt.text = usedBulletAmount;
    }

    public void Spawned()
    {
        damagedAmountTxt.text = "";
        usedBulletAmountTxt.text = "";
    }
    public void SetDisable()
    {
        GameObjectPoolManager.Instance.UnusedGameObject(gameObject);
    }
}
