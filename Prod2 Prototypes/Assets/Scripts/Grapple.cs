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


	//Grapple Variables
	private bool mShouldPull;
	private bool mGrappleLatched;
	private bool mGrappleLaunched;
	public float mGrappleSpeed;
	public float mGrappleDistance;
	private float mGrappleTime = 0;
	private Rigidbody mGrappleHookRB;

	//Lerping  variables
	public float mTime = 0;
	public  float interval;

	private Vector3 mGrappleTarget;

	void Start ()
	{
		player = GameObject.Find("Player");
		mGrappleHookRB = this.GetComponent<Rigidbody>();
		mShouldPull = false;
		mGrappleLatched = false;
		mGrappleLaunched = false;
	}

	void Update ()
	{
		checkInput();
		if(mGrappleLaunched)
			moveGrappleHook();
		DoPointer();

	}

	private void DoPointer()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, dashTo))
		{
			pointer.transform.position = hit.point;

			if (Input.GetKeyDown(KeyCode.Space) && !mShouldPull) //&& mGrappleLatched) 
			{
				player.GetComponent<Rigidbody>().velocity = (hit.point - transform.position).normalized * pointerSpeed;
				currentPoint = hit.point;
			}
				else if (Input.GetKey(KeyCode.Space) && mShouldPull) //&& mGrappleLatched) 
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

		if(Input.GetKeyUp(KeyCode.Space) &&!mGrappleLaunched)
		{
			Vector3
			mGrappleTarget = player.transform.forward  * mGrappleSpeed;
			mGrappleLaunched = true;
		}
	}

	private void moveGrappleHook()
	{
		if(gameObject.transform.position != mGrappleTarget)
		{
			Vector3 tmp = Vector3.Lerp(transform.position, player.transform.position, mGrappleTime);
			gameObject.transform.position = tmp;
			mTime += interval * Time.deltaTime;
		}
	}
}
