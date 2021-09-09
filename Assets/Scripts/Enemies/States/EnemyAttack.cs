using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : IState
{
    public EnemyCombat enemyCombat;
    Animator animator;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public EnemyAttack(EnemyCombat enemyCombat_, Animator animator_)
    {
        enemyCombat = enemyCombat_;
        animator = animator_;
    }
    
    public void OnStateEnter()
    {
        animator.SetTrigger("Attack");
    }

    public void StateUpdate()
    {   
        if (Time.time >= nextAttackTime)
        {   
            enemyCombat.Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void OnStateExit()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
    }
}
