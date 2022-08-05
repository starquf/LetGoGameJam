using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpArea : UIBase
{
    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Sprite heartSprite;
    [SerializeField]
    private Sprite extraSprite;

    private List<Image> hearts;

    [SerializeField]
    private int startingHeart = 3;
    private int maxHeartCnt = 5;
    private int maxExtraCnt = 2;

    public override void Init()
    {
        mydataType = UIDataType.Hp;
        hearts = GetComponentsInChildren<Image>().ToList();

        for (int i = 0; i < maxHeartCnt; i++)
        {
            hearts[i].sprite = emptySprite;
        }

        for (int i = maxHeartCnt; i < maxHeartCnt + maxExtraCnt; i++)
        {
            hearts[i].sprite = extraSprite;
            hearts[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < startingHeart; i++)
        {
            hearts[i].sprite = heartSprite;
        }

        foreach(var heart in hearts)
        {
            heart.transform.DOScale(new Vector3(1.1f, 1.1f, 0), .5f).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public override void SetData(string data)
    {
        HeartInfo heartInfo = JsonUtility.FromJson<HeartInfo>(data);

        for (int i = 0; i < maxHeartCnt; i++)
        {
            hearts[i].sprite = emptySprite;
            hearts[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < heartInfo.maxHeartCnt; i++)
        {
            hearts[i].color = new Color(1, 1, 1, 1);
        }

        for (int i = maxHeartCnt; i < maxHeartCnt + heartInfo.maxExtraHeartCnt; i++)
        {
            hearts[i].sprite = extraSprite;
            hearts[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < heartInfo.heart; i++)
        {
            hearts[i].sprite = heartSprite;
        }

        for (int i = maxHeartCnt; i < maxHeartCnt + heartInfo.extraHeart; i++)
        {
            hearts[i].color = new Color(1, 1, 1, 1);
        }
    }
}
