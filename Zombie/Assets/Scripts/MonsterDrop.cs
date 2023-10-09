using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterDrop : MonoBehaviour
{
    public DropInventory inventory; //이걸 연결해야 뭐뭐를 드롭했는지 확인가능.

    [SerializeField] float hidden = 0.2f; //히든아이템이 나올 확률. 조정 가능.
    public float pickedNum; //랜덤함수가 매번 새로 뽑을 실수.

    public GameObject[] items;
    //Walkie =0, Battery=1
    //Bible =2, Cross =3, Holywater =4 에 할당됨.


    //private void Update()
    //{
    //    DropTable(); //테스트용 라인.
    //}



    //여기를 확인해주세요!
    //이 스크립트를 몬스터 프리팹에 달아주고,
    //Enemy.cs 스크립트에서 Die 처리 안에서 이 DropTable()함수를 호출해주세요.
    //몬스터가 죽으면서 일정 확률로 아이템을 떨구게 됩니다.
    public void DropTable() 
    {
        pickedNum = Random.value; //0f부터 1.0f 사이의 실수를 반환함.
        Debug.Log("고른 값은"+ pickedNum);
        if (pickedNum<(1-hidden))
        {
            if (0.3f <= pickedNum && pickedNum < (1-hidden) && !inventory.droppedBattery)
            {
                Debug.Log("배터리 드롭함.");
                inventory.droppedBattery = true;
                DropItem(2);
            }
            Debug.Log("아무것도 드롭되지 않았다.");
        }
        else if((1 - hidden)<=pickedNum)
        {
            //성수, 십자가, 성경 순으로 드롭됨.
            if(!inventory.droppedHolywater)
            {
                Debug.Log("성수를 드롭함.");
                inventory.droppedHolywater = true;
                DropItem(4);
            }
            else if(!inventory.droppedCross)
            {
                Debug.Log("십자가를 드롭함.");
                inventory.droppedCross = true;
                DropItem(3);
            }
            else if(!inventory.droppedBible)
            {
                Debug.Log("성경을 드롭함.");
                inventory.droppedBible = true;
                DropItem(2);
            }
            else
                Debug.Log("아무것도 드롭되지 않았다.");
        }
    }

    private void DropItem(int DropItemNumber) //실제 아이템 instantiate하는 함수.
    {
        Vector3 spawnPosition = gameObject.transform.position + Vector3.up * 0.5f;
        GameObject selectedItem = items[DropItemNumber];
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
