using UnityEngine;

public class DropBattery : MonoBehaviour, IItem
{
    public GameObject iconBattery;
    public DropInventory inventory;
    public void Use(GameObject target)
    {
        inventory.isBattery = true;
        iconBattery.SetActive(true);
        Destroy(gameObject);
    }
}
