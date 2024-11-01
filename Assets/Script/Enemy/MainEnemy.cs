using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour
{
    protected MainState state;
    protected Rigidbody2D rb;
    public basicenemyidle idle { get; private set; }
    public basicenemypatrol patrol { get; private set; }

    public Rigidbody2D getRb { get; private set; }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        getRb = rb;
        idle = new basicenemyidle(this);
        patrol = new basicenemypatrol(this);
        state = null;
        changestate(idle);
    }

    // Update is called once per frame
    void Update() //check new condition
    {
        state?.logic();
    }

    void FixedUpdate() {  //check phisical condition
        state?.fixedlogic();
    }

    public void changestate(MainState newstate) {
        if (state == newstate)
            return;
        
        state?.exit();
        state = newstate;
        state.enter();
    }


}
