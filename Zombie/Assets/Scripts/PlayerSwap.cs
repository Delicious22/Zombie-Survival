using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{

    /// <summary>
    /// 무기 소유 관련 함수3개
    /// 무기 리스트
    /// 보유중인 무기
    /// 현재 장착중인 무기
    /// </summary>
    public GameObject [] weapons;
    public bool [] hasWeapons;
    public GameObject equipWeapon;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// playerInput으로부터 스왑버튼을 감지하고 그에 맞게 무기를 스왑하는 형식
    /// </summary>
    void Update()
    {
        int weaponIndex = -1;
        if (playerInput.sDown1) weaponIndex = 0;
        if (playerInput.sDown2) weaponIndex = 1;
        if(!hasWeapons[1] && playerInput.sDown2)
            return;
        if(playerInput.sDown1 || playerInput.sDown2 ) 
        {
            if(equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }
}
