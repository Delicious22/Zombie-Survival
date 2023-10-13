using System.Runtime.CompilerServices;
using UnityEngine;

public class DropCross : MonoBehaviour, IItem
{
    public GameObject iconCross;
    public GameObject slot2;
    public DropInventory inventory;
    public static bool isPicked = false;
    public bool IsPicked { get { return isPicked; } }

    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isCross = true;
        iconCross.SetActive(true);
        slot2.SetActive(false);
        Destroy(gameObject);
    }

    public void SetPicked(bool picked)
    {
        isPicked = picked;
    }

}
