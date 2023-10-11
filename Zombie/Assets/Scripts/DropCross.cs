using UnityEngine;

public class DropCross : MonoBehaviour, IItem
{
    public GameObject IconCross;
    public GameObject Slot2;
    public DropInventory Inventory;

    public void Use(GameObject target)
    {
        Inventory.isCross = true;
        IconCross.SetActive(true);
        Slot2.SetActive(false);
        Destroy(gameObject);
    }
}
