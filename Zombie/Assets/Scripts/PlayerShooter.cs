using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour {
    public Gun gun; // 사용할 총
    public ShotGun shotgun; // 사용할 총2
    public Transform gunPivot; // 총 배치의 기준점
    public Transform shotgunPivot;
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    private PlayerSwap playerSwap; // 플레이어의 현 무기 상황
    private PlayerInput playerInput; // 플레이어의 입력
    private Animator playerAnimator; // 애니메이터 컴포넌트

    private void Start() {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        playerSwap = GetComponent<PlayerSwap>();
    }

    /// <summary>
    /// 어차피 첫 시작은 무족건 권총이니까 권총시작
    /// </summary>
    private void OnEnable() {
        // 슈터가 활성화될 때 총도 함께 활성화

        gun.gameObject.SetActive(true);


    }
    
    private void OnDisable() {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        
        if (playerSwap.equipWeapon == playerSwap.weapons[0])
            gun.gameObject.SetActive(false);
        if (playerSwap.equipWeapon == playerSwap.weapons[1])
            shotgun.gameObject.SetActive(false);

    }

    private void Update() {
        // 입력을 감지하고 총 발사하거나 재장전

        if (Time.timeScale <= 0) return;
        if(playerInput.fire)
        {

            if (playerSwap.equipWeapon == playerSwap.weapons[0])
            {
                gun.Fire();
            }
                
            if (playerSwap.equipWeapon == playerSwap.weapons[1])
            {
                shotgun.Fire();
            }

        }
        else if(playerInput.reload)
        {
            
            if (playerSwap.equipWeapon == playerSwap.weapons[0])
            {
                if(gun.Reload())
                {
                    playerAnimator.SetTrigger("Reload");
                }
            }
                
            if (playerSwap.equipWeapon == playerSwap.weapons[1])
            {
               if(shotgun.ShotgunReload())
               {
                    playerAnimator.SetTrigger("Reload");
               }
            }
        }
        UpdateUI();
    }

    // 탄약 UI 갱신
    private void UpdateUI() {
        if (playerSwap.equipWeapon == playerSwap.weapons[0])
        {
            if (gun != null && UIManager.instance != null)
            {
                // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
                UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
            }
        }    
        if (playerSwap.equipWeapon == playerSwap.weapons[1])
        {
            if (shotgun != null && UIManager.instance != null)
            {
                // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
                UIManager.instance.UpdateAmmoText(shotgun.magAmmo, shotgun.ammoRemain);
            }
        }
       
    }

    // 애니메이터의 IK 갱신

    /// <summary>
    /// 샷건의 OnAnimatorIK 추가
    /// 이걸로 수정하는 법을 몰라서 그냥 유니티 안에서 Transform.position을 바꿈.
    /// </summary>
    
    private void OnAnimatorIK(int layerIndex) {
        
        if(playerSwap.equipWeapon == playerSwap.weapons[0])
        {
            gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
            

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1.0f);

            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);

            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
        }


        if(playerSwap.equipWeapon == playerSwap.weapons[1])
        {
            /// <summary>
            /// 샷건 피봇 자체가 좀 아래에 잡히길래 그냥 강제로 Position 을 위로 올림.
            /// </summary>

            shotgunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow)+new Vector3(0,0.15f,0);

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1.0f);

            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);

            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
        }
    }
}