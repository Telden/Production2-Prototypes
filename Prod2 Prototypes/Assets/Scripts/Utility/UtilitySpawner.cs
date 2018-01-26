using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitySpawner : MonoBehaviour
{
	[Header("GET THIS FROM SOMEWHERE ELSE (level manager?) EVENTUALLY")]
	[Header("Spawn this many players")]
	[Range(1, 4)]
	public int numOfPlayers = 1;

	[Header("The player prefab")]
	public GameObject playerPrefab;

	[Header("Player instances (filled on Start())")]
	// keep track of each player for adjusting cameras as more players are introduced
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	[Header("Array of start positions")]
	// perhaps change this so each position it grabbed from some sort of level manager so that the positions can be different for each track
	// should be able to do something similar with assigning controls to each player
	public Vector3[] startPositions;

	void Start ()
	{
		// spawn players
		for (int i = 0; i < numOfPlayers; i++)
		{
			// instantiate and save the instance for initialization
			GameObject tmp = Instantiate(playerPrefab, startPositions[i], Quaternion.identity);

			// depending on which number player we're on, set the color (and eventually the controls)
			switch(i)
			{
				case 0:
				{
					player1 = tmp;
					break;
				}
				case 1:
				{
					player2 = tmp;
					break;
				}
				case 2:
				{
					player3 = tmp;
					break;
				}
				case 3:
				{
					player4 = tmp;
					break;
				}
				default:
				{
					break;
				}
			}

			// set the name of the instance for clarity
			tmp.name = "Player" + (i+1);
			Debug.Log("Spawning Player " + (i+1));
		}

		// setting up camera location
		for (int i = 0; i < numOfPlayers; i++)
		{
			switch(i)
			{
				case 0:
				{
					player1.GetComponent<MeshRenderer>().material.color = Color.red;
					player1.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
					break;
				}
				case 1:
				{
					player2.GetComponent<MeshRenderer>().material.color = Color.blue;
					player1.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
					player2.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
					break;
				}
				case 2:
				{
					player3.GetComponent<MeshRenderer>().material.color = Color.yellow;
					player1.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
					player2.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
					player3.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
					break;
				}
				case 3:
				{
					player4.GetComponent<MeshRenderer>().material.color = Color.green;
					player1.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
					player2.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
					player3.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
					player4.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
					break;
				}
			}
		}
	}
}
