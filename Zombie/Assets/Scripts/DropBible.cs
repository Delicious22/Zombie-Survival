using UnityEngine;

public class DropBible : MonoBehaviour, IItem
{
    public GameObject IconBible;
    public GameObject Slot1;
    public DropInventory Inventory;

    public void Use(GameObject target)
    {
        Inventory.isBible = true;
        IconBible.SetActive(true);
        Slot1.SetActive(false);
        Destroy(gameObject);
    }
}
