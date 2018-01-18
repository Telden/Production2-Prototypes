using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	/*Private Variables*/
	[SerializeField]
	private float mCurrentAcceleration = 0;
	[SerializeField]
	private float mAccelerationIncrease;
	[SerializeField]
	private float mMaxAccleration;
	[SerializeField]
	private float mMaxVelocity;
	private float mDeceleration = 1;
	[SerializeField]
	private float mRotationVelocity;

	public float tmpSpd;
	public float tmpMaxSpd;
	public float tmpRot;
	public float tmpDrag;



	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody>().drag = tmpDrag;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//checkInput();
		tmpMovement();
		tmpTerminalVelocity();
	}

	void tmpMovement()
	{
		// add forward and backward movement using GetAxis
		GetComponent<Rigidbody>().AddForce(transform.forward * Input.GetAxis("Vertical") * tmpSpd);
		// rotate the vehicle using GetAxis
		transform.Rotate(0, Input.GetAxis("Horizontal") * tmpRot, 0);

		Debug.DrawLine(transform.position + transform.forward * 5, transform.position);
	}

	void tmpTerminalVelocity()
	{
		if (GetComponent<Rigidbody>().velocity.x > tmpMaxSpd)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(tmpMaxSpd, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
		}
		if (GetComponent<Rigidbody>().velocity.x < tmpMaxSpd * -1)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(tmpMaxSpd * -1, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
		}

		if (GetComponent<Rigidbody>().velocity.z > tmpMaxSpd)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, tmpMaxSpd);
		}
		if (GetComponent<Rigidbody>().velocity.z < tmpMaxSpd * -1)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, tmpMaxSpd * -1);
		}
	}

	void checkInput()
	{
		//Forwards
		if(Input.GetKey(KeyCode.UpArrow))
		{
			mCurrentAcceleration += mAccelerationIncrease;
			if(mCurrentAcceleration < mMaxVelocity)
				transform.GetComponent<Rigidbody>().AddForce(transform.forward * mCurrentAcceleration);
			else
			{
				transform.GetComponent<Rigidbody>().AddForce(transform.forward * mMaxVelocity);
				mCurrentAcceleration = mMaxVelocity;
			}
		}

		//Backwards
		if(Input.GetKey(KeyCode.DownArrow))
		{
			mCurrentAcceleration -= mAccelerationIncrease;
			if(mCurrentAcceleration > mMaxVelocity * -1)
				transform.GetComponent<Rigidbody>().AddForce((transform.forward  *-1) * mCurrentAcceleration);
			else
			{
				transform.GetComponent<Rigidbody>().AddForce((transform.forward *-1) * mMaxVelocity);
				mCurrentAcceleration = mMaxVelocity * -1;
			}
		}

		//Right
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(0,mRotationVelocity,0);
		}

		//Left
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(0,-mRotationVelocity,0);
		}
	}
}
