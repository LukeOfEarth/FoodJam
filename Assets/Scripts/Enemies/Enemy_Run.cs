using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    public Enemy enemy;
    public EnemyCombat enemyCombat;
    Rigidbody2D rigidbody;
    Transform target;
    Transform self;

    public float speed = 2.5f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        self = animator.GetComponent<Transform>();
        rigidbody = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        enemyCombat = animator.GetComponent<EnemyCombat>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyCombat.CanAttack())
        {
            animator.SetTrigger("Attack");
        }
        else if (!enemy.TargetInSight(target, self))
        {
            animator.SetTrigger("Idle");
        }
        else {
            Vector2 targetPos = new Vector2(target.position.x, self.position.y); // can only move horizontally
            Vector2 newPos = Vector2.MoveTowards(rigidbody.position, targetPos, speed * Time.fixedDeltaTime); 
            rigidbody.MovePosition(newPos);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
