using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float camSpeed = 10;
	[SerializeField] private Vector2 min;
	[SerializeField] private Vector2 max;

	private void FixedUpdate()
	{
		Vector3 clampPos = new Vector3(Mathf.Clamp(target.position.x, min.x, max.x), Mathf.Clamp(target.position.y, min.y, max.y));
		transform.position = new Vector3(clampPos.x, clampPos.y, -10);
	}
}
