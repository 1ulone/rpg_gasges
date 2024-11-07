using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
	public int damage { get; private set; }
	public void SetDamage(int i) { damage = i; }
}
