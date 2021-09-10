using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFalling : IState
{
    Animator animator;

    public StateFalling(Animator animator_)
    {
        animator = animator_;
    }
    
    public void OnStateEnter()
    {
        animator.SetBool("InAir", true);
    }

    public void StateUpdate()
    {
        
    }

    public void OnStateExit()
    {
        animator.SetBool("InAir", false);
    }
}
