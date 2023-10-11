using UnityEngine;

public class DropHolywater : MonoBehaviour, IItem
{
    public GameObject IconHolywater;
    public GameObject Slot3;
    public DropInventory Inventory;

    public void Use(GameObject target)
    {
        Inventory.isHolywater = true;
        IconHolywater.SetActive(true);
        Slot3.SetActive(false);
        Destroy(gameObject);
    }
}
