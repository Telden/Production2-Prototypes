using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityKillFloor : MonoBehaviour
{
	public float floorHeight;
	public Vector3 respawnPosition;
	public Vector3 respawnRotation;

	void Update ()
	{
		if (transform.position.y < floorHeight)
		{
			transform.position = respawnPosition;
			transform.rotation = Quaternion.Euler(respawnRotation);
		}
	}
}