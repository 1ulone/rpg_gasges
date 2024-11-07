using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject 
{
	public float movementSpeed;
	public int damage; 
	public int maxHealth = 5; 

	public float idleLength = 10;
	public float patrolLength = 5;
	public float chaseViewLength = 5;
	public float attackViewLength = 2;
	public float cooldownLength = 5;

	public string idle = "enemy_idle_";
	public string walk = "enemy_walk_";
	public string attack = "enemy_attack_";

}
