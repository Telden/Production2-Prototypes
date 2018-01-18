using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyGenerate : MonoBehaviour
{
	public int num;
	public int maxX, maxY, maxZ;
	public int minSize, maxSize;
	public GameObject asteroid;

	void Start ()
	{
		Gen();
	}

	void Update ()
	{
		
	}

	void Gen()
	{
		int seedX, seedY, seedZ;
		float scale;
		GameObject tmp;

		for (int i = 0; i < num; i++)
		{
			seedX = Random.Range(-maxX, maxX);
			seedY = Random.Range(-maxY, maxY);
			seedZ = Random.Range(-maxZ, maxZ);

			scale = Random.Range(minSize, maxSize);

			tmp = Instantiate(asteroid, new Vector3(seedX, seedY, seedZ), transform.rotation);
			tmp.transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}
