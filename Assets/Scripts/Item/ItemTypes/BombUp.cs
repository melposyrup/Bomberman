using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUp : ItemBase
{
	public override ItemType Type => ItemType.BombUp;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
			if (player != null)
			{
				player.IncreaseBombMaxNum();

				Destroy(gameObject);
			}
		}
	}
}