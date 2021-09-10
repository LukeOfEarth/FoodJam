using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IState
{
    public EnemyCombat enemyCombat;
    Animator animator;

    public float attackRate = 1f; // strikes/second
    float nextAttackTime = 0f;


    public StateAttack(EnemyCombat enemyCombat_, Animator animator_)
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
            animator.SetBool("IsCooldown", false);
            enemyCombat.Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
        else{
            animator.SetBool("IsCooldown", true);
        }
    }


    public void OnStateExit()
    {
        //animator.SetBool("IsCooldown", false);
        animator.ResetTrigger("Attack");
    }
}
