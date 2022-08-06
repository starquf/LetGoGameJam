using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class LimitAmountWeapon
{
    public int maxEnemy;
    public List<AmountWeapon> maxWeaponType;
}

[Serializable]
public class AmountWeapon
{
    public WeaponType type;
    public int amount;
}


[Serializable]
public class EliteWave
{
    public enemyType elite;
    public int enterScore;
    public int resetWaveEnemyAcount;
    public bool isEnter = false;
}

public class StageHandler : MonoBehaviour
{
    //public Action OnWaveNumberChanged;  // 퀴즈?

    // 1 웨이브 기본
    private enum eWaveState
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }
    private eWaveState state;

    public int wavePlusEnemyAmount = 3;
    public int defaultwaveEnemyAmount = 5;
    public float waveTimer = 10f;
    public Vector2 spawnWaitTimerRange = new Vector2(0.1f, 0.3f);


    // 2 웨이브 넘버에 따른 관련 변수들
    private int waveNumber; // 웨이브 숫자는 시간이 갈 수록 증가함
    private float nextWaveSpawnTimer;   // 다음 웨이브까지의 대기시간
    private float nextEnemySpawnTimer;  // 적 스폰시 동시에 여러마리 스폰을 방지하기 위한 타이머
    private int remainingEnemySpawnAmount;  // 한 웨이브에 몇마리의 적을 생성할 것인지?

    public List<EliteWave> eliteWaves = new List<EliteWave>();

    public List<enemyInfo> enemyInfos = new List<enemyInfo>();

    [HideInInspector]
    public int amountEnemy = 0;
    [HideInInspector]
    public Dictionary<WeaponType, int> amountWeaponType = new Dictionary<WeaponType, int>();

    public List<LimitAmountWeapon> maxLimit = new List<LimitAmountWeapon>();

    // 3 스폰 지점을 리스트로 생성
    //[SerializeField] private Transform spawnPositionTransform;
    [SerializeField] private List<Transform> spawnPositionTransformList;
    private Vector3 spawnPosition;

    // 다음 생성될 위치를 이미지로 표시
    [SerializeField] private Transform nextWaveSpawnPositionTransform;

    public List<Enemy> allEnemyList = new List<Enemy>();

    private void Awake()
    {
        GameManager.Instance.stageHandler = this;
        //// 자식으로 적 스폰 위치가 존재한다면 리스트에 넣어줌
        //if (spawnPositionTransformList != null && spawnPositionTransformList.Count > 0)
        //    spawnPositionTransformList.Clear();

        //// 자식의 첫번째 오브젝트는 nextWaveSpawnPos이므로 스킵하고 다음부터 추가
        //for (int i = 1; i < this.transform.childCount; i++)
        //{
        //    spawnPositionTransformList.Add(this.transform.GetChild(i));
        //}
    }

    private void Start()
    {
        amountWeaponType = new Dictionary<WeaponType, int>();
        for (int i = 0; i < maxLimit[GetLimitIdxForPlayerLevel()].maxWeaponType.Count; i++)
        {
            amountWeaponType.Add(maxLimit[GetLimitIdxForPlayerLevel()].maxWeaponType[i].type, 0);
        }
        // 최소 스폰 시작 전 세팅
        state = eWaveState.WaitingToSpawnNextWave;

        // 3 스폰 생성 위치를 랜덤으로 지정해준다
        // spawnPosition = spawnPositionTransform.position;
        SetRandomSpawnPos();

        // 최초 게임 시작 후 3초 이후에 순환 시작
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        if (GameManager.Instance.timeScale <= 0f)
            return;

        switch (state)
        {
            case eWaveState.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;
            case eWaveState.SpawningWave:
                /*
                    스폰 예정된 적의 숫자가 0이 될 때까지 순환됨
                    nextEnemySpawnTimer 값 세팅은 매번 200ms의 랜덤값을 줌으로써 겹치게 생성하는걸 방지할 수 있음
                */
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        if (!(maxLimit[GetLimitIdxForPlayerLevel()].maxEnemy > amountEnemy))
                        {
                            state = eWaveState.WaitingToSpawnNextWave;
                            nextWaveSpawnTimer = waveTimer;
                            return;
                        }
                        nextEnemySpawnTimer = UnityEngine.Random.Range(spawnWaitTimerRange.x, spawnWaitTimerRange.y);

                        // 실제 적 생성 후 remainingEnemySpawnAmount 하나씩 감소
                        SetRandomSpawnPos();
                        Enemy enemy = GetRandomEnemy();
                        allEnemyList.Add(enemy);
                        enemy.transform.position = spawnPosition;
                        //Enemy.Create(spawnPosition + UtilClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        // 스폰 예정된 적을 모두 소진했다면 새로운 스폰위치를 랜덤으로 받고 다시 스폰 대기상태로...
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = eWaveState.WaitingToSpawnNextWave;

                            // 3 위치를 지정한 스폰 리스트에서 랜덤으로 가져옴
                            // spawnPosition = spawnPositionTransform.position;
                            //SetRandomSpawnPos();

                            nextWaveSpawnTimer = waveTimer;   // 이런값들은 외부시트로 관리
                        }
                    }
                }
                break;
        }
    }

    private int GetLimitIdxForPlayerLevel()
    {
        int level = GameManager.Instance.playerTrm.GetComponentInChildren<PlayerUpgrade>().CurrentLevel;
        if(level < 5)
        {
            return 0;
        }
        else if(level < 10)
        {
            return 1;
        }
        else if(level <15)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private bool CanSpawnEnemy(enemyInfo enemyInfo, ref Enemy enemy)
    {
        int rand = Random.Range(0, enemyInfo.enemyList.Count);
        enemyType eType = enemyInfo.enemyList[rand];
        enemy = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Enemy/" + eType.ToString(), transform).GetComponent<Enemy>();

        for (int j = 0; j < enemy.canHaveWeaponList.Count; j++)
        {
            WeaponType weaponType = enemy.canHaveWeaponList[j].type;
            for (int k = 0; k < maxLimit[GetLimitIdxForPlayerLevel()].maxWeaponType.Count; k++)
            {
                if(maxLimit[GetLimitIdxForPlayerLevel()].maxWeaponType[k].type == weaponType)
                {
                    //print(weaponType.ToString());
                    if(maxLimit[GetLimitIdxForPlayerLevel()].maxWeaponType[k].amount > amountWeaponType[weaponType])
                    {
                        return true;
                    }
                }
            }
        }
        GameObjectPoolManager.Instance.UnusedGameObject(enemy.weapon.gameObject);
        GameObjectPoolManager.Instance.UnusedGameObject(enemy.gameObject);
        enemy = null;
        return false;
    }
    public bool CanGetWeapon(WeaponType weaponType)
    {
        int playerLevel = GetLimitIdxForPlayerLevel();
        for (int i = 0; i < maxLimit[playerLevel].maxWeaponType.Count; i++)
        {
            AmountWeapon maxAmountWeapon = maxLimit[playerLevel].maxWeaponType[i];
            if (maxAmountWeapon.type == weaponType)
            {
                //print("이게 안되노" + maxAmountWeapon.amount + ", " + amountWeaponType[weaponType]+ weaponType.ToString());
                if (maxAmountWeapon.amount > amountWeaponType[weaponType])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    private Enemy GetRandomEnemy()
    {
        Enemy enemy = null;
        int rand = 0;
        enemyInfo enemyInfo = null;
        bool isRoop = false;
        List<int> canRand = new List<int>();
        for (int i = 0; i < enemyInfos.Count; i++)
        {
            canRand.Add(i);
        }
        do
        {
            rand = canRand[Random.Range(0, canRand.Count)];
            canRand.Remove(rand);
            enemyInfo = enemyInfos[rand];
            isRoop = enemyInfo.enterMinScore > GameManager.Instance.Score || !CanSpawnEnemy(enemyInfo, ref enemy);
        } while (isRoop);
        amountEnemy++;
        amountWeaponType[enemy.weapon.weaponType]++;

        return enemy;
    }

    public Tuple<enemyType, int> CanSpawnElite()
    {
        enemyType eType = enemyType.NONE;
        for (int i = 0; i < eliteWaves.Count; i++)
        {
            EliteWave eliteWave = eliteWaves[i];
            if(!eliteWave.isEnter)
            {
                if (eliteWave.enterScore <= GameManager.Instance.Score)
                {
                    return new Tuple<enemyType, int>(eliteWaves[i].elite, i);
                }
            }
        }
        return new Tuple<enemyType, int>(eType, 0);
    }

    private void SpawnWave()
    {
        // 웨이브 숫자가 늘어날수록 스폰하는 적의 숫자로 같이 늘려줌
        remainingEnemySpawnAmount = Mathf.Clamp(defaultwaveEnemyAmount + wavePlusEnemyAmount * waveNumber, 1, 15);     // 이런값들은 외부시트로 관리
        print(waveNumber + 1 + "웨이브, " + remainingEnemySpawnAmount + "명 소환");

        Tuple<enemyType, int> eliteInfo = CanSpawnElite();

        if (!eliteInfo.Item1.Equals(enemyType.NONE))
        {
            print("엘리트 출격");
            Enemy enemy = GameObjectPoolManager.Instance.GetGameObject("Prefabs/Enemy/" + eliteInfo.Item1, transform).GetComponent<Enemy>();
            enemy.SetElite();
            enemy.transform.position = spawnPosition;
            allEnemyList.Add(enemy);

            amountEnemy += 10;

            defaultwaveEnemyAmount = eliteWaves[eliteInfo.Item2].resetWaveEnemyAcount;
            eliteWaves[eliteInfo.Item2].isEnter = true;
            waveNumber = 0;
        }

        state = eWaveState.SpawningWave;
        waveNumber++;
        //OnWaveNumberChanged?.Invoke();
    }

    private void SetRandomSpawnPos()
    {
        spawnPosition = GetRandomSpawnPosition();
        nextWaveSpawnPositionTransform.position = spawnPosition;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public Vector3 GetRandomSpawnPosition()
    {
        List<Vector3> canSpawnList = new List<Vector3>();

        for (int i = 0; i < spawnPositionTransformList.Count; i++)
        {
            Vector3 pos = spawnPositionTransformList[i].position;

            if (IsInMap(pos))
            {
                canSpawnList.Add(pos);
            }
            else
            {
                print("난 지능이 상승했다 히히");
            }
        }

        if (canSpawnList.Count > 0)
        {
            return canSpawnList[Random.Range(0, canSpawnList.Count)];
        }

        return Vector3.zero;
    }

    public bool IsInMap(Vector3 pos)
    {
        return pos.x < GameManager.Instance.mapMax.position.x &&
               pos.x > GameManager.Instance.mapMin.position.x &&
               pos.y < GameManager.Instance.mapMax.position.y &&
               pos.y > GameManager.Instance.mapMin.position.y;
    }
}