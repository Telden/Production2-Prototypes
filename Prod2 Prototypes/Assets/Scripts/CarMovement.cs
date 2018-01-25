using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
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
	public float tmpSpd;
	public float tmpMaxSpd;
	public float tmpRot;
	public float tmpRotLess;
	public float tmpDrag;
	public float jumpForce;

	[Header("Ground")]
	public LayerMask ground;
	public bool grounded;
	public float tmpArtificialGravityForce;

	[Header("Controls")]
	public string axisHor;
	public string axisVer;
	public KeyCode keyRight;
	public KeyCode keyLeft;
	public KeyCode keyJump;

	[Header("Attacking")]
	public GameObject punchHitbox;
	public Transform[] hitboxLocations;
	public float startUp;
	public float duration;
	public bool punching;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		rb.drag = tmpDrag;
		punchHitbox.SetActive(false);
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
		float turnRestriction = rb.velocity.magnitude / tmpMaxSpd;

		// add forward and backward movement using GetAxis
		rb.AddForce(transform.forward * Input.GetAxis(axisVer) * tmpSpd);

		if (punching)
		{
			// rotate the vehicle using GetAxis
			transform.Rotate(0, Input.GetAxis(axisHor) * tmpRotLess * turnRestriction, 0);
		}
		else
		{
			// rotate the vehicle using GetAxis
			transform.Rotate(0, Input.GetAxis(axisHor) * tmpRot * turnRestriction, 0);
		}

		Debug.DrawLine(transform.position + transform.forward * 5, transform.position);
	}

	void tmpTerminalVelocity()
	{
		if (rb.velocity.x > tmpMaxSpd)
		{
			rb.velocity = new Vector3(tmpMaxSpd, rb.velocity.y, rb.velocity.z);
		}
		if (rb.velocity.x < tmpMaxSpd * -1)
		{
			rb.velocity = new Vector3(tmpMaxSpd * -1, rb.velocity.y, rb.velocity.z);
		}

		if (rb.velocity.z > tmpMaxSpd)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, tmpMaxSpd);
		}
		if (rb.velocity.z < tmpMaxSpd * -1)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, tmpMaxSpd * -1);
		}

		if (rb.velocity.y > tmpMaxSpd)
		{
			rb.velocity = new Vector3(rb.velocity.x, tmpMaxSpd, rb.velocity.z);
		}
		if (rb.velocity.y < tmpMaxSpd * -1)
		{
			rb.velocity = new Vector3(rb.velocity.x, tmpMaxSpd * -1, rb.velocity.z);
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
		rb.AddForce(transform.up * -1 * tmpArtificialGravityForce);
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

		if (Input.GetKeyDown (keyJump) && !punching)
		{
			Invoke ("DoPunchJump", startUp); 
		}
	}

	// doing punches
	private void DoPunchRight()
	{
		punchHitbox.transform.position = hitboxLocations[0].position;
		punchHitbox.SetActive(true);
		punching = true;
		Invoke("StopPunchRight", duration);
	}

	private void DoPunchLeft()
	{
		punchHitbox.transform.position = hitboxLocations[1].position;
		punchHitbox.SetActive(true);
		punching = true;
		Invoke("StopPunchLeft", duration);
	}

	private void DoPunchJump()
	{
		punchHitbox.transform.position = hitboxLocations[2].position;
		punchHitbox.SetActive(true);
		punching = true;
		rb.AddForce(transform.up * jumpForce);
		Invoke("StopPunchJump", duration);
	}

	// stopping punches
	private void StopPunchRight()
	{
		punching = false;
		punchHitbox.SetActive(false);
	}

	private void StopPunchLeft()
	{
		punching = false;
		punchHitbox.SetActive(false);
	}

	private void StopPunchJump()
	{
		punching = false;
		punchHitbox.SetActive(false);
	}
}
