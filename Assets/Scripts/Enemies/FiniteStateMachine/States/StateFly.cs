using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFly : IState
{
    public EnemyCombat enemyCombat;
    Transform target;
    Rigidbody2D rigidbody;
    Animator animator;
    float speed = 2.5f;

    public StateFly(Rigidbody2D rigidbody_, Transform target_, Animator animator_, float speed_)
    {
        rigidbody = rigidbody_;
        target = target_;
        animator = animator_;
        speed = speed_;
    }
    
    public void OnStateEnter()
    {
        animator.SetTrigger("Fly");
    }

    public void StateUpdate()
    {
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, targetPos, speed * Time.fixedDeltaTime); 
        rigidbody.MovePosition(newPos);
    }

    public void OnStateExit()
    {
        animator.ResetTrigger("Fly");
    }
}
