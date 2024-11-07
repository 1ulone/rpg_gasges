using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemychase : MainState 
{
	protected Vector2 chaseDirection;
	public basicenemychase(MainEnemy enemy) : base(enemy) {}

	public override void enter()
	{
		ChangeDirection();
	}

   	public override void logic()
    {
		enemy.getRb.velocity = chaseDirection.normalized * enemy.data.movementSpeed * 1.5f;
		if (onAttack)
			enemy.changestate(enemy.attack);

		if (startTime + 1f < Time.time)
			ChangeDirection();
    }

	private void ChangeDirection()
	{
		chaseDirection = enemy.getDirectionToPlayer();
		Vector2 dirAnim = enemy.getDirectionFromVelocity();
		enemy.direction = dirAnim;

		enemy.playAnimation(enemy.data.walk, enemy.getDirectionTag(dirAnim));
		startTime = Time.time;
	}
}
