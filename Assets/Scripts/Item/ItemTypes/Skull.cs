using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : ItemBase
{
	public override ItemType Type => ItemType.Skull;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
			if (player != null)
			{
				player.SetSkull(true);

				Destroy(gameObject);
			}
		}
	}
}
