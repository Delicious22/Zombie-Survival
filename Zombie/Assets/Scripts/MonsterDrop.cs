using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterDrop : MonoBehaviour
{
    public DropInventory DropInventory; //이걸 연결해야 뭐뭐를 드롭했는지 확인가능.

    public float Hidden = 0.2f; //히든아이템이 나올 확률. 조정 가능.
    public float WalkiePercent = 0.1f; //무전기가 나올 가능성.
    public float PickedNum; //랜덤함수가 매번 새로 뽑을 실수.

    public GameObject[] Items;
    //Walkie =0, Battery=1
    //Bible =2, Cross =3, Holywater =4 에 할당됨.

    private void Update()
    {
        DropTable();
    }

    //여기를 확인해주세요!
    //이 스크립트를 몬스터 프리팹에 달아주고,
    //Enemy.cs 스크립트에서 Die 처리 안에서 이 DropTable()함수를 호출해주세요.
    //몬스터가 죽으면서 일정 확률로 아이템을 떨구게 됩니다.
    public void DropTable() 
    {
        PickedNum = Random.value; //0f부터 1.0f 사이의 실수를 반환함.
        Debug.Log("고른 값은"+ PickedNum);
        if (PickedNum<(1-Hidden - WalkiePercent))
        {
            if (0.3f <= PickedNum && PickedNum < (1-Hidden - WalkiePercent) && !DropInventory.isDropBattery)
            {
                Debug.Log("배터리 드롭함.");
                DropInventory.isDropBattery = true;
                DropItem(1);
            }
            Debug.Log("아무것도 드롭되지 않았다.");
        }
        else if((1 - Hidden - WalkiePercent)<= PickedNum && PickedNum< (1 - Hidden) && !DropInventory.isDropWalkie)
        {
            Debug.Log("무전기 드롭함.");
            DropInventory.isDropBattery = true;
            DropItem(0);
        }
        else if((1 - Hidden)<=PickedNum)
        {
            //성수, 십자가, 성경 순으로 드롭됨.
            if(!DropInventory.isDropHolywater)
            {
                Debug.Log("성수를 드롭함.");
                DropInventory.isDropHolywater = true;
                DropItem(4);
            }
            else if(!DropInventory.isDropCross)
            {
                Debug.Log("십자가를 드롭함.");
                DropInventory.isDropCross = true;
                DropItem(3);
            }
            else if(!DropInventory.isDropBible)
            {
                Debug.Log("성경을 드롭함.");
                DropInventory.isDropBible = true;
                DropItem(2);
            }
            else
                Debug.Log("아무것도 드롭되지 않았다.");
        }
    }

    private void DropItem(int DropItemNumber) //실제 아이템 instantiate하는 함수.
    {
        Vector3 spawnPosition = gameObject.transform.position + Vector3.up * 0.5f;
        GameObject selectedItem = Items[DropItemNumber];
        //Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        switch (DropItemNumber)
        {
            case 0:
                Instantiate(selectedItem, spawnPosition, Quaternion.Euler(new Vector3 (-25,-155,10)));
                break;
            case 1:
                Instantiate(selectedItem, spawnPosition, Quaternion.Euler(new Vector3(-210, 70, 270)));
                break;
            case 2:
                Instantiate(selectedItem, spawnPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
                break;
            case 3:
                Instantiate(selectedItem, spawnPosition, Quaternion.Euler(new Vector3(35, 100, 0)));
                break;
            case 4:
                Instantiate(selectedItem, spawnPosition, Quaternion.Euler(new Vector3(15, -10, -5)));
                break;
        }

    }
}
