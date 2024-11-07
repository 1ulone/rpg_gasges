using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainEnemy : MonoBehaviour
{
	protected string currentDirection;
	protected string currentState;
	protected int health;

    protected MainState state;
    protected Rigidbody2D rb;
	protected Animator anim;
	protected Vector2 prevVectorDir;
	protected DamageComponent hitbox;

	public Vector2 direction { get; set; }
	public string CurrentDirection { get { return currentDirection; } }

    public basicenemyidle idle { get; protected set; }
    public basicenemypatrol patrol { get; protected set; }
    public basicenemychase chase { get; protected set; }
    public basicenemyattack attack { get; protected set; }
    public basicenemycooldown cooldown { get; protected set; }
    public basicenemyhurt hurt { get; protected set; }

    public Rigidbody2D getRb { get { return rb; } }
    public MainState prevState { get; protected set; }

	[SerializeField] public EnemyData data;
	[SerializeField] public LayerMask playerMask;
	[SerializeField] protected Transform healthUI;
	[SerializeField] protected TextMeshPro status;

	protected virtual void Initiate()
	{
	    idle = new basicenemyidle(this);
        patrol = new basicenemypatrol(this);
		chase = new basicenemychase(this);
		attack = new basicenemyattack(this);
		cooldown = new basicenemycooldown(this);
		hurt = new basicenemyhurt(this);
	}

    void Start() 
	{
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		hitbox = GetComponentInChildren<DamageComponent>();

		Initiate();
    
		hitbox.SetDamage(data.damage);
		hitbox.gameObject.SetActive(false);
		prevVectorDir = Vector2.zero;
		health = data.maxHealth;
        state = null;

		healthUI.localPosition = new Vector3(0, 1.2f, 0);
		healthUI.localScale = new Vector3(1, 0.1f, 0);

        changestate(patrol);
    }

    void Update() //check new condition
    {
		if (health <= 0)
			Destroy(this.gameObject);

		status.text = state.ToString();
        state?.logic();
    }

    void FixedUpdate() {  //check phisical condition
        state?.fixedlogic();
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<DamageComponent>(out DamageComponent d))
			gotHurt(d.damage);
	}

	private void gotHurt(int d)
	{
		if (state == hurt)
			return;

		health -= d;
		UpdateHealthUI();
		changestate(hurt);
	}

	private void UpdateHealthUI()
	{
		float newsize = (float)health/(float)data.maxHealth;
		float newoffset = 0.5f*(1-newsize);

		healthUI.localScale = new Vector3(newsize, healthUI.localScale.y, 0);
		healthUI.localPosition = new Vector3(-newoffset, healthUI.localPosition.y, 0);
	}
	
    public void changestate(MainState newstate) {
        if (state == newstate)
            return;
        
        state?.exit();
		prevState = state;
        state = newstate;
        state.enter();
    }

	public void playAnimation(string newstate, string dir)
	{
		if (currentState + currentDirection != newstate + dir)
		{
			currentState = newstate;
			currentDirection = dir;
			anim.Play(currentState + currentDirection);
		}
	}

	public Vector2 getRandomDirection()
	{
		int xx = Random.Range(-1, 2);
		int yy = Random.Range(-1, 2);
		Vector2 dir = Vector2.zero;

		if (xx == prevVectorDir.x && prevVectorDir.x != 0)
			xx*=-1;

		if (yy == prevVectorDir.y && prevVectorDir.y != 0)
			yy*=-1;

		if (xx == yy || (xx != 0 && yy != 0))
		{	
			int r = Random.Range(0, 101);
			if (r > 50)
				xx = 0;
			else 
				yy = 0;
		}

		dir = new Vector2(xx, yy);
		prevVectorDir = dir;

		return dir;
	}

	public Vector2 getDirectionToPlayer()
	{
		Vector3 pos = GameObject.FindObjectOfType<PlayerController>().transform.position;
		Vector2 dirTo = pos - transform.position;

		return dirTo;
	}

	public Vector2 getDirectionFromVelocity()
	{
		return new Vector2(
				(int)Mathf.Clamp(rb.velocity.x, -1, 1), 
				(int)Mathf.Clamp(rb.velocity.y, -1, 1));
	}

	public Vector2 getDirectionFromAnimation()
	{
		if (currentDirection == null)
			return Vector2.down;

		Vector2 rdir = Vector2.down;
		switch(currentDirection)
		{
			case "down" : { rdir = Vector2.down; } break;
			case "up" : { rdir = Vector2.up; } break;
			case "right" : { rdir = Vector2.right; } break;
			case "left" : { rdir = Vector2.left; } break;
		}

		return rdir;
	}

	public string getDirectionTag(Vector2 i)
	{
		if (i.x == 0 && i.y >  0) { return "up"; } else 
		if (i.x == 0 && i.y <  0) { return "down"; } else 
		if (i.x <  0 && i.y == 0) { return "left"; } else 
		if (i.x >  0 && i.y == 0) { return "right"; } else { return "down"; } 
	}

	public virtual void onAttackEvent()
	{
		SetHitbox(getDirectionFromAnimation());
	}

	public void OnEndAttack()
	{
		hitbox.gameObject.SetActive(false);
		changestate(cooldown);
	}

	public IEnumerator hitflash()
	{
		int i = 0;
		while(i < 5)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSecondsRealtime(0.04f);
			GetComponent<SpriteRenderer>().color = Color.white;
			yield return new WaitForSecondsRealtime(0.04f);
			i++;
		}
	}

	public void SetHitbox(Vector2 dir)
	{
		hitbox.gameObject.SetActive(true);
		hitbox.transform.position = transform.position + (Vector3)dir;
	}
}
