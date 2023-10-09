using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterDrop : MonoBehaviour
{
    public DropInventory inventory; //�̰� �����ؾ� ������ ����ߴ��� Ȯ�ΰ���.

    [SerializeField] float hidden = 0.2f; //����������� ���� Ȯ��. ���� ����.
    public float pickedNum; //�����Լ��� �Ź� ���� ���� �Ǽ�.

    public GameObject[] items;
    //Walkie =0, Battery=1
    //Bible =2, Cross =3, Holywater =4 �� �Ҵ��.


    //private void Update()
    //{
    //    DropTable(); //�׽�Ʈ�� ����.
    //}



    //���⸦ Ȯ�����ּ���!
    //�� ��ũ��Ʈ�� ���� �����տ� �޾��ְ�,
    //Enemy.cs ��ũ��Ʈ���� Die ó�� �ȿ��� �� DropTable()�Լ��� ȣ�����ּ���.
    //���Ͱ� �����鼭 ���� Ȯ���� �������� ������ �˴ϴ�.
    public void DropTable() 
    {
        pickedNum = Random.value; //0f���� 1.0f ������ �Ǽ��� ��ȯ��.
        Debug.Log("�� ����"+ pickedNum);
        if (pickedNum<(1-hidden))
        {
            if (0.3f <= pickedNum && pickedNum < (1-hidden) && !inventory.droppedBattery)
            {
                Debug.Log("���͸� �����.");
                inventory.droppedBattery = true;
                DropItem(2);
            }
            Debug.Log("�ƹ��͵� ��ӵ��� �ʾҴ�.");
        }
        else if((1 - hidden)<=pickedNum)
        {
            //����, ���ڰ�, ���� ������ ��ӵ�.
            if(!inventory.droppedHolywater)
            {
                Debug.Log("������ �����.");
                inventory.droppedHolywater = true;
                DropItem(4);
            }
            else if(!inventory.droppedCross)
            {
                Debug.Log("���ڰ��� �����.");
                inventory.droppedCross = true;
                DropItem(3);
            }
            else if(!inventory.droppedBible)
            {
                Debug.Log("������ �����.");
                inventory.droppedBible = true;
                DropItem(2);
            }
            else
                Debug.Log("�ƹ��͵� ��ӵ��� �ʾҴ�.");
        }
    }

    private void DropItem(int DropItemNumber) //���� ������ instantiate�ϴ� �Լ�.
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
