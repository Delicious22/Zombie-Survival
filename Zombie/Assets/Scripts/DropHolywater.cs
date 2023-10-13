using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DropHolywater : MonoBehaviour, IItem
{
    public GameObject iconHolywater;
    public GameObject slot3;
    public DropInventory inventory;
    public static bool isPicked = false;
    public bool IsPicked { get { return isPicked; } }


    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isHolywater = true;
        iconHolywater.SetActive(true);
        slot3.SetActive(false);
        Destroy(gameObject);
    }

    public void SetPicked(bool picked)
    {
        isPicked = picked;
    }

}
