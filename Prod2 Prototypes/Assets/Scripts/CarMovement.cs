using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	/*Private Variables*/
	/*[SerializeField]
	private float mCurrentAcceleration = 0;
	[SerializeField]
	private float mAccelerationIncrease;
	[SerializeField]
	private float mMaxAccleration;
	[SerializeField]
	private float mMaxVelocity;
	private float mDeceleration = 1;
	[SerializeField]
	private float mRotationVelocity;*/

	[Header("Movement")]
	public float tmpSpd;
	public float tmpMaxSpd;
	public float tmpRot;
	public float tmpRotLess;
	public float tmpDrag;

	[Header("Ground")]
	public LayerMask ground;
	public bool grounded;
	public float tmpArtificialGravityForce;

	[Header("Controls")]
	public string axisHor;
	public string axisVer;
	public KeyCode keyRight;
	public KeyCode keyLeft;

	[Header("Attacking")]
	public GameObject punchRight;
	public GameObject punchLeft;
	public float startUp;
	public float duration;
	public bool punching;

	void Start ()
	{
		GetComponent<Rigidbody>().drag = tmpDrag;

		punchRight.SetActive(false);
		punchLeft.SetActive(false);
	}

	void Update ()
	{
		//checkInput();
		tmpMovement();
		tmpTerminalVelocity();
		tmpPunch();
		tmpCheckGround();

		if (!grounded)
		{
			tmpApplyArtificialGravity();
		}
	}

	void tmpMovement()
	{
		// add forward and backward movement using GetAxis
		GetComponent<Rigidbody>().AddForce(transform.forward * Input.GetAxis(axisVer) * tmpSpd);

		if (punching)
		{
			// rotate the vehicle using GetAxis
			transform.Rotate(0, Input.GetAxis(axisHor) * tmpRotLess, 0);
		}
		else
		{
			// rotate the vehicle using GetAxis
			transform.Rotate(0, Input.GetAxis(axisHor) * tmpRot, 0);
		}

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

	void tmpCheckGround()
	{
		if (Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z), ground))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}
	}

	void tmpApplyArtificialGravity()
	{
		GetComponent<Rigidbody>().AddForce(transform.up * -1 * tmpArtificialGravityForce);
	}

	void tmpPunch()
	{
		if (Input.GetKeyDown(keyRight) && !punching)
		{
			Invoke("DoPunchRight", startUp);
		}

		if (Input.GetKeyDown(keyLeft) && !punching)
		{
			Invoke("DoPunchLeft", startUp);
		}
	}

	// doing punches
		private void DoPunchRight()
		{
			punchRight.SetActive(true);
			punching = true;
			Invoke("StopPunchRight", duration);
		}
		private void DoPunchLeft()
		{
			punchLeft.SetActive(true);
			punching = true;
			Invoke("StopPunchLeft", duration);
		}

	// stopping punches
		private void StopPunchRight()
		{
			punching = false;
			punchRight.SetActive(false);
		}
		private void StopPunchLeft()
		{
			punching = false;
			punchLeft.SetActive(false);
		}

	/*void checkInput()
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
	}*/
}
