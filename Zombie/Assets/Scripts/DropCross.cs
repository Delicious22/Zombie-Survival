using UnityEngine;

public class DropCross : MonoBehaviour, IItem
{
    public GameObject iconCross;
    public GameObject slot2;
    public DropInventory inventory;

    public void Use(GameObject target)
    {
        inventory.isCross = true;
        iconCross.SetActive(true);
        slot2.SetActive(false);
        Destroy(gameObject);
    }
}
