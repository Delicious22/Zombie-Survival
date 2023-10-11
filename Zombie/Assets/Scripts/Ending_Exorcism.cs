using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_Exorcism : MonoBehaviour
{
    public Animator Animator;
    public float TransitionTime = 0.8f;

    public DropInventory DropInventory;
    public GameObject Scoreboard; //여기에 스코어보드 오브젝트 연결!
    public bool toScoreboard = false;
    
    void Update()
    {
        if (toScoreboard)
        {
            Scoreboard.SetActive(true); //연결한 스코어보드 on!
            toScoreboard = false; //두번 이상 실행되지 않게 false로 바꿔준다.

            gameObject.SetActive(false);//자신은 비활성화.
        }
    }

    private void OnEnable()
    {
        if (DropInventory.isEndingExorcism)
        {
            Debug.Log("exorcism 애니메이션을 재생할까?");
            StartCoroutine(playEnding(TransitionTime)); //0.8f후 애니메이션 재생.
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
        DropInventory.isHolywater = false;
        DropInventory.isEndingExorcism = false;
        DropInventory.isCross = false;
        DropInventory.isBible = false; //반드시 이 순서대로 해주십시오.

        toScoreboard = true;
    }
}
