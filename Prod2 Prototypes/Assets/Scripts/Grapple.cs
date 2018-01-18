using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
	private GameObject player;

	public GameObject pointer;
	public float pointerSpeed;
	public LayerMask dashTo;
	public Vector3 currentPoint;

	void Start ()
	{
		player = GameObject.Find("Player");
	}

	void Update ()
	{
		DoPointer();
		DoInput();
	}

	private void DoPointer()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, dashTo))
		{
			pointer.transform.position = hit.point;

			if (Input.GetKeyDown(KeyCode.Space)) 
			{
				player.GetComponent<Rigidbody>().velocity = (hit.point - transform.position).normalized * pointerSpeed;
				currentPoint = hit.point;
			}
		}
		else
		{
			pointer.transform.position = transform.position;
		}

		if ((transform.position - currentPoint).magnitude < 2)
		{
			player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	private void DoInput()
	{
		
	}
}
