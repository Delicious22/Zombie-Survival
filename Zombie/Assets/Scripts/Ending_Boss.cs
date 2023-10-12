using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Boss : MonoBehaviour
{
    public Animator Animator;
    private float transitionTime = 1f;

    private void OnEnable()
    {
        if (GameManager.instance.isBossClear)
        {
            Debug.Log("Boss 애니메이션을 재생할까?");
            StartCoroutine(playEnding(transitionTime)); //0.8f후 애니메이션 재생.
        }
    }


    IEnumerator playEnding(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Time.timeScale = 0f;
        //start animation재생하여 엔딩오브젝트들(그림, 텍스트)의 알파값을 100으로 만들며 보여줌.
        Animator.SetTrigger("Start");
        Debug.Log("Boss 애니메이션 start 트리거");
    }

    public void endEnding() //버튼의 onclick으로 실행되는 함수.
    {
        //end animation재생하여 엔딩오브젝트들(그림, 텍스트)의 알파값을 0으로 되돌림.
        Animator.SetTrigger("End");
        Debug.Log("exorcism 애니메이션 end 트리거");

        GameRestart();
    }
    public void GameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
