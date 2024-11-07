using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	public static HitStop instances;

	private void Awake()
	{
		instances = this;
	}

	public void Init(float time)
	{
		StartCoroutine(Stop(time));
	}

	public IEnumerator Stop(float time)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(time);
		Time.timeScale = 1;
	}
}
