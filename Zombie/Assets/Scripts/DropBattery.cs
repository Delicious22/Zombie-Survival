using UnityEngine;

public class DropBattery : MonoBehaviour, IItem
{
    public GameObject iconBattery;
    public DropInventory inventory;
    private static bool isPicked = false;

    public bool IsPicked { get { return isPicked; } }

    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isBattery = true;
        iconBattery.SetActive(true);
        Destroy(gameObject);
    }
}
