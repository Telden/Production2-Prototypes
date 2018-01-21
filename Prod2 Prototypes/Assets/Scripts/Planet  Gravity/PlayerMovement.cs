using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float mMovementSpeed;
	private Rigidbody mRigidBody;

	// Use this for initialization
	void Start () {
		mRigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		getInput();
	}

	void getInput()
	{
		//Forwards
		if(Input.GetAxis("Horizontal") > 0)
		{
			mRigidBody.AddForce(transform.forward * mMovementSpeed);
		}
		//Backwards
		else if(Input.GetAxis("Horizontal") < 0)
		{
			mRigidBody.AddForce(transform.forward * -mMovementSpeed);
		}
		//right
		if(Input.GetAxis("Vertical") > 0)
		{
			mRigidBody.AddForce(transform.right * mMovementSpeed);
		}
		else if (Input.GetAxis("Vertical") < 0)
		{
			mRigidBody.AddForce(transform.right * -mMovementSpeed);
		}
	}
}
