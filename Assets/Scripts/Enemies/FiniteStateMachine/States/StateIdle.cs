using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    Animator animator;

    public StateIdle(Animator animator_)
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
