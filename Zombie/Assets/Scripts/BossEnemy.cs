using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public bool isKicked = false;
    public bool isInArea = false;
    public bool isRushed = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if (dead == true || Time.time < lastAttackTime + timeBetAttack) return;

        if (!isKicked && isInArea)
        {
            StartCoroutine(KickCoroutine());
        }
    }

    private void PausePathFinder(bool isPause)
    {
        if (isPause == true)
        {
            StopCoroutine(updatePathCoroutine);
            pathFinder.isStopped = true;
            pathFinder.enabled = false;
        }
        else
        {
            pathFinder.enabled = true;
            StartCoroutine(updatePathCoroutine);
        }
    }

    IEnumerator KickCoroutine()
    {
        isKicked = true;

        PausePathFinder(true);

        enemyAnimator.SetTrigger("Kick");
        yield return new WaitForSeconds(0.5f);

        if (isInArea == true)
        {
            targetEntity.OnDamage(damage, transform.position, targetEntity.transform.position - transform.position);
        }

        yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        lastAttackTime = Time.time;
        isKicked = false;

        PausePathFinder(false);
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (!dead)
        {
            if (targetEntity != null && other.transform == targetEntity.transform)
            {
                isInArea = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!dead && other.transform == targetEntity.transform)
        {
            isInArea = false;
        }
    }
}
