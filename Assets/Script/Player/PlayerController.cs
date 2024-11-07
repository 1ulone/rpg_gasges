using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 2.5f;
	[SerializeField] private int maxHealth = 10;
	[SerializeField] private Transform hitbox;
	[SerializeField] private Image healthUI;

	private Rigidbody2D rb;
	private Animator anim;
	private string currentTag;
	private string prevTag;
	private bool onAttack;
	private bool onHurt;
	private int health;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		hitbox.GetComponent<DamageComponent>().SetDamage(1);
		hitbox.gameObject.SetActive(false);

		health = maxHealth;
		onAttack = false;
    }

    void Update()
    {
		float dirx = Input.GetAxisRaw("Horizontal");
		float diry = Input.GetAxisRaw("Vertical");
		Vector2 dir = new Vector2(dirx, diry);

		if (onHurt)
			return;

		Movement(dir);
		
		if (Input.GetKeyDown(KeyCode.J) && !onAttack)
			Attack();

		AnimationController();
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<DamageComponent>(out DamageComponent d))
		{
			UpdateHealth(d.damage);
			if (other.TryGetComponent<Projectile>(out Projectile p))
				p.onDeath(this.transform.position);
		}

		if (other.TryGetComponent<RoomTrigger>(out RoomTrigger rt))
			rt.ChangeScene();
	}

	private void UpdateHealth(int d)
	{
		if (onHurt)
			return;

		rb.velocity = Vector2.zero;
		HitStop.instances.Init(0.25f);

		onHurt = true;
		health -= d; 

		Invoke("ResetHurt", 1f);
		StartCoroutine(hitflash());

		healthUI.fillAmount = (float)health/(float)maxHealth;
	}

	private IEnumerator hitflash()
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

	private void ResetHurt()
		=> onHurt = false;

	private void Movement(Vector2 dir)
	{
		if (dir.x != 0 && dir.y != 0 || onAttack)
			return;

		rb.velocity = dir * movementSpeed;
	}

	private void Attack()
	{
		onAttack = true;
		hitbox.gameObject.SetActive(true);

		if (currentTag.Contains("down")) { hitbox.position = transform.position + (Vector3.down*1.5f); } else 
		if (currentTag.Contains("up")) { hitbox.position = transform.position + (Vector3.up*1.5f); } else 
		if (currentTag.Contains("left")) { hitbox.position = transform.position + (Vector3.left*1.5f); } else 
		if (currentTag.Contains("right")) { hitbox.position = transform.position + (Vector3.right*1.5f); }  

		rb.velocity = Vector2.zero;
	}

	private void EndAttack() 
	{ 
		onAttack = false; 
		hitbox.gameObject.SetActive(false);
	}

	private void AnimationController()
	{
		if (onAttack)
		{
			if (currentTag.Contains("down")) { PlayAnimation("player_attack_down"); } else 
			if (currentTag.Contains("up")) { PlayAnimation("player_attack_up"); } else 
			if (currentTag.Contains("right"))  { PlayAnimation("player_attack_right"); } else 
			if (currentTag.Contains("left"))  { PlayAnimation("player_attack_left"); }  
		} else 
		if (rb.velocity != Vector2.zero) 
		{
			if (rb.velocity.y < 0 && rb.velocity.x == 0) { PlayAnimation("player_walk_down"); } else 
			if (rb.velocity.y > 0 && rb.velocity.x == 0) { PlayAnimation("player_walk_up"); } else 
			if (rb.velocity.y == 0 && rb.velocity.x > 0) { PlayAnimation("player_walk_right"); } else 
			if (rb.velocity.y == 0 && rb.velocity.x < 0) { PlayAnimation("player_walk_left"); }  
		} 
		else if (rb.velocity == Vector2.zero)
		{
			if (currentTag != null)
			{
				if (currentTag.Contains("down")) { PlayAnimation("player_idle_down"); } else 
				if (currentTag.Contains("up")) { PlayAnimation("player_idle_up"); } else 
				if (currentTag.Contains("right"))  { PlayAnimation("player_idle_right"); } else 
				if (currentTag.Contains("left"))  { PlayAnimation("player_idle_left"); }  
			}
			else { PlayAnimation("player_idle_down"); }
		}
	}

	private bool CheckAttackString(string a, string b)
	{
		if (a == null || b == null)
			return true;
		return !(a.Contains("attack") && b.Contains("attack"));
	}

	public void PlayAnimation(string tag)
	{
		if (currentTag != tag && CheckAttackString(currentTag, tag))
		{
			anim.Play(tag);
			currentTag = tag;
		}
	}
}
