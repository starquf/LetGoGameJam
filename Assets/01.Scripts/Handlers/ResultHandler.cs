using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        scoreCountTxt.text = GameManager.Instance.Score.ToString() + " ";
        levelTxt.text = GameManager.Instance.playerTrm.GetComponentInChildren<PlayerUpgrade>().CurrentLevel.ToString() + " ";

        timeTxt.text = TimeSpan.FromSeconds(Time.time - GameManager.Instance.StartTime).ToString("hh':'mm':'ss") + " ";
        killEnemyCountTxt.text = GameManager.Instance.KillEnemyCount.ToString();

        List<WeaponType> weaponTypes = GameManager.Instance.UseWeaponInfoDic.Keys.ToList();
        for (int i = 0; i < weaponTypes.Count; i++)
        {
            UsedWeaponInfo usedWeaponInfo = GameManager.Instance.UseWeaponInfoDic[weaponTypes[i]];
            WeaponUseInfo weaponUseInfo = GameObjectPoolManager.Instance.GetGameObject(WeaponUseInfoPrefabPath, contentTrm).GetComponent<WeaponUseInfo>();
            weaponUseInfo.SetUI(weaponTypes[i], usedWeaponInfo.damageAmount.ToString(), usedWeaponInfo.useCount.ToString());
        }
    }
}
