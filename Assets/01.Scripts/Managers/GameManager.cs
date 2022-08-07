using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;

[System.Serializable]
public class UsedWeaponInfo
{
    public Sprite weaponSpr;
    public float damageAmount;
    public int useCount;
}


public class GameManager : MonoBehaviour
{
    // 싱글톤 (실질적인 Awake 함수 안에 있음)
    #region SingleTon
    private static GameManager _instance;
    public static GameManager Instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    Debug.LogError("게임매니저가 존재하지 않습니다!!");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        OnAwake();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = default;
        }
    }
    #endregion

    [HideInInspector]
    public Transform playerTrm;
    [HideInInspector]
    public int Score => score;
    [SerializeField]
    private int score;

    [HideInInspector]
    public float KillEnemyCount => killEnemyCount;
    private float killEnemyCount;

    [HideInInspector]
    public float StartTime => startTime;
    private float startTime;


    [HideInInspector]
    public Dictionary<WeaponType, UsedWeaponInfo> UseWeaponInfoDic => useWeaponInfoDic;
    private Dictionary<WeaponType, UsedWeaponInfo> useWeaponInfoDic;

    public float timeScale = 1f;

    public float doubleSpeed = 0f;
    public bool isShowRange = false;

    public CinemachineVirtualCamera cmPerlinObject;
    public VirtualCameraScript vCamScript;

    public MapScript map;

    [HideInInspector]
    public Transform mapMin;
    [HideInInspector]
    public Transform mapMax;

    private List<IPoolableComponent> allItemList = new List<IPoolableComponent>();

    // 어웨이크 대신 이거 쓰셈
    private void OnAwake()
    {
        useWeaponInfoDic = new Dictionary<WeaponType, UsedWeaponInfo>();
        allItemList = new List<IPoolableComponent>();
    }

    // 여기에는 다른곳에서 참조해야되는 핸들러들 넣기
    #region GameUtils

    [HideInInspector]
    public SoundHandler soundHandler;
    [HideInInspector]
    public InGameUIHandler inGameUIHandler;
    [HideInInspector]
    public PopUpInfoHandler popUpInfoHandler;
    [HideInInspector]
    public EffectHandler effectHandler;
    [HideInInspector]
    public OptionHandler optionHandler;
    [HideInInspector]
    public UpgradeHandler upgradeHandler;
    [HideInInspector]
    public UpgradeUIHandler upgradeUIHandler;
    [HideInInspector]
    public StageHandler stageHandler;
    [HideInInspector]
    public ResultHandler resultHandler;
    [HideInInspector]
    public WeaponSpriteContainer weaponSpriteContainer;

    #endregion
    public void DistroyAll()
    {
        DisableAllItem();
        stageHandler.AllDieEnemy();
    }

    public void DisableAllItem()
    {
        for (int i = allItemList.Count-1; i >=0 ; i--)
        {
            allItemList[i].SetDisable();
        }
        allItemList.Clear();
    }

    // 씬 이동 시 반드시 해야하는 거
    public void ResetOnSceneChanged()
    {
        Debug.Log("Reset");
        ResetEvents();
        DOTween.KillAll();
        killEnemyCount = 0;
        score = 0;
        startTime = 0;
        isShowRange = false;
    }

    public void allItemListAdd(IPoolableComponent poolable)
    {
        if (!allItemList.Contains(poolable))
        {
            allItemList.Add(poolable);
        }
    }
    public void allItemListRemove(IPoolableComponent poolable)
    {
        if(allItemList.Contains(poolable))
        {
            allItemList.Remove(poolable);
        }
    }


    public void SetScore(int _score)
    {
        score += _score;
        inGameUIHandler.SendData(UIDataType.Score, _score.ToString());
    }

    public void SetStartTime(float time)
    {
        startTime = time;
    }
    public void AddKillEnemyCount(int count)
    {
        killEnemyCount += count;
    }
    public void addUsedWeaponDamageInfo(WeaponType weaponType, float addDamage)
    {
        if (!useWeaponInfoDic.ContainsKey(weaponType))
        {
            useWeaponInfoDic.Add(weaponType, new UsedWeaponInfo() { weaponSpr = weaponSpriteContainer.weaponSpriteDic[weaponType], useCount = 0, damageAmount = 0 });
        }
        useWeaponInfoDic[weaponType].damageAmount += addDamage;
    }
    public void addUsedWeaponInfo(WeaponType weaponType, int useGunCount)
    {
        if (!useWeaponInfoDic.ContainsKey(weaponType))
        {
            useWeaponInfoDic.Add(weaponType, new UsedWeaponInfo() { weaponSpr = weaponSpriteContainer.weaponSpriteDic[weaponType], useCount = 0, damageAmount = 0 });
        }
        useWeaponInfoDic[weaponType].useCount += useGunCount;
    }

    private void ResetEvents()
    {
        EventManager<string>.RemoveAllEvents();
        DOTween.KillAll();
    }
}
