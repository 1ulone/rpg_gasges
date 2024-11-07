using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	public static Pool instances;

	[SerializeField] private List<content> objectLists;
	private Dictionary<string, Queue<GameObject>> objectDictionary = new Dictionary<string, Queue<GameObject>>();

	private void Awake()
	{
		instances = this;
		foreach(content c in objectLists)
		{
			Queue<GameObject> q = new Queue<GameObject>();
			for (int i = 0; i < c.count; i++)
			{
				GameObject g = Instantiate(c.prefab);
				g.transform.SetParent(this.transform);
				g.name = c.tag.ToLower();
				g.SetActive(false);
				q.Enqueue(g);
			}

			objectDictionary.Add(c.tag.ToLower(), q);
		}
	}

	public GameObject create(string tag, Vector3 position, Vector3 rotation, Transform parent = null)
	{
		if (!objectDictionary.ContainsKey(tag.ToLower()))
			return null;

		GameObject n = objectDictionary[tag.ToLower()].Dequeue();
		n.transform.position = position;
		n.transform.rotation = Quaternion.Euler(rotation);
		if (parent != null)
			n.transform.SetParent(parent);

		n.SetActive(true);
		return n;
	}

	public void destroy(GameObject prefab)
	{
		if (!objectDictionary.ContainsKey(prefab.name.ToLower()))
			return;

		prefab.transform.position = Vector3.zero;
		prefab.transform.rotation = Quaternion.Euler(Vector3.zero);
		prefab.transform.SetParent(this.transform);

		objectDictionary[prefab.name.ToLower()].Enqueue(prefab);
		prefab.SetActive(false);
	}
}

[System.Serializable]
public class content 
{
	public string tag;
	public GameObject prefab;
	public int count;
}
