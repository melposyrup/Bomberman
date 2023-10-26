using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : ItemBase
{
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
			if (player != null)
			{
				player.IncreaseFirePowerNum();

				Destroy(gameObject);
			}
		}
	}
}
