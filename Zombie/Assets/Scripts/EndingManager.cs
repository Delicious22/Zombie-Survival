using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public DropInventory DropInventory; //인벤토리 여기에 할당.
    public GameObject Ending_Escape;
    public GameObject Ending_Exorcism;

    void Update()
    {
        if(DropInventory.isEndingEscape)
        {
            Ending_Escape.SetActive(true);
        }

        if(DropInventory.isEndingExorcism)
        {
            Ending_Exorcism.SetActive(true);
        }
    }
}
