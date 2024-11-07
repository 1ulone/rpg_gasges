using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnend : MonoBehaviour
{
	private void destroy()
		=> Pool.instances.destroy(this.gameObject);
}
