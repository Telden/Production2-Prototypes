using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour {

	// Use this for initialization
	public Rigidbody mCurrentSurface; //The current surface that the player is on
	private Rigidbody mRigidbody;
	public float mGravity;
	/* Resource Used https://mikeloscocco.wordpress.com/2015/10/13/mario-galaxy-physics-in-unity/ */

	void Start () {
		mRigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate()
	{
		updateOrientation();
	}

	void updateOrientation()
	{
		Vector3 normal = Vector3.zero;
		normal = getSurfaceNormal();
		//Set the rotation of the object to what normal of the ground underneath it is
		this.transform.localRotation = Quaternion.FromToRotation(this.transform.up, normal) * this.transform.rotation;
		//Add a gravitational force in the opposite direction of the normal on the ground's surface
		mRigidbody.AddForce(normal * -mGravity);

	}

	Vector3 getSurfaceNormal()
	{
		//Get the distance between the player and the gravity object
		float distance = Vector3.Distance(this.transform.position, mCurrentSurface.transform.position);
		Vector3 normal = Vector3.zero;

		//Send a raycast of with the length of the distance between the two objects
		RaycastHit rayHit;
		if(Physics.Raycast(this.transform.position, -this.transform.position, out rayHit, distance))
		{
			normal = rayHit.normal;
		}

		//Return the normal to where the raycast hit the surface of the object
		return normal;
	}

}
