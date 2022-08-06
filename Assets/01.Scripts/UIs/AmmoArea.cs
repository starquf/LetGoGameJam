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

    [SerializeField]
    private Sprite bulletSprite;
    [SerializeField]
    private Sprite emptySprite;

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
                bullet.sprite = bulletSprite;
            }

            Effect particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, bullets[bullets.Count - 1].transform).GetComponent<Effect>();
            particle.transform.localPosition = Vector3.zero;
            particle.Play();

            expendBulletsText.text = "inf.";
            return;
        }

        int bulletCount = int.Parse(data.Trim());

        if (bulletCount > 5)
        {
            foreach(var bullet in bullets)
            {
                bullet.sprite = bulletSprite;
            }
            expendBulletsText.text = (bulletCount - 5).ToString();

            Effect particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, bullets[bullets.Count - 1].transform).GetComponent<Effect>();
            particle.transform.localPosition = Vector3.zero;
            particle.Play();
        }
        else
        {
            foreach (var bullet in bullets)
            {
                bullet.sprite = emptySprite;
            }

            for (int i = 0; i < bulletCount; i++)
            {
                bullets[i].sprite = bulletSprite;
            }

            int idx = Mathf.Clamp(bulletCount - 1, 0, 4);
            
            Effect particle = GameObjectPoolManager.Instance.GetGameObject(AMMO_PARTICLE, bullets[idx].transform).GetComponent<Effect>();
            particle.transform.localPosition = Vector3.zero;
            particle.Play();

            expendBulletsText.text = "";
        }
    }

}
