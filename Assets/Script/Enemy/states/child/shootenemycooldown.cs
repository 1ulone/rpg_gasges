using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootenemycooldown : basicenemycooldown 
{
	public shootenemycooldown(MainEnemy enemy) : base(enemy) {}

   	public override void enter()
    {
		enemy.playAnimation(enemy.data.walk, enemy.CurrentDirection);
		enemy.direction = -enemy.getDirectionFromAnimation();
		startTime = Time.time;
   	}

   	public override void logic()
	{
		enemy.getRb.velocity = enemy.direction * (enemy.data.movementSpeed/2);
		if (startTime + enemy.data.cooldownLength < Time.time)
			enemy.changestate(enemy.idle);
	}
}
