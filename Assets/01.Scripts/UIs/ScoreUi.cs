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
    }

    public override void SetData(string data)
    {
        scoreText.text = data;
    }
}
