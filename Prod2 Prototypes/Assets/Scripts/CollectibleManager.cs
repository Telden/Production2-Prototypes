using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour {

	//public float mTotalPickups;
	public float mPickupsLeft = 0;
	public  Canvas tmpWinScreen;
	public Text mCollectibleUI;

	// Use this for initialization
	void Start () {
		//mTotalPickups = mPickupsLeft;
		tmpWinScreen.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		mCollectibleUI.text = "Shards left: " + mPickupsLeft.ToString();
		if(mPickupsLeft == 0)
		{
			print("Player wins!");
			tmpWinScreen.enabled = true;

		}
			
	}

	public void reducePickupsLeft()
	{
		mPickupsLeft -= 1;
	}

	public void registerPickup()
	{
		mPickupsLeft++;
	}
}
