using System.Runtime.CompilerServices;
using UnityEngine;

public class DropWalkie : MonoBehaviour, IItem
{
    public GameObject iconWalkie;
    public DropInventory inventory;
    public static bool isPicked = false;
    public bool IsPicked { get { return isPicked; } }

    public void Use(GameObject target)
    {
        isPicked = true;
        inventory.isWalkie = true;
        iconWalkie.SetActive(true);
        Destroy(gameObject);
    }

    public void SetPicked(bool picked)
    {
        isPicked = picked;
    }

}
