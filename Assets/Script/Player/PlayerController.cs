using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 2.5f;
	private Rigidbody2D rb;
	private Animator anim;
	private string currentTag;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		float dirx = Input.GetAxisRaw("Horizontal");
		float diry = Input.GetAxisRaw("Vertical");
		Vector2 dir = new Vector2(dirx, diry);

		rb.velocity = dir * movementSpeed;

		if (rb.velocity != Vector2.zero) 
		{
			PlayAnimation("player_walk");
		} 
		else if (rb.velocity == Vector2.zero)
		{
			PlayAnimation("player_idle");
		}
    }

	public void PlayAnimation(string tag)
	{
		if (currentTag != tag)
		{
			anim.Play(tag);
			currentTag = tag;
		}
	}
}
