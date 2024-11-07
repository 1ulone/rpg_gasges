using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private string deathEffect;

	public void onDeath(Vector3 dpos)
	{
		Pool.instances.create(deathEffect, dpos, Vector3.zero);
		Pool.instances.destroy(this.gameObject);
	}
}
