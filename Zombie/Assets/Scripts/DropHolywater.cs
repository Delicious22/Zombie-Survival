using UnityEngine;

public class DropHolywater : MonoBehaviour, IItem
{
    public GameObject iconHolywater;
    public GameObject slot3;
    public DropInventory inventory;

    public void Use(GameObject target)
    {
        inventory.isHolywater = true;
        iconHolywater.SetActive(true);
        slot3.SetActive(false);
        Destroy(gameObject);
    }
}
