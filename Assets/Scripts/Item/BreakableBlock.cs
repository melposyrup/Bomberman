using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
	public GameObject[] Items;
	public float[] Probabilities;

	private void Awake()
	{
		this.GetComponent<Rigidbody>().isKinematic = false;

		if (Items.Length != Probabilities.Length)
		{
			Debug.LogError("Items and Probabilities arrays must have the same length");
			return;
		}
	}

	public void SpawnRandomItems()
	{
		// random amount of items
		int itemsToSpawn = Random.Range(1, 4);

		for (int i = 0; i < itemsToSpawn; i++)
		{
			// random item
			GameObject itemToSpawn = ChooseItemByProbability();
			
			if (itemToSpawn != null)
			{
				//random direction
				float angle = Random.Range(0f, Mathf.PI * 2);
				Vector3 spawnPos = new Vector3(
					Mathf.Cos(angle) * 0.2f,
					0f,
					Mathf.Sin(angle) * 0.2f
				) + transform.position;

				Vector3 throwDirection = spawnPos + Vector3.up;
				GameObject item = Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
				item.GetComponent<ItemBase>().SetThrowDirection(throwDirection);
				item.GetComponent<ItemBase>().SetThrowStatus(true);
			}
		}

		Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("StageLand"))
		{
			this.GetComponent<Rigidbody>().isKinematic = true;
		}

		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (!this.GetComponent<Rigidbody>().isKinematic)
			{
				Player player = collision.gameObject.GetComponent<Player>();
				player.playerStateMachine.ChangeState(player.DeadState);
			}
		}
	}

	GameObject ChooseItemByProbability()
	{
		float total = 0;

		foreach (var probability in Probabilities)
		{
			total += probability;
		}

		float randomPoint = Random.value * total;

		for (int i = 0; i < Probabilities.Length; i++)
		{
			if (randomPoint < Probabilities[i])
			{
				return Items[i];
			}
			else
			{
				randomPoint -= Probabilities[i];
			}
		}

		return null;
	}
}
