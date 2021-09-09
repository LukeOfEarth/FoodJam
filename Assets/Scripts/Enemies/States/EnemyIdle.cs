using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : IState
{
    Animator animator;

    public EnemyIdle(Animator animator_)
    {
        animator = animator_;
    }
    
    public void OnStateEnter()
    {
        animator.SetTrigger("Idle");
    }

    public void StateUpdate()
    {
    }

    public void OnStateExit()
    {
        animator.ResetTrigger("Idle");
    }
}
