using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrumMovement : MonoBehaviour
{
	private Rigidbody rb;

	[Header("Movement")]
	public float movSpeed;
	public float boostForce;
	public float jumpForce;

	[Header("Being grounded")]
	public bool grounded;
	public LayerMask groundMask;
	public float artificialGravityForce;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		Movement();
		DrumSticks();
		CheckGround();
		ArtificialGravity();

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}
	}

	void Movement()
	{
		rb.AddForce(transform.up * -1 * Input.GetAxis("Horizontal") * movSpeed);
		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			rb.AddForce(Vector3.up * jumpForce);
		}
	}

	void DrumSticks()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			rb.AddForce(transform.up * boostForce);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			rb.AddForce(transform.up * boostForce * -1);
		}
	}

	void CheckGround()
	{
		if (Physics.Raycast(transform.position, Vector3.down, 2.0f, groundMask))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}
	}

	void ArtificialGravity()
	{
		if (!grounded)
		{
			rb.AddForce(Vector3.down * artificialGravityForce);
		}
	}
}
