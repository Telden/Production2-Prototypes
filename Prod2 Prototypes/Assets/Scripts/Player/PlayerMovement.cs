using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
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
	public float spd;
	public float maxSpd;
	public float rot;
	public float rotLess;
	public float drag;

	[Header("Ground")]
	public LayerMask ground;
	public bool grounded;
	public float artificialGravityForce;

	[Header("Controls")]
	public string axisHor;
	public string axisVer;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		rb.drag = drag;
	}

	void Update ()
	{
		Movement();
		TerminalVelocity();
		CheckGround();
		ApplyArtificialGravity();
	}

	void Movement()
	{
		float turnRestriction = rb.velocity.magnitude / maxSpd;

		// add forward and backward movement using GetAxis
		rb.AddForce(transform.forward * Input.GetAxis(axisVer) * spd);

		float tmpHorAxis;
		/*if (Vector3.Angle(transform.forward, rb.velocity.normalized) > 90)
		{
			Debug.Log("mag is negaitive");
			tmpHorAxis = Input.GetAxis(axisHor) * -1;
		}*/

		//else
		tmpHorAxis = Input.GetAxis(axisHor);

		if (GetComponent<PlayerPunch>().punching)
		{
			// rotate the vehicle using GetAxis
			transform.Rotate(0, tmpHorAxis * rotLess * turnRestriction, 0);
		}
		else
		{
			// rotate the vehicle using GetAxis
			transform.Rotate(0, tmpHorAxis * rot * turnRestriction, 0);
		}

		Debug.DrawLine(transform.position + transform.forward * 5, transform.position);
	}

	void TerminalVelocity()
	{
		// x tvs
		if (rb.velocity.x > maxSpd)
		{
			rb.velocity = new Vector3(maxSpd, rb.velocity.y, rb.velocity.z);
		}
		if (rb.velocity.x < maxSpd * -1)
		{
			rb.velocity = new Vector3(maxSpd * -1, rb.velocity.y, rb.velocity.z);
		}

		// z tvs
		if (rb.velocity.z > maxSpd)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpd);
		}
		if (rb.velocity.z < maxSpd * -1)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpd * -1);
		}

		// y tvs (but only for falling)
		/*if (rb.velocity.y > maxSpd)
		{
			rb.velocity = new Vector3(rb.velocity.x, maxSpd, rb.velocity.z);
		}
		if (rb.velocity.y < maxSpd * -1)
		{
			rb.velocity = new Vector3(rb.velocity.x, maxSpd * -1, rb.velocity.z);
		}*/
	}

	void CheckGround()
	{
		// check if we're grounded
		if (Physics.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z), ground))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}
	}

	void ApplyArtificialGravity()
	{
		// if we're not grounded, apply artificial gravity to make us fall faster
		// this allows us to fall faster without increasing our Rigidbody's gravity scale
		// (which can slow down movement on the ground)
		if (!grounded)
		{
			rb.AddForce(transform.up * -1 * artificialGravityForce);
		}
	}
}
