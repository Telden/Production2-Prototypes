using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
	public float floorHeight;
	public Vector3 respawnPosition;

	void Update ()
	{
		if (transform.position.y < floorHeight)
		{
			transform.position = respawnPosition;
		}
	}
}
