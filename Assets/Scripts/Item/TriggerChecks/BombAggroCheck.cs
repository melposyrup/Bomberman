using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAggroCheck : MonoBehaviour
{
	private ItemBase _itembase;

	private void Awake()
	{
		_itembase = GetComponentInParent<ItemBase>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		// Unity§«•Ï•§•‰©`§Ú π”√§∑§∆≈–∂®§π§Î•≥©`•…
		int playerLayer = LayerMask.NameToLayer("Player");
		int airWallLayer = LayerMask.NameToLayer("AirWall");
		int bombLayer = LayerMask.NameToLayer("Bomb");

		if (collision.gameObject.layer == playerLayer
			|| collision.gameObject.layer == airWallLayer
			|| collision.gameObject.layer == bombLayer)
		{
			_itembase.SetAggroStatus(true);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		int playerLayer = LayerMask.NameToLayer("Player");
		int airWallLayer = LayerMask.NameToLayer("AirWall");
		int bombLayer = LayerMask.NameToLayer("Bomb");

		if (collision.gameObject.layer == playerLayer
			|| collision.gameObject.layer == airWallLayer
			|| collision.gameObject.layer == bombLayer)
		{
			_itembase.SetAggroStatus(false);
		}
	}
}
