using UnityEngine;

public class DropBattery : MonoBehaviour, IItem
{
    public GameObject IconBattery;
    public DropInventory Inventory;
    public void Use(GameObject target)
    {
        Inventory.isBattery = true;
        IconBattery.SetActive(true);
        Destroy(gameObject);
    }
}
