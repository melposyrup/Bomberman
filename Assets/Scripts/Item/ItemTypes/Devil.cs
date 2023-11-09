using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : ItemBase
{
	public override ItemType Type => ItemType.Devil;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
			if (player != null)
			{
				player.SetDevil(true);

				Destroy(gameObject);
			}
		}
	}
}
