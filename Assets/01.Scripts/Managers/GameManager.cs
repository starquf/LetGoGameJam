using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    private int score;

    public float timeScale = 1f;
    public float doubleSpeed = 0f;

    public CinemachineVirtualCamera cmPerlinObject;
    public VirtualCameraScript vCamScript;

    // 어웨이크 대신 이거 쓰셈
    private void OnAwake()
    {

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

    #endregion

    // 씬 이동 시 반드시 해야하는 거
    public void ResetOnSceneChanged()
    {
        ResetEvents();
    }

    public void SetScore(int _score)
    {
        score += _score;
        inGameUIHandler.SendData(UIDataType.Score, _score.ToString());
    }

    private void ResetEvents()
    {
        EventManager<string>.RemoveAllEvents();
    }
}
