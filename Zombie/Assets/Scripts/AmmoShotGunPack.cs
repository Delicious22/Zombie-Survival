using System.Runtime.CompilerServices;
using UnityEngine;

// 총알을 충전하는 아이템
public class AmmoShotGunPack : MonoBehaviour, IItem {
    public int ammo = 2; // 충전할 총알 수
    private static bool isPicked = false;

    public bool IsPicked { get { return isPicked; } }

    /// <summary>
    /// 샷건은 플레이어의 해즈 웨폰과 웨폰에 추가되야함.
    /// </summary>
    public void Use(GameObject target) {
        // 전달 받은 게임 오브젝트로부터 PlayerShooter 컴포넌트를 가져오기 시도
        isPicked = true;
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();
        PlayerSwap playerSwap = target.GetComponent<PlayerSwap>();
        // PlayerShooter 컴포넌트가 있으며, 총 오브젝트가 존재하면
        if (playerShooter != null && playerShooter.shotgun != null)
        {
            // 총의 남은 탄환 수를 ammo 만큼 더한다
            playerShooter.shotgun.ammoRemain += ammo;
        }
        playerSwap.hasWeapons[1] = true;
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }

    public void SetPicked(bool picked)
    {
        isPicked = picked;
    }
}