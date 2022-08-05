using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : UIBase
{
    private Text scoreText;

    public override void Init()
    {
        mydataType = UIDataType.Score;
        scoreText = GetComponent<Text>();
        scoreText.text = "0";
    }

    public override void SetData(string data)
    {
        scoreText.transform.DOScale(new Vector3(1.3f, 1.3f, 0), .4f).OnComplete(()=>
        {
            scoreText.transform.DOScale(new Vector3(1f, 1f, 0), .4f).SetEase(Ease.OutQuart);
        });

        scoreText.DOText(data, .8f,true,ScrambleMode.Numerals);
    }
}
