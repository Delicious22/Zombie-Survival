using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI

    public EnemyData[] EnemyDatas; // 생성할 적 데이터
    public Transform[] spawnPoints; // 적 AI를 소환할 위치들


    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    private int wave; // 현재 웨이브

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 스폰 실행
        if (enemies.Count <= 0)
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

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i = 0 ; i< spawnCount ; i++)
        {
            CreateEnemy();
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy() {
        int spawnNum = Mathf.RoundToInt(wave * (Random.Range(0, wave * 0.1f)));

        if (spawnNum > EnemyDatas.Length) spawnNum = EnemyDatas.Length;

        EnemyData enemyData = EnemyDatas[spawnNum];

        Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];

        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        enemy.Setup(enemyData);

        enemies.Add(enemy);


        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        enemy.onDeath += () => GameManager.instance.AddScore(enemyData);
    }
}