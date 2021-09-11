using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat: MonoBehaviour
{
    public Animator animator;    
    public Transform attackPoint;
    public LayerMask playerLayer;

    private int numAttack = 0;

    int currentHealth;

    public float attackRange = 0.5f;
    public int MaxHealth {get; protected set;} = 10;
    public int AttackDamage {get; protected set;} = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool CanAttack()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        return (players.Length != 0);
    }
    

    public void Attack()
    {     
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach(Collider2D player in players){
             player.GetComponent<PlayerState>().TakeDamage(AttackDamage);
        }
        numAttack += 1;
    }


    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }


    void Die()
    {
        animator.SetTrigger("Death");
        
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
