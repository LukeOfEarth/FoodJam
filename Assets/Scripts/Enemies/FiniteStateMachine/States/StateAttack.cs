using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IState
{
    public EnemyCombat enemyCombat;
    Animator animator;

    public float attackRate = 1f; // strikes/second
    public float defaultAttackInterval = 0.1f;
    float nextAttackTime = 0f;
    float damageTime = 0f;
    string anim_name = "Attack";

    float damageInterval = -1;
    


    public StateAttack(EnemyCombat enemyCombat_, Animator animator_, float attackRate_, string anim_name_="Attack")
    {
        enemyCombat = enemyCombat_;
        animator = animator_;
        attackRate = attackRate_;
        anim_name = anim_name_;
    }
    

    public void OnStateEnter()
    {
        animator.SetTrigger("Attack");
        float anim_length = GetAnimationClipLength(anim_name);
        damageInterval = anim_length > 0 ? Mathf.Min(0.8f * anim_length, 1f/attackRate) : defaultAttackInterval;
    }


    public void StateUpdate()
    {   
        float currentTime = Time.time;
        if (currentTime > nextAttackTime)
        {   
            animator.SetBool("IsCooldown", false);
            damageTime = currentTime + damageInterval;
            nextAttackTime = currentTime + 1f / attackRate;
        }
        else if (currentTime > damageTime){
            enemyCombat.Attack();
            damageTime = nextAttackTime + 100f; // will be reset
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


    public float GetAnimationClipLength(string name)
    {
        float length = -1f;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if ((clip.name) == name)
            {
                length = clip.length;
                break;
            }
        }
        return length;
    }
}
