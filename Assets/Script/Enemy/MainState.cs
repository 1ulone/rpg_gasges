using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState
{
    protected MainEnemy enemy;

    protected float startTime;
	protected bool onPlayer;
	protected bool onAttack;

    public MainState(MainEnemy enemy) 
	{
        this.enemy = enemy;
    }

    public virtual void enter() 
	{
    }

    public virtual void exit() 
	{
    }

    public virtual void logic()
	{
    }

    public virtual void fixedlogic() 
	{
//		onPlayer = Physics2D.Raycast(enemy.transform.position, enemy.direction, enemy.data.chaseViewLength, enemy.playerMask);
//		onAttack = Physics2D.Raycast(enemy.transform.position, enemy.direction, enemy.data.attackViewLength, enemy.playerMask);

		onPlayer = Physics2D.OverlapCircle(enemy.transform.position, enemy.data.chaseViewLength, enemy.playerMask);
		onAttack = Physics2D.OverlapCircle(enemy.transform.position, enemy.data.attackViewLength, enemy.playerMask);

		Debug.DrawRay(enemy.transform.position, enemy.direction*10, Color.red);
    }
}
