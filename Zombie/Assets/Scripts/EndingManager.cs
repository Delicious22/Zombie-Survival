using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public DropInventory dropinventory;
    public GameObject Ending_Escape;
    public GameObject Ending_Exorcism;
    public GameObject Ending_Boss;

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

        if(GameManager.instance.isBossClear == true)
        {
            Ending_Boss.SetActive(true);
        }
    }
}
