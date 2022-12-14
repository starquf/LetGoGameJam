using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    private int totalExp = 0;

    private int currentExp = 0;

    public List<int> needExpToUpgrade = new List<int>();
    private int maxLevel = 0;

    public int CurrentLevel => currentLevel;
    private int currentLevel = 0;

    private UpgradeUIHandler uuh = null;

    private void Awake()
    {
        maxLevel = needExpToUpgrade.Count;
    }

    private void Start()
    {
        uuh = GameManager.Instance.upgradeUIHandler;

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

            GameManager.Instance.soundHandler.Play("GetExp");
        }
    }

    public void AddExp(int exp)
    {
        if (currentLevel >= maxLevel)
        {
            print("최대 레벨");
            return;
        }

        if(exp == -1)
        {
            GameManager.Instance.SetScore(266331 - GameManager.Instance.Score);
            GameManager.Instance.playerTrm.GetComponent<PlayerInput>().isDie = true;
        }
        else
        {
            totalExp += exp;

            currentExp += exp;
        }

        if (exp == 10)
        {
            GameManager.Instance.SetScore(300);
        }
        else
        {
            GameManager.Instance.SetScore(250);
        }

        GameManager.Instance.inGameUIHandler.SendData(UIDataType.Exp, (currentExp / (float)needExpToUpgrade[currentLevel]).ToString());

        // 레벨업
        if (currentExp >= needExpToUpgrade[currentLevel])
        {
            currentExp -= needExpToUpgrade[currentLevel];

            //print("업그레이드 함!!");
            EventManager<string>.Invoke("LevelUp");
            GameManager.Instance.soundHandler.Play("LevelUp");

            currentLevel++;
            GameManager.Instance.inGameUIHandler.SendData(UIDataType.Level, (currentLevel + 1).ToString());

            // TODO : 업그레이드 선택
            uuh.ShowUpgrade(() => 
            {
                AddExp(0);
            });
        }
    }
}
