using UnityEngine;

public class DropBible : MonoBehaviour, IItem
{
    public GameObject iconBible;
    public GameObject slot1;
    public DropInventory inventory;
    private static bool isPicked = false;
    public bool IsPicked { get { return isPicked; } }

    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isBible = true;
        iconBible.SetActive(true);
        slot1.SetActive(false);
        Destroy(gameObject);
    }
}
