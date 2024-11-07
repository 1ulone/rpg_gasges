using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public static bool onPause;

	[SerializeField] private CanvasGroup panel;

	private void Awake()
	{
		onPause = false;
		panel.alpha = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (onPause)
			{
				Time.timeScale = 1;
				panel.alpha = 0;
				onPause = false;
			} else 
			if (!onPause)
			{
				Time.timeScale = 0;
				panel.alpha = 1;
				onPause = true;
			}
		}
	}
}
