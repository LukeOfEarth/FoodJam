using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat: MonoBehaviour
{
    public Animator animator;    
    public Transform attackPoint;
    public LayerMask playerLayer;

    public AudioClip damageSound;
    public AudioClip attackSound;
    public GameObject soundFx;

    public GameObject scoreFX;

    private int numAttack = 0;

    int currentHealth;

    public float attackRange = 0.5f;
    public int MaxHealth {get; protected set;} = 1;
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
        if(attackSound != null)
        {
            GameObject sound = Instantiate(soundFx);
            sound.GetComponent<SoundFX>().PlaySound(attackSound);
        }
    }


    public void TakeDamage(int damage)
    {
        //animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if(soundFx != null)
        {
            PlayDamageSound();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }


    void Die()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>().score += 100;
        Instantiate(scoreFX, this.gameObject.transform.position, scoreFX.transform.rotation);
        animator.enabled = false;
        if (GetComponent<GroundEnemy>())
        {
            GetComponent<GroundEnemy>().enabled = false;
        }

        if (GetComponent<AirEnemy>())
        {
            GetComponent<AirEnemy>().enabled = false;
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 5;
        this.enabled = false;
        StartCoroutine("Kill");
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator Kill()
    {
        StartCoroutine("Flash");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.01f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;
        StartCoroutine("Flash");
    }

    void PlayDamageSound()
    {
        GameObject sound = Instantiate(soundFx);
        sound.GetComponent<SoundFX>().PlaySound(damageSound);
    }
}
