using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : Player
{
	[Header("Getting hit")]
	public float knockbackForce;
	public int maxHealth;
	private int health;

	[Header("Lap count")]
	public int currentLap = 1;
	public GameObject previousCheckpoint;
	public Text laptxt;

	private LevelCheckpoints lc;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		lc = GameObject.Find("LevelInfo").GetComponent<LevelCheckpoints>();
		health = maxHealth;
	}

	void Update()
	{
		DoUI();
	}

	void DoUI()
	{
		laptxt.text = "Lap: " + currentLap.ToString();
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

		if (other.CompareTag("Checkpoint"))
		{
			Debug.Log("reached " + other.gameObject.name);

			// for the first collision
			if (previousCheckpoint == null)
			{
				previousCheckpoint = other.gameObject;
			}

			// for every time afterwards
			switch (lc.CheckConnection(previousCheckpoint, other.gameObject))
			{
				case 0:
				{
					// no connection made
					break;
				}
				case 1:
				{
					previousCheckpoint = other.gameObject;
					break;
				}
				case 2:
				{
					previousCheckpoint = other.gameObject;
					currentLap++;
					break;
				}
				default:
				{
					// we shouldn't ever get here
					break;
				}
			}
		}
	}
}