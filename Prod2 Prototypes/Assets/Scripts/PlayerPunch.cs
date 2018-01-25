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

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		CheckInput();
	}

	void CheckInput()
	{
		if (Input.GetKeyDown(KeyCode.E) && !punching)
		{
			Invoke("DoPunchRight", startUp);
		}

		if (Input.GetKeyDown(KeyCode.Q) && !punching)
		{
			Invoke("DoPunchLeft", startUp);
		}

		if (Input.GetKeyDown (KeyCode.Space) && !punching)
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
		punchHitbox.transform.position = hitboxLocations[2].position;
		punchHitbox.SetActive(true);
		punching = true;
		rb.AddForce(transform.up * jumpForce);
		Invoke("StopPunch", duration);
	}

	// stopping punches
	private void StopPunch()
	{
		punching = false;
		punchHitbox.SetActive(false);
	}
}