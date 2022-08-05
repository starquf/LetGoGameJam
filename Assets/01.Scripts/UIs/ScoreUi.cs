using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : UIBase
{
    private Text scoreText;
    private int scoreint = 0;

    public override void Init()
    {
        mydataType = UIDataType.Score;
        scoreText = GetComponent<Text>();
        scoreText.text = "0";
    }

    public override void SetData(string data)
    {
        GameObject a;
        scoreint += int.Parse(data.Trim());

        a = GameObjectPoolManager.Instance.GetGameObject("PreFabs/UI/Score+",transform);

        a.GetComponent<ScorePlus>().SetText(data);
        StartCoroutine(TimeWait());

        IEnumerator TimeWait()
        {
            yield return new WaitForSeconds(.4f);
            scoreText.transform.DOScale(new Vector3(1.3f, 1.3f, 0), .4f).OnComplete(() =>
            {
                scoreText.transform.DOScale(new Vector3(1f, 1f, 0), .4f).SetEase(Ease.OutQuart);
            });

            scoreText.DOText(scoreint.ToString(), .8f, true, ScrambleMode.Numerals);
        }
    }
}
