using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemypatrol : MainState
{
    public basicenemypatrol(MainEnemy enemy) : base(enemy) {}

    public override void enter() 
	{
		enemy.direction = enemy.getRandomDirection(); 
		enemy.playAnimation(enemy.data.walk, enemy.getDirectionTag(enemy.direction));
		startTime = Time.time;
	}
   
	public override void logic()
    {
        enemy.getRb.velocity = enemy.direction * enemy.data.movementSpeed;
        if (startTime + 5 < Time.time)
            enemy.changestate(enemy.idle);

		if (onPlayer && enemy.prevState != enemy.cooldown)
			enemy.changestate(enemy.chase);
    }
}
