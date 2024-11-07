using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemycooldown : MainState 
{
	public basicenemycooldown(MainEnemy enemy) : base(enemy) {}

	public override void enter()
	{
		enemy.playAnimation(enemy.data.idle, enemy.CurrentDirection);
		startTime = Time.time;
	}

   	public override void logic()
	{
		if (startTime + enemy.data.cooldownLength < Time.time)
			enemy.changestate(enemy.idle);
	}
}
