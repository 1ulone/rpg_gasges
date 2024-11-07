using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTrigger : MonoBehaviour
{
	[SerializeField] private int sceneIndexTo = 1;

	public void ChangeScene()
	{
		SceneManager.LoadScene(sceneIndexTo);
	}
}
