using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemyhurt : MainState 
{
	public basicenemyhurt(MainEnemy enemy) : base(enemy) {}

   	public override void enter()
   	{
		enemy.getRb.velocity = Vector2.zero;

		Vector3 pos = GameObject.FindObjectOfType<PlayerController>().transform.position;
		Vector2 dirTo = pos - enemy.transform.position;
		dirTo = new Vector2((int)dirTo.x, (int)dirTo.y);

		enemy.direction = dirTo;
		enemy.playAnimation(enemy.data.idle, enemy.getDirectionTag(enemy.direction));
		enemy.StartCoroutine(enemy.hitflash());
		HitStop.instances.Init(0.1f);
		
		startTime = Time.time;
   	}

   	public override void logic()
    {
		if (startTime + 2f < Time.time)
			enemy.changestate(enemy.idle);
   	}
}
