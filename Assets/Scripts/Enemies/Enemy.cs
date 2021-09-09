using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
	public bool isFlipped = false;
	public int minLookHeight = 2;
	public float sightRange = 15f;
	public float speed = 2.5f;

	public Animator animator;
	public EnemyCombat enemyCombat;

	private StateMachine stateMachine;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

		stateMachine = new StateMachine();
		var enemyRun = new EnemyRun(GetComponent<Rigidbody2D>(), player, animator, speed);
		var enemyIdle = new EnemyIdle(animator);
		var enemyAttack = new EnemyAttack(GetComponent<EnemyCombat>(), animator);

		stateMachine.AddTransition(enemyIdle, enemyRun, () => TargetInSight(player, transform));
		stateMachine.AddAnyTransition(enemyIdle, () => !TargetInSight(player, transform));
		stateMachine.AddAnyTransition(enemyAttack, () => enemyCombat.CanAttack());
		stateMachine.AddTransition(enemyAttack, enemyIdle, () => !enemyCombat.CanAttack());


		stateMachine.SetState(enemyIdle);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
		stateMachine.UpdateStateMachine();
    }

	public void LookAtPlayer()
	{
		if (Mathf.Abs(transform.position.y - player.position.y) > minLookHeight){
			return;
		}
		
		
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}


	public bool TargetInSight(Transform target, Transform self)
    {
        Vector2 vecToTarget = new Vector2(target.position.x - self.position.x, target.position.y - self.position.y);
        float distanceToTarget = vecToTarget.magnitude;
        return (distanceToTarget <= sightRange) && (Mathf.Abs(target.position.y - self.position.y) <= minLookHeight);
    }

}