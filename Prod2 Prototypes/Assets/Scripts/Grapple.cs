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
	public GameObject mReturnPoint;
	public GameObject mTargetPoint;

	//Grapple Variables
	public bool mShouldPull;
	public bool mGrappleLatched;
	public bool mGrappleLaunched;
	public bool mGrappleReturn;
	public float mGrappleSpeed;
	public float mGrappleDistance;
	public float mGrappleTime = 0;
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
		mGrappleReturn = false;
	}

	void Update ()
	{
		checkInput();
		if(mGrappleLaunched)
			moveGrappleHook();
		//DoPointer();

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
			this.GetComponent<Rigidbody>().velocity = (mTargetPoint.transform.position - this.transform.position).normalized * mGrappleSpeed;
			mGrappleLaunched = true;
		}
	}


	private void moveGrappleHook()
	{
		if(mGrappleLaunched && !mGrappleReturn)
		{
			//Check the distance between player and the hook
			float distance = Vector3.Distance(this.transform.position, player.transform.position);
			if(distance > mGrappleDistance)
			{
				mGrappleReturn = true;
				this.GetComponent<Rigidbody>().velocity =  Vector3.zero;
				mGrappleTime  = 0;
			}
		}
		else if(this.transform.position != mReturnPoint.transform.position && mGrappleReturn)
		{
			Vector3 tmp = Vector3.Lerp(this.transform.position, mReturnPoint.transform.position, mGrappleTime);
			this.transform.position = tmp;
			mGrappleTime +=  interval * Time.deltaTime;

			//Check the distance between player and the hook
			float distance = Vector3.Distance(this.transform.position, mReturnPoint.transform.position );
			if(distance < 0.5)
			{
				this.transform.position = mReturnPoint.transform.position;
				mGrappleReturn = false;
				mGrappleLaunched = false;
				mGrappleTime =  0;
			}
		}
	}

}
