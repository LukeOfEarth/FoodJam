using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    [SerializeField] private LayerMask m_WhatIsGround;   
	[SerializeField] private Transform m_GroundCheck;   
	public bool m_Grounded; 
	const float k_GroundedRadius = .2f;

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

		stateMachine = new StateMachine();
		var enemyRun = new StateRun(GetComponent<Rigidbody2D>(), player, animator, speed);
		var enemyIdle = new StateIdle(animator);
		var enemyAttack = new StateAttack(GetComponent<EnemyCombat>(), animator, attackRate, "Goblin_Attack");
		var enemyAir = new StateFalling(animator);

		stateMachine.AddTransition(enemyIdle, enemyRun, () => TargetInSight(player, transform));
        stateMachine.AddTransition(enemyAttack, enemyIdle, () => !enemyCombat.CanAttack());
		stateMachine.AddTransition(enemyAir, enemyIdle, () => m_Grounded);

		stateMachine.AddAnyTransition(enemyAir, () => !m_Grounded);
		stateMachine.AddAnyTransition(enemyIdle, () => !TargetInSight(player, transform));
		stateMachine.AddAnyTransition(enemyAttack, () => enemyCombat.CanAttack());

		stateMachine.SetState(enemyIdle);
    }

	public override void FixedUpdate()
	{
		LookAtPlayer();
		stateMachine.UpdateStateMachine();
		
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				break;
			}
		}
	}
}
