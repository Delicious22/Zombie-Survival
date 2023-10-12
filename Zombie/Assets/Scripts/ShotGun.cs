using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngineInternal;

// 총을 구현한다
public class ShotGun : MonoBehaviour {
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum ShotgunState {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    /// <summary>
    /// Set Pellet Limit
    /// </summary>
    public int shotgunPellet = 5; 

    public GameObject laser ; 
    public ShotgunState shotgunState { get; private set; } // 현재 총의 상태

    public Transform fireShotgunTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleShotgunFlashEffect; // 총구 화염 효과
    public ParticleSystem shellShotgunEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 150; // 공격력
    private float fireDistance = 150f; // 사정거리

    public int ammoRemain = 0; // 남은 전체 탄약
    public int magCapacity = 1 ; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약

    private int storeLastBullet ;
    private bool isStoreBullet =false;
    public float timeBetFire = 1.5f; // 총알 발사 간격
    public float reloadTime = 3f; // 재장전 소요 시간
    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake() {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer=GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable() {
        // 총 상태 초기화


        ///<summary>
        /// isStoreBullet을 false로 선언 해주고 
        /// magAmmo를 한번이라도 저장했으면 magCapacity로 부터 저장하는게 아니라
        /// OnDisable할때 저장했던 값을 불러와서 저장하게함
        /// Gun 스크립트에도 같은 방식으로 적용
        /// </summary>
        if(!isStoreBullet)
        {
            magAmmo = magCapacity;
            isStoreBullet = true;
        }
        else
            magAmmo = storeLastBullet;
        shotgunState = ShotgunState.Ready;
        //탄창채우고 총을 쏠상태로 변경
        lastFireTime = 0;
        // 총쏜시점 초기화
    }

    /// <summary>
    /// 무기를 스왑 ( 비활성화) 하면서 갖고있던 총알 갯수를 저장함
    /// </summary>
    private void OnDisable()
    {
        storeLastBullet = magAmmo;
    }

    // 발사 시도
    public void Fire() {

        //준비상태일때 마지막 총 발사시점에서 timebetFire이상의 시간이 지남
        if(shotgunState == ShotgunState.Ready && Time.time >= lastFireTime+timeBetFire && magAmmo >=1)
        {
            lastFireTime = Time.time;
            //마지막 총 발사시점 갱신
            Shot();
            // 발사

        }

    }

    // 실제 발사 처리
    private void Shot() {

        
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        /// <summary>
        /// 샷건의 펠릿수 만큼 Raycast가 작동
        /// </summary>
        /// 

        muzzleShotgunFlashEffect.Play();
        shellShotgunEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(shotClip);

        for (int i = 0; i < shotgunPellet ; i++)
        {
            if(Physics.Raycast(fireShotgunTransform.position, fireShotgunTransform.forward+new Vector3(Random.Range(-0.7f,0.7f), Random.Range(-0.1f,0.1f),0), out hit, fireDistance))
            {
                IDamageable target = hit.collider.GetComponent<IDamageable>();

                if(target != null)
                {
                    target.OnDamage(damage,hit.point, hit.normal);
                }
                hitPosition = hit.point;
                ShotgunEffect(hitPosition);
            }   
        }
        
        magAmmo--;
        if(magAmmo<=0)
        {
            shotgunState = ShotgunState.Empty;
        }
    }


    /// <summary>
    /// 잘은 모르겠지만 LineRenderer가 하나로 동시 작동이 안되는듯함(당연한걸수도). 
    /// 그래서 코루틴이 안먹히는거같았음.
    /// 차라리 LineRenderer를 하나로 쓰지말고 여러개를 만들어서 쓰자고 생각함.
    /// 먼저 Laser 라는 라인렌더러 컴포넌트를 가진 아무 오브젝트를 만들어서 프리펩에 박음
    /// 
    /// 그러고 샷건이펙트라는 함수를 여러번 실행하면서 여러개의 레이저를 프리펩으로부터 불러냈음.
    /// 그러한 레이저들은 각자의 LineRenderer 속성을 가지고 있기 때문에 
    /// 코루틴을 이용해서 동시에 여러 작업이 가능해졌음. 
    /// positionCount=0을 Coroutine으로 실행하면서 라인을 지웠음 
    /// </summary>

    private void ShotgunEffect(Vector3 end)
    {
        LineRenderer lr = Instantiate(laser).GetComponent<LineRenderer>();
        lr.positionCount =2;
        lr.SetPosition(0,fireShotgunTransform.position);
        lr.SetPosition(1,end);
        StartCoroutine(DisableShotEffect(lr));
    }
    private IEnumerator DisableShotEffect(LineRenderer lr) 
    {
        yield return new WaitForSeconds(0.15f);
        lr.positionCount = 0 ;
    }
    /// // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition) {
        muzzleShotgunFlashEffect.Play();
        shellShotgunEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(shotClip);
        bulletLineRenderer.SetPosition(0,fireShotgunTransform.position);
        bulletLineRenderer.SetPosition(1,hitPosition);
        
        // // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;
        Debug.Log("asdd2");

        // // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);
        Debug.Log("asd23");
        // // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;


    }

    // 재장전 시도
    public bool ShotgunReload() {

        if(shotgunState==ShotgunState.Reloading || ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            return false;

        }

        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {
        // 현재 상태를 재장전 중 상태로 전환
        shotgunState = ShotgunState.Reloading;
        
        gunAudioPlayer.PlayOneShot(reloadClip);
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);
        int ammoToFill = magCapacity - magAmmo;

        if(ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;
        
        // 총의 현재 상태를 발사 준비된 상태로 변경
        shotgunState = ShotgunState.Ready;
    }
}