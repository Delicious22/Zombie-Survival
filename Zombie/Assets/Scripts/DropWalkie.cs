using UnityEngine;

public class DropWalkie : MonoBehaviour, IItem
{
    public GameObject iconWalkie;
    public DropInventory inventory;
    public void Use(GameObject target)
    {
        inventory.isWalkie = true;
        iconWalkie.SetActive(true);
        Destroy(gameObject);
    }
}
