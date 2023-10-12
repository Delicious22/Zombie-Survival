using System.Runtime.CompilerServices;
using UnityEngine;

public class DropCross : MonoBehaviour, IItem
{
    public GameObject iconCross;
    public GameObject slot2;
    public DropInventory inventory;
    private static bool isPicked = false;
    public bool IsPicked { get { return isPicked; } }

    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isCross = true;
        iconCross.SetActive(true);
        slot2.SetActive(false);
        Destroy(gameObject);
    }
}
