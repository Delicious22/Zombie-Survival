using UnityEngine;

public class DropWalkie : MonoBehaviour, IItem
{
    public GameObject IconWalkie;
    public DropInventory Inventory;
    public void Use(GameObject target)
    {
        Inventory.isWalkie = true;
        IconWalkie.SetActive(true);
        Destroy(gameObject);
    }
}
