using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : Player
{
	[Header("Attacking")]
	public GameObject punchHitbox;
	public Transform[] hitboxLocations;
	public float startUp;
	public float duration;
	public float jumpForce;
	public bool punching;

	private string plKey = "PunchLeft", prKey = "PunchRight", jKey = "Jump";

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		if (gameObject.name == "Player2")
		{
			plKey = plKey + "2";
			prKey = prKey + "2";
			jKey = jKey + "2";
		}
	}

	void Update()
	{
		CheckInput();
	}

	void CheckInput()
	{
		if (Input.GetButtonDown(prKey) && !punching)
		{
			Invoke("DoPunchRight", startUp);
		}

		if (Input.GetButtonDown(plKey) && !punching)
		{
			Invoke("DoPunchLeft", startUp);
		}

		if (Input.GetButtonDown (jKey) && !punching)
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
		Invoke("StopPunch", duration);
	}

	private void DoPunchLeft()
	{
		punchHitbox.transform.position = hitboxLocations[1].position;
		punchHitbox.SetActive(true);
		punching = true;
		Invoke("StopPunch", duration);
	}

	private void DoPunchJump()
	{
		// only actually jump if there's something to jump off of
		if (GetComponent<PlayerMovement>().grounded == true)
		{
			rb.AddForce(transform.up * jumpForce);
		}

		punchHitbox.transform.position = hitboxLocations[2].position;
		punchHitbox.SetActive(true);
		punching = true;
		Invoke("StopPunch", duration);
	}

	// stopping punches
	private void StopPunch()
	{
		punching = false;
		punchHitbox.SetActive(false);
	}
}