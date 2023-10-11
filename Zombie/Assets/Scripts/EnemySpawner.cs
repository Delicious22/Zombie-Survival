using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviourPun, IPunObservable {
    public Enemy enemyPrefab; // 생성할 적 AI

    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public Color strongEnemyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트

    private int enemyCount = 0; // 현재 남은 적 수
    private int wave; // 현재 웨이브

    // 주기적으로 자동 실행되는 동기화 메서드
    public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
        // 로컬 오브젝트라면 쓰기 부분이 실행됨
        if(stream.IsWriting)
        {
            // 남은 적 수를 네트워크를 통해 보내기
            stream.SendNext(enemies.Count);
            // 현재 웨이브를 네트워크를 통해 보내기
            stream.SendNext(wave);
        }
        else
        {
            // 리모트 오브젝트라면 읽기 부분이 실행됨
            // 남은 적 수를 네트워크를 통해 받기
            enemyCount = (int)stream.ReceiveNext();
            // 현재 웨이브를 네트워크를 통해 받기
            wave = (int)stream.ReceiveNext();
        }
    }

    // 빨간줄 에러 떠서 주석 처리함
    // Photon이 기본적으로 지원하지 않는 형식인 Color를 네트워크로 전송하고자 할 때 필요한 것입니다.
    void Awake()
    {
        PhotonPeer.RegisterType(typeof(Color), 128, ColorSerialization.SerializeColor,
            ColorSerialization.DeserializeColor);
    }

    private void Update() {
        // 호스트만 적을 직접 생성할 수 있음
        // 다른 클라이언트는 호스트가 생성한 적을 동기화를 통해 받아옴
        if (PhotonNetwork.IsMasterClient)
        {
            // 게임오버 상태일 때는 생성하지 않음
            if (GameManager.instance != null && GameManager.instance.isGameover)
            {
                return;
            }

            // 적을 모두 물리친 경우 다음 스폰 실행
            if (enemies.Count <= 0)
            {
                SpawnWave();
            }
        }
        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() {
        if(PhotonNetwork.IsMasterClient)
        {
            // 호스트는 직접 갱신한 적 리스트를 이용해 남은 적 수 표시
            UIManager.instance.UpdateWaveText(wave, enemies.Count);
        }
        else
        {
            // 클라이언트는 적 리스트를 갱신할 수 없으므로
            // 호스트가 보내준 enemyCount를 이용해 적 수 표시
            UIManager.instance.UpdateWaveText (wave, enemies.Count);
        }
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave() {
        wave ++ ;

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i = 0 ; i< spawnCount ; i++)
        {
            float enemyIntensity = Random.Range(0f,1f);
            CreateEnemy(enemyIntensity);
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity) {
        //Lerp 선형보간 
        float health = Mathf.Lerp(healthMin,healthMax, intensity);
        float damage = Mathf.Lerp(damageMin,damageMax, intensity);
        float speed = Mathf.Lerp(speedMin,speedMax, intensity);

        Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);

        Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];

        // 적 프리팹으로부터 적 생성, 네트워크상의 모든 클라이언트에 생성됨
        GameObject createdEnemy = PhotonNetwork.Instantiate(enemyPrefab.gameObject.name,
            spawnPoint.position, spawnPoint.rotation);

        // 생성한 적을 셋업하기 위해 Enemy 컴포넌트를 가져옴
        Enemy enemy = createdEnemy.GetComponent<Enemy>();

        // 생성한 적의 능력치와 추적 대상 설정
        enemy.photonView.RPC("Setup", RpcTarget.All, health, damage, speed, skinColor);

        enemies.Add(enemy);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => StartCoroutine(DestroyAfter(enemy.gameObject, 10f));
        enemy.onDeath += () => GameManager.instance.AddScore(100);
    }

    // 포톤의 PhotonNetwork.Destroy()는 지연 파괴를 지원하지 않으므로 지연 파괴를 직접 구현함
    IEnumerator DestroyAfter(GameObject target, float delay)
    {
        // delay 만큼 쉬고
        yield return new WaitForSeconds(delay);
        // target이 아직 파괴되지 않았다면
        if(target != null)
        {
            // target을 모든 네트워크상에서 파괴
            PhotonNetwork.Destroy(target);
        }
    }
}