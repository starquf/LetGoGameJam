using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultHandler : Handler
{
    readonly string WeaponUseInfoPrefabPath = "Prefabs/UI/WeaponUseInfo";

    [SerializeField]
    private Text scoreCountTxt;
    [SerializeField]
    private Text levelTxt;
    [SerializeField]
    private Text timeTxt;
    [SerializeField]
    private Text killEnemyCountTxt;

    [SerializeField]
    private Transform contentTrm;
    [SerializeField]
    private Button confirmBtn;
    [SerializeField]
    private Button restartBtn;


    public override void OnAwake()
    {
        GameManager.Instance.resultHandler = this;
    }

    private void OnRestartButton()
    {

    }
    private void OnConfirmButton()
    {

    }

    public override void OnStart()
    {
        confirmBtn.onClick.AddListener(OnConfirmButton);
        restartBtn.onClick.AddListener(OnRestartButton);
    }

    public void SetUI()
    {
        scoreCountTxt.text = GameManager.Instance.Score.ToString();
        levelTxt.text = GameManager.Instance.playerTrm.GetComponentInChildren<PlayerUpgrade>().CurrentLevel.ToString();
        timeTxt.text = (Time.time - GameManager.Instance.StartTime).ToString();
        killEnemyCountTxt.text = GameManager.Instance.KillEnemyCount.ToString();

        foreach (var item in GameManager.Instance.UseWeaponInfoDic)
        {
            WeaponUseInfo weaponUseInfo = GameObjectPoolManager.Instance.GetGameObject(WeaponUseInfoPrefabPath, contentTrm).GetComponent<WeaponUseInfo>();
            weaponUseInfo.SetUI(item.Value.weaponSpr, item.Value.damageAmount.ToString(), item.Value.useCount.ToString());
        }
    }
}
