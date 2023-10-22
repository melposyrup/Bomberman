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
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
			|| collision.gameObject.layer == LayerMask.NameToLayer("AirWall")
			|| collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
		{
			_itembase.SetAggroStatus(true);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
			|| collision.gameObject.layer == LayerMask.NameToLayer("AirWall")
			|| collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
		{
			_itembase.SetAggroStatus(false);
		}
	}
}
