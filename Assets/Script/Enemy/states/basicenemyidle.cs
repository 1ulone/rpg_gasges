using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemyidle : MainState
{
    public basicenemyidle(MainEnemy enemy) : base(enemy) {

    }

	public override void enter() 
	{
		enemy.playAnimation(enemy.data.idle, enemy.CurrentDirection);
		enemy.getRb.velocity = Vector2.zero;
		startTime = Time.time;
	}

	public override void logic()
    {
        if (startTime + 2 < Time.time)
            enemy.changestate(enemy.patrol);

		if (onPlayer && enemy.prevState != enemy.cooldown)
			enemy.changestate(enemy.chase);
    }
}
