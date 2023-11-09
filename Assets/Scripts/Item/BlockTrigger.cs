using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
		{
			GetComponentInParent<BreakableBlock>().SpawnRandomItems();
		}
	}




}
