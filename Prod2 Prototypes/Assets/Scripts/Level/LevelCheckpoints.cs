using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckpoints : MonoBehaviour
{
	public GameObject[] checkpoints;

	// returns 0 for no connection, 1 for connection, 2 for lap
	public int CheckConnection(GameObject previous, GameObject checking)
	{
		if (previous == checking)
		{
			return 0;
		}
		for (int i = 0; i < checkpoints.Length; i++)
		{
			if (checkpoints[i] == checking)
			{
				// the previous was the last in the list and the one we're checking is the first...
				if (checkpoints[checkpoints.Length - 1] == previous && checkpoints[0] == checking)
				{
					return 2;
				}
				// if the previous node comes before the one we're checking
				else if (i != 0 && checkpoints[i - 1] == previous)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
			else
			{
				// do nothing
			}
		}
		// if for some reason we couldn't find that checkpoint
		return 0;
	}
}
