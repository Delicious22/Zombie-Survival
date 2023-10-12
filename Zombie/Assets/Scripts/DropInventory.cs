using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInventory : MonoBehaviour
{
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject IconWalkie;
    public GameObject IconBattery;
    public GameObject IconBible;
    public GameObject IconCross;
    public GameObject IconHolywater;
    
    //드롭템을 인벤토리에 가지고 있나 아닌가를 나타내는 bool.
    public bool isWalkie = false;
    public bool isBattery = false;
    public bool isBible = false;
    public bool isCross = false;
    public bool isHolywater = false;

    //이번 판에 몬스터가 한 번이라도 드롭했는지를 확인함. (mosterdrop.cs에서 사용하게 됨.)
    public bool isDropWalkie = false;
    public bool isDropBattery = false;
    public bool isDropBible = false;
    public bool isDropCross = false;
    public bool isDropHolywater = false;

    //엔딩 플래그
    public bool isEndingEscape = false;
    public bool isEndingExorcism = false;

    void Start()
    {
        //드롭템 인벤토리 초기화
        Slot1.SetActive(true);
        Slot2.SetActive(true);
        Slot3.SetActive(true);
        IconWalkie.SetActive(false);
        IconBattery.SetActive(false);
        IconBible.SetActive(false);
        IconCross.SetActive(false);
        IconHolywater.SetActive(false);
    }

    void Update()
    {
        if (isWalkie && isBattery && !isEndingEscape)
        {
            isEndingEscape = true;
            Debug.Log("노말엔딩 출력!");
        }

        if(isBible && isCross && isHolywater && !isEndingExorcism)
        {
            isEndingExorcism = true;
            Debug.Log("히든엔딩 출력!");
        }
    }
}
