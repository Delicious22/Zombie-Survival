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

    //������� �κ��丮�� ������ �ֳ� �ƴѰ��� ��Ÿ���� bool.
    public bool isWalkie = false;
    public bool isBattery = false;
    public bool isBible = false;
    public bool isCross = false;
    public bool isHolywater = false;

    //�̹� �ǿ� ���Ͱ� �� ���̶� ����ߴ����� Ȯ����. (mosterdrop.cs���� ����ϰ� ��.)
    public bool droppedWalkie = false;
    public bool droppedBattery = false;
    public bool droppedBible = false;
    public bool droppedCross = false;
    public bool droppedHolywater = false;

    //���� �÷���
    public bool endingEscape = false;
    public bool endingExorcism = false;

    void Start()
    {
        //����� �κ��丮 �ʱ�ȭ
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
        if (isWalkie && isBattery && !endingEscape)
        {
            endingEscape = true;
            Debug.Log("�븻���� ���!");
        }

        if(isBible && isCross && isHolywater && !endingExorcism)
        {
            endingExorcism = true;
            Debug.Log("���翣�� ���!");
        }
    }
}
