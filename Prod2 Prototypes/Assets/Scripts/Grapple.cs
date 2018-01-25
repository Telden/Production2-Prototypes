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
	private bool mShouldPull;


	//Lerping  variables
	public float mTime = 0;
	public  float interval;
	void Start ()
	{
		player = GameObject.Find("Player");
		mShouldPull = false;
	}

	void Update ()
	{
		checkInput();
		DoPointer();

	}

	private void DoPointer()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, dashTo))
		{
			pointer.transform.position = hit.point;

			if (Input.GetKeyDown(KeyCode.Space) && !mShouldPull) 
			{
				player.GetComponent<Rigidbody>().velocity = (hit.point - transform.position).normalized * pointerSpeed;
				currentPoint = hit.point;
			}
			else if (Input.GetKey(KeyCode.Space) && mShouldPull) 
			{
				//Cache the gameobject hit
				GameObject hitObject = hit.transform.gameObject;

				//Lerp  the hit object towards the player
				if(hitObject.transform.position != player.transform.position)
				{
					Vector3 tmp = Vector3.Lerp(hitObject.transform.position, player.transform.position, mTime);
					hitObject.transform.position = tmp;
					mTime += interval * Time.deltaTime;
				}
			}
			else
				mTime = 0;
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

	private void checkInput()
	{
		if(Input.GetKeyUp(KeyCode.E))
		{
			if(mShouldPull)
			{
				print("Pushing");
				mShouldPull = false;
				mTime = 0;
			}

			else
			{
				print("Pulling");
				mShouldPull  = true;
			}
		}
	}
}
