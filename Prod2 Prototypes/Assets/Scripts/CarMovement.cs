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



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkInput();
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
