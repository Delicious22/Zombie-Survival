using UnityEngine;

public class DropBattery : MonoBehaviour, IItem
{
    public GameObject iconBattery;
    public DropInventory inventory;
    public static bool isPicked = false;

    public bool IsPicked { get { return isPicked; } }

    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isBattery = true;
        iconBattery.SetActive(true);
        Destroy(gameObject);
    }

    public void SetPicked(bool picked)
    {
        isPicked = picked;
    }

}
