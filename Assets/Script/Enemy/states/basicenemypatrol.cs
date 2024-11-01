using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemypatrol : MainState
{
    public basicenemypatrol(MainEnemy enemy) : base(enemy) {

    }

    public override void enter() {
    startTime = Time.time;
   }
   
   public override void logic()
    {
        enemy.getRb.velocity += Vector2.right * 0.005f;
        if (startTime + 5 < Time.time)
            enemy.changestate(enemy.idle);
    }
}
