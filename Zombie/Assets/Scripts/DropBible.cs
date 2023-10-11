using UnityEngine;

public class DropBible : MonoBehaviour, IItem
{
    public GameObject iconBible;
    public GameObject slot1;
    public DropInventory inventory;

    public void Use(GameObject target)
    {
        inventory.isBible = true;
        iconBible.SetActive(true);
        slot1.SetActive(false);
        Destroy(gameObject);
    }
}
