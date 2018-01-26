using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	CollectibleManager mpCollectibleManager;

	// Use this for initialization
	void Start (){
		mpCollectibleManager = GameObject.Find("GameSystem").GetComponent<CollectibleManager>();
		mpCollectibleManager.registerPickup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pickUpObject()
	{
		mpCollectibleManager.reducePickupsLeft();
		this.gameObject.SetActive(false);
	}
}
