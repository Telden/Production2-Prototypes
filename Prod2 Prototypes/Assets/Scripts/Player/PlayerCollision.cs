using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	private Rigidbody rb;
	public float knockbackForce;

	private int health;
	public int maxHealth;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		health = maxHealth;
	}

	int getHealth()
	{
		return health;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Attack"))
		{
			Debug.Log("ouch");
			rb.AddForce((transform.position - other.transform.position).normalized * knockbackForce);
			health--;
		}
	}
}