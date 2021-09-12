using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

		stateMachine = new StateMachine();
		var enemyFly = new StateFly(GetComponent<Rigidbody2D>(), player, animator, speed);
		var enemyIdle = new StateIdle(animator);
		var enemyAttack = new StateAttack(GetComponent<EnemyCombat>(), animator, attackRate, "Fly_Attack");

		stateMachine.AddTransition(enemyIdle, enemyFly, () => TargetInSight(player, transform));
        stateMachine.AddTransition(enemyAttack, enemyIdle, () => !enemyCombat.CanAttack());

		stateMachine.AddAnyTransition(enemyIdle, () => !TargetInSight(player, transform));
		stateMachine.AddAnyTransition(enemyAttack, () => enemyCombat.CanAttack());

		stateMachine.SetState(enemyIdle);
    }
}
