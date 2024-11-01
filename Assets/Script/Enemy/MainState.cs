using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState
{
    protected MainEnemy enemy;
    protected float startTime;

    public MainState(MainEnemy enemy) {
        this.enemy = enemy;
    }

    public virtual void enter() {

    }

    public virtual void exit() {

    }

    public virtual void logic(){

    }

    public virtual void fixedlogic() {
        
    }
}
