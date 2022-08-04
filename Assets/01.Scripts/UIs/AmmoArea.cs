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

    public override void Init()
    {
        mydataType = UIDataType.Ammo;
        bullets = GetComponentsInChildren<Image>().ToList();
    }

    public override void SetData(string data)
    {
        int bulletCount = int.Parse(data.Trim());

        if(bulletCount > 5)
        {
            foreach(var bullet in bullets)
            {
                bullet.gameObject.SetActive(true);
            }
            expendBulletsText.text = (bulletCount - 5).ToString();
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

            expendBulletsText.text = "";
        }
    }

}
