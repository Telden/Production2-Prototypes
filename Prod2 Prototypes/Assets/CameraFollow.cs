using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[Header("Following")]
	public GameObject myDrum;
	public Vector3 followPosition;
	private Vector3 distance;

	void Start ()
	{
		distance = transform.position - myDrum.transform.position;
	}

	void Update ()
	{
		transform.position = new Vector3(myDrum.transform.position.x + distance.x, myDrum.transform.position.y + distance.y, myDrum.transform.position.z + distance.z);
	}
}
