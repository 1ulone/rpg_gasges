using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemyattack : MainState 
{
    public basicenemyattack(MainEnemy enemy) : base(enemy) {

    }

	public override void enter() 
	{
		Vector2 dirAnim = enemy.getDirectionFromVelocity();
		enemy.playAnimation(enemy.data.attack, enemy.getDirectionTag(dirAnim));
		enemy.getRb.velocity = Vector2.zero;
	}
}
