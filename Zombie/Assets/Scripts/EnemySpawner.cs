using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI
    public BossEnemy bossPrefab; // 보스 프리펩

    public EnemyData[] EnemyDatas; // 생성할 적 데이터
    public EnemyData BossData; // 보스 좀비 데이터
    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    private List<Enemy> deactEnemys = new List<Enemy>();
    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    private int wave; // 현재 웨이브

    private void Start()
    {
        StartCoroutine(DeactEnemyCollector());
    }

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 스폰 실행
        if (enemies.Count <= 0 && wave != 3)
        {
            SpawnWave();
        }

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() {
        // 현재 웨이브와 남은 적의 수 표시
        UIManager.instance.UpdateWaveText(wave, enemies.Count);
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave() {
        wave ++ ;

        if (wave == 3)
        {
            // 보스몬스터 소환
            CreateBoss();
            return;
        }
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i = 0 ; i< spawnCount ; i++)
        {
            CreateEnemy();
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy() {
        // 적용할 적 데이터 번호를 결정
        int spawnNum = Mathf.RoundToInt(wave * (Random.Range(0, wave * 0.1f)));
        if (spawnNum >= EnemyDatas.Length) spawnNum = EnemyDatas.Length -1;
        EnemyData enemyData = EnemyDatas[spawnNum];

        // 적을 생성할 위치 랜덤 결정
        Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];

        // 생성 할 적을 담을 변수
        Enemy enemy = null;

        // 비활성화 되있는 적들이 있으면 결정된 데이터를 가진 적이 있는지 검색
        for (int i = 0; i < deactEnemys.Count; i++)
        {
            if (deactEnemys[i].EnemyTypeNum == (int)enemyData.type)
            {
                enemy = deactEnemys[i];
                deactEnemys.Remove(enemy);
                enemy.gameObject.SetActive(true);
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = spawnPoint.rotation;
                break;
            }
        }

        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.Setup(enemyData);
            enemy.onDeath += () => enemies.Remove(enemy);
            enemy.onDeath += () => StartCoroutine(AddDeactEnemyCoroutine(enemy, deactEnemys));
            enemy.onDeath += () => GameManager.instance.AddScore(enemyData);
        }

        enemies.Add(enemy);
    }

    private void CreateBoss()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Enemy boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        boss.Setup(BossData);
        boss.onDeath += () => enemies.Remove(boss);
        boss.onDeath += () => Destroy(boss, 5f);
        boss.onDeath += () => GameManager.instance.AddScore(BossData);

        enemies.Add(boss);
    }

    /// <summary>
    /// 죽은 좀비를 비활성화 리스트에 추가하고 오브젝트를 비활성화
    /// enemy 자신과 비활성화 리스트를 매개변수로 받는다
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="deactEnemys"></param>
    /// <returns></returns>
    IEnumerator AddDeactEnemyCoroutine(Enemy enemy, List<Enemy> deactEnemys)
    {
        yield return new WaitForSeconds(2f);

        deactEnemys.Add(enemy);
        enemy.gameObject.SetActive(false);
    }

    /// <summary>
    /// 일정 시간마다 비활성화 리스트를 검사하고
    /// 15개 이상 존재하면 가장 오래된 적을 삭제후 리스트에서 제거
    /// </summary>
    /// <returns></returns>
    IEnumerator DeactEnemyCollector()
    {
        while(true)
        {
            if (deactEnemys.Count > 15)
            {
                Destroy(deactEnemys[0]);
                deactEnemys.Remove(deactEnemys[0]);
            }

            yield return new WaitForSeconds(5f);
        }
    }
}