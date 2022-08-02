using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� (�������� Awake �Լ� �ȿ� ����)
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
                    Debug.LogError("���ӸŴ����� �������� �ʽ��ϴ�!!");
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

    // �����ũ ��� �̰� ����
    private void OnAwake()
    {
        
    }

    // ���⿡�� �ٸ������� �����ؾߵǴ� �ڵ鷯�� �ֱ�
    #region GameUtils


    #endregion

    // �� �̵� �� �ݵ�� �ؾ��ϴ� ��
    public void ResetOnSceneChanged()
    {
        ResetEvents();
    }

    private void ResetEvents()
    {
        EventManager<string>.RemoveAllEvents();
    }
}
