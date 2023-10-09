using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_Exorcism : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 0.8f;

    public DropInventory dropinventory;
    public GameObject scoreboard; //���⿡ ���ھ�� ������Ʈ ����!
    public bool toScoreboard = false;

    void Update()
    {
        if (toScoreboard)
        {
            scoreboard.SetActive(true); //������ ���ھ�� on!
            toScoreboard = false; //�ι� �̻� ������� �ʰ� false�� �ٲ��ش�.

            gameObject.SetActive(false);//�ڽ��� ��Ȱ��ȭ.
        }
    }

    private void OnEnable()
    {
        if (dropinventory.endingExorcism)
        {
            Debug.Log("exorcism �ִϸ��̼��� ����ұ�?");
            StartCoroutine(playEnding(transitionTime)); //0.8f�� �ִϸ��̼� ���.
        }
    }

    
    IEnumerator playEnding(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Time.timeScale = 0f;
        //start animation����Ͽ� ����������Ʈ��(�׸�, �ؽ�Ʈ)�� ���İ��� 100���� ����� ������.
        animator.SetTrigger("Start");
        Debug.Log("exorcism �ִϸ��̼� start Ʈ����");
    }

    public void endEnding() //��ư�� onclick���� ����Ǵ� �Լ�.
    {
        //end animation����Ͽ� ����������Ʈ��(�׸�, �ؽ�Ʈ)�� ���İ��� 0���� �ǵ���.
        animator.SetTrigger("End");
        Debug.Log("exorcism �ִϸ��̼� end Ʈ����");

        //������ 2���̻� ������� �ʰ� bool �ٲٱ�.
        dropinventory.isHolywater = false;
        dropinventory.endingExorcism = false;
        dropinventory.isCross = false;
        dropinventory.isBible = false; //�ݵ�� �� ������� ���ֽʽÿ�.

        toScoreboard = true;
    }
}
