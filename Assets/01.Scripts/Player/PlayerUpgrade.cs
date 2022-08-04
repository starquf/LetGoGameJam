using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    private int totalExp = 0;

    private int currentExp = 0;

    public List<int> needExpToUpgrade = new List<int>();
    private int maxLevel = 0;

    private int currentLevel = 0;

    private void Awake()
    {
        maxLevel = needExpToUpgrade.Count;
    }

    private void Start()
    {
        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Exp, "0");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        ExpBall exp = coll.GetComponent<ExpBall>();

        // 경험치에 닿았을 때
        if (exp != null)
        {
            exp.SetDisable();

            AddExp(exp.expPoint);
        }
    }

    public void AddExp(int exp)
    {
        if (currentLevel >= maxLevel)
        {
            print("최대 레벨");
            return;
        }

        totalExp += exp;

        currentExp += exp;

        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Exp, (currentExp / (float)needExpToUpgrade[currentLevel]).ToString());

        print($"점수 먹음 !! {exp}");

        // 레벨업
        if (currentExp >= needExpToUpgrade[currentLevel])
        {
            currentExp -= needExpToUpgrade[currentLevel];

            print("업그레이드 함!!");

            currentLevel++;
            GameManager.Instance.inGameUIHandler.SendData(UIDataType.Level, (currentLevel + 1).ToString());

            // TODO : 업그레이드 선택


        }
    }
}
