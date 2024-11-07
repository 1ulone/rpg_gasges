using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MainEnemy 
{
	[SerializeField] private string projectileTag;

	protected override void Initiate()
	{
	    idle = new basicenemyidle(this);
        patrol = new basicenemypatrol(this);
		chase = new basicenemychase(this);
		attack = new basicenemyattack(this);
		cooldown = new shootenemycooldown(this);
		hurt = new basicenemyhurt(this);
	}

   	public override void onAttackEvent()
   	{
		float angle = 0;
		if (currentDirection.Contains("down")) { angle = -90f; } else 
		if (currentDirection.Contains("up")) { angle = 90f; } else 
		if (currentDirection.Contains("right")) { angle = 0f; } else 
		if (currentDirection.Contains("left")) { angle = 180f; }  

		GameObject projectile = Pool.instances.create(projectileTag, transform.position + (Vector3)(getDirectionFromAnimation() * 1f), new Vector3(0, 0, angle));
		projectile.GetComponent<DamageComponent>().SetDamage(data.damage);
		projectile.GetComponent<Rigidbody2D>().velocity = getDirectionFromAnimation() * 5f;
    }
}
