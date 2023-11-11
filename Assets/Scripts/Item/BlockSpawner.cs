using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	[Header("Setup manually")]
	public GameObject blockPrefab;
	public LayerMask stageLandLayer; // Assign the correct layer in the inspector

	public float xRange = 10.0f;
	public float zRange = 10.0f;
	public float height = 30.0f;

	public void StartSpawn()
	{
		// Start the spawning coroutine
		StartCoroutine(SpawnBlocks());
	}

	private IEnumerator SpawnBlocks()
	{
		// Loop indefinitely
		while (true)
		{
			// Wait for 3 seconds
			yield return new WaitForSeconds(3f);

			//Debug.Log("SpawnBlocks");

			// Generate a random position in integer coordinates
			Vector3 spawnPosition = new Vector3(
				Mathf.Round(Random.Range(-xRange, xRange)),
				Mathf.Round(height * 2) / 2.0f,
				Mathf.Round(Random.Range(-zRange, zRange))
			);

			// Cast a ray downward from the spawn position
			if (Physics.Raycast(spawnPosition, Vector3.down, 2 * height, stageLandLayer))
			{
				// If hit, instantiate the block at the spawn position
				GameObject blockInstance =
					Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
				blockInstance.transform.SetParent(this.transform);

				Debug.Log("SpawnBlocksSuccess");

			}
			// If nothing is hit, the coroutine will simply wait for the next cycle
		}
	}


	public void Active()
	{
		this.enabled = true;
	}
	public void Deactive()
	{
		this.enabled = false;
	}

}
