using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRed : MonoBehaviour
{
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
			if (player != null)
			{
				player.SetPowerBomb(true);

				Destroy(gameObject);
			}
		}
	}
}
