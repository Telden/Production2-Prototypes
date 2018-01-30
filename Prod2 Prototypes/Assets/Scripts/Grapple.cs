using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grapple : MonoBehaviour
{
	private GameObject player;

	public GameObject pointer;
	public float pointerSpeed;
	public LayerMask dashTo;
	public Vector3 currentPoint;
	public GameObject mReturnPoint;
	public GameObject mTargetPoint;

	//Grapple Variables
	public bool mShouldPull;
	public bool mGrappleLatched;
	public bool mGrappleLaunched;
	public bool mGrappleReturn;
	public float mGrappleSpeed;
	public float mGrappleDistance;
	public float mGrappleTime = 0;
	private Rigidbody mGrappleHookRB;
	private bool mMovePlayer;
	//Lerping  variables
	public float mTime = 0;
	public  float interval;
	public GameObject hitObject;
	private Vector3 mGrappleTarget;
	public Text mTargeter;
	Vector3 mDistanceVector;
	//rendering components of the grapplehook
	SphereCollider mHitBox;
	MeshRenderer mMeshRender;
	float mPullInterval = 0.1f;
	//ParticleSystem  mParts;
	void Start ()
	{
		player = GameObject.Find("Player");
		mGrappleHookRB = this.GetComponent<Rigidbody>();
		mHitBox = this.GetComponent<SphereCollider>();
		mHitBox.enabled = false;
		mMeshRender = this.GetComponent<MeshRenderer>();
		mMeshRender.enabled = false;
		//mParts = this.GetComponent<ParticleSystem>();
		//mParts.Stop();
		mShouldPull = false;
		mGrappleLatched = false;
		mGrappleLaunched = false;
		mGrappleReturn = false;
	}

	void Update ()
	{
		updateUI();
		if(!mGrappleLatched && !mGrappleLaunched && !mGrappleReturn)
			updatePosition();
		checkInput();
		if(mGrappleLaunched || mGrappleReturn)
			moveGrappleHook();
		if(mMovePlayer)
			movePlayer();

	}

	private void updateUI()
	{

		mDistanceVector = Vector3.Normalize(player.transform.forward) * mGrappleDistance;
		RaycastHit hit;
		if(Physics.Raycast(player.transform.position, mDistanceVector, out hit, dashTo))
		{
			pointer.transform.position = hit.point;
			mTargeter.color = new Color(255, 0, 0);
		}
		else
		{
			pointer.transform.position = player.transform.position;
			mTargeter.color = new Color(255, 255, 0);
		}
	}

	private void checkInput()
	{

		if(Input.GetKeyUp(KeyCode.E))
		{
			if(mShouldPull)
			{
				print("Pushing");
				mShouldPull = false;
				mTime = 0;
			}

			else
			{
				print("Pulling");
				mShouldPull  = true;
			}
		}

		if(Input.GetKeyUp(KeyCode.R) && mGrappleLatched)
		{
			mGrappleReturn = true;
			mGrappleLatched  = false;
		}

		if(Input.GetKeyUp(KeyCode.Space) &&!mGrappleLaunched && !mGrappleLatched)
		{
			this.GetComponent<Rigidbody>().velocity = (mTargetPoint.transform.position - this.transform.position).normalized * mGrappleSpeed;
			mHitBox.enabled = true;
			mMeshRender.enabled = true;
			mGrappleLaunched = true;
		}

		else if(Input.GetKey(KeyCode.Space) && mGrappleLatched)
		{
			doGrappleHook();
		}

		if(!Input.GetKey(KeyCode.Space)  && mShouldPull)
			mTime = 0;
	}

	private void updatePosition()
	{
		this.transform.position = mReturnPoint.transform.position;
	}
	private void moveGrappleHook()
	{
		if(mGrappleLaunched && !mGrappleReturn && !mGrappleLatched)
		{
			//Check the distance between player and the hook
			float distance = Vector3.Distance(this.transform.position, player.transform.position);
			if(distance > mGrappleDistance)
			{
				mGrappleReturn = true;
				this.GetComponent<Rigidbody>().velocity =  Vector3.zero;
				mGrappleTime  = 0;
			}
		}
		else if(this.transform.position != mReturnPoint.transform.position && mGrappleReturn && !mGrappleLatched)
		{
			Vector3 tmp = Vector3.Lerp(this.transform.position, mReturnPoint.transform.position, mGrappleTime);
			this.transform.position = tmp;
			mGrappleTime +=  interval * Time.deltaTime;

			//Check the distance between player and the hook
			float distance = Vector3.Distance(this.transform.position, mReturnPoint.transform.position );
			if(distance < 0.5)
			{
				this.transform.position = mReturnPoint.transform.position;
				mGrappleReturn = false;
				mGrappleLaunched = false;
				mGrappleTime =  0;
				resetGrappleHook();
			}
		}
	}

	private void doGrappleHook()
	{
		if(!mShouldPull)
		{
			//player.GetComponent<Rigidbody>().velocity = ( this.transform.position - player.transform.position).normalized * pointerSpeed;
			mMovePlayer =  true;
			mHitBox.enabled = false;
		}
		else
		{
			//Lerp  the hit object towards the player
			float distance = Vector3.Distance(hitObject.transform.position, player.transform.position);
			if(hitObject.transform.position != player.transform.position && distance > 15)
			{
				Vector3 tmp = Vector3.Lerp(hitObject.transform.position, player.transform.position, mTime);
				hitObject.transform.position = tmp;
				mTime += mPullInterval * Time.deltaTime;
			}
		}
	}

	private void movePlayer()
	{
		if(player.transform.position  != this.transform.position)
		{
			Vector3 tmp = Vector3.Lerp(player.transform.position, this.transform.position, mTime);
			player.transform.position = tmp;
			mTime += interval * Time.deltaTime;
			float distance = Vector3.Distance(player.transform.position, this.transform.position);
			if(distance <= 1)
			{
				mTime = 0;
				player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				mMovePlayer = false;
				mGrappleLatched = false;
				resetGrappleHook();

			}
				
		}
	}

	private void resetGrappleHook()
	{
		mHitBox.enabled = false;
		mMeshRender.enabled = false;
		this.transform.position = mReturnPoint.transform.position;
		//resetParticleSystem();
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name != "Player")
		{
			//mParts.Play();
			this.GetComponent<Rigidbody>().velocity =  Vector3.zero;
			hitObject = col.gameObject;
			this.transform.parent = hitObject.transform;
			mHitBox.enabled = false;
			mGrappleLatched = true;
			mGrappleLaunched = false;
			//Invoke("resetParticleSystem", 6f);
		}
			

	}

	/*void resetParticleSystem()
	{
		mParts.Stop();
	}*/

}
