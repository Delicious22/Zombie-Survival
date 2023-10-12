using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Exorcism : MonoBehaviour
{
    public Animator Animator;
    private float transitionTime = 1f;
    
    public DropInventory Dropinventory;

    
    private void OnEnable()
    {
        if (Dropinventory.isEndingExorcism)
        {
            Debug.Log("exorcism 애니메이션을 재생할까?");
            StartCoroutine(playEnding(transitionTime)); //0.8f후 애니메이션 재생.
        }
    }

    
    IEnumerator playEnding(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Time.timeScale = 0f;
        //start animation재생하여 엔딩오브젝트들(그림, 텍스트)의 알파값을 100으로 만들며 보여줌.
        Animator.SetTrigger("Start");
        Debug.Log("exorcism 애니메이션 start 트리거");
    }

    public void endEnding() //버튼의 onclick으로 실행되는 함수.
    {
        //end animation재생하여 엔딩오브젝트들(그림, 텍스트)의 알파값을 0으로 되돌림.
        Animator.SetTrigger("End");
        Debug.Log("exorcism 애니메이션 end 트리거");

        //엔딩이 2번이상 재생되지 않게 bool 바꾸기.
        Dropinventory.isHolywater = false;
        Dropinventory.isEndingExorcism = false;
        Dropinventory.isCross = false;
        Dropinventory.isBible = false; //반드시 이 순서대로 해주십시오.

        GameRestart();
    }
    public void GameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
