using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AmmoArea : UIBase
{
    private List<Image> bullets;
    [SerializeField]
    private Text expendBulletsText;

    private readonly string AMMO_PARTICLE = "Prefabs/Effect/AmmoEffect";

    public override void Init()
    {
        mydataType = UIDataType.Ammo;
        bullets = GetComponentsInChildren<Image>().ToList();
    }

    public override void SetData(string data)
    {
        if(data.Trim().Contains("inf"))
        {
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
            }

            GameObject particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, bullets[bullets.Count - 1].transform);
            particle.transform.localPosition = Vector3.zero;

            expendBulletsText.text = "inf.";
            return;
        }

        int bulletCount = int.Parse(data.Trim());

        if (bulletCount > 5)
        {
            foreach(var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
            }
            expendBulletsText.text = (bulletCount - 5).ToString();

            GameObject particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, bullets[bullets.Count - 1].transform);
            particle.transform.localPosition = Vector3.zero;
        }
        else
        {
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(false);
            }

            for (int i = 0; i < bulletCount; i++)
            {
                bullets[i].gameObject.SetActive(true);
            }

            int idx = Mathf.Clamp(bulletCount - 1, 0, 4);

            GameObject particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, bullets[idx].transform);

            particle.transform.localPosition = Vector3.zero;

            expendBulletsText.text = "";
        }
    }

}
