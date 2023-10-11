using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public DropInventory dropinventory;
    public GameObject Ending_Escape;
    public GameObject Ending_Exorcism;

    void Update()
    {
        if(dropinventory.isEndingEscape)
        {
            Ending_Escape.SetActive(true);
        }

        if(dropinventory.isEndingExorcism)
        {
            Ending_Exorcism.SetActive(true);
        }
    }
}