using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpArea : UIBase
{
    private List<Image> hearts;

    public override void Init()
    {
        mydataType = UIDataType.Hp;
        hearts = GetComponentsInChildren<Image>().ToList();

        foreach(var heart in hearts)
        {
            heart.transform.DOScale(new Vector3(1.1f, 1.1f, 0), .5f).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public override void SetData(string data)
    {
        foreach(var hp in hearts)
        {
            hp.gameObject.SetActive(false);
        }

        for (int i = 0; i < int.Parse(data.Trim()); i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
}
