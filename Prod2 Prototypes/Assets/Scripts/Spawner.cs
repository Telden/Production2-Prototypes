using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Header("GET THIS INFO FROM SOMEWHERE ELSE EVENTUALLY")]
	[Header("Spawn this many players")]
	[Range(1, 4)]
	public int numOfPlayers = 1;

	[Header("The player prefab")]
	public GameObject playerPrefab;

	[Header("Array of start positions")]
	// perhaps change this so each position it grabbed from some sort of level manager so that the positions can be different for each track
	// should be able to do something similar with assigning controls to each player
	public Vector3[] startPositions;

	void Start ()
	{
		for (int i = 0; i < numOfPlayers; i++)
		{
			// instantiate and save the instance for initialization
			GameObject tmp = Instantiate(playerPrefab, startPositions[i], Quaternion.identity);

			// depending on which number player we're on, set the color (and eventually the controls)
			switch(i)
			{
				case 0:
				{
					tmp.GetComponent<MeshRenderer>().material.color = Color.red;
					break;
				}
				case 1:
				{
					tmp.GetComponent<MeshRenderer>().material.color = Color.blue;
					break;
				}
				case 2:
				{
					tmp.GetComponent<MeshRenderer>().material.color = Color.yellow;
					break;
				}
				case 3:
				{
					tmp.GetComponent<MeshRenderer>().material.color = Color.green;
					break;
				}
				default:
				{
					tmp.GetComponent<MeshRenderer>().material.color = Color.white;
					break;
				}
			}

			// set the name of the instance for clarity
			tmp.name = "Player" + (i+1);
			Debug.Log("Spawning Player " + (i+1));
		}
	}
}
