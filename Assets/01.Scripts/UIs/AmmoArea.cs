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
        int bulletCount = int.Parse(data.Trim());

        if(data.Trim().Contains("inf"))
        {
            foreach (var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
            }
            expendBulletsText.text = "inf.";
            return;
        }

        if(bulletCount > 5)
        {
            foreach(var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
            }
            expendBulletsText.text = (bulletCount - 5).ToString();

            GameObject particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, null);
            Vector2 pos = bullets[bullets.Count - 1].transform.position;

            particle.transform.position = pos;
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

            GameObject particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, null);

            int idx = Mathf.Clamp(bulletCount - 1, 0, 4);

            Vector2 pos = bullets[idx].transform.position;

            particle.transform.position = pos;

            expendBulletsText.text = "";
        }
    }

}
