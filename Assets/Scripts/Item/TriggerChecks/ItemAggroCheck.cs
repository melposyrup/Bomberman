using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemAggroCheck : MonoBehaviour
{
	private ItemBase _itembase;

	private void Awake()
	{
		_itembase = GetComponentInParent<ItemBase>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		int playerLayer = LayerMask.NameToLayer("Player");
		int airWallLayer = LayerMask.NameToLayer("AirWall");
		int itemLayer = LayerMask.NameToLayer("Item");
		int bombLayer = LayerMask.NameToLayer("Bomb");

		if (collision.gameObject.layer == playerLayer
			|| collision.gameObject.layer == airWallLayer
			|| collision.gameObject.layer == itemLayer
			|| collision.gameObject.layer == bombLayer)
		{
			_itembase.SetAggroStatus(true);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		int playerLayer = LayerMask.NameToLayer("Player");
		int airWallLayer = LayerMask.NameToLayer("AirWall");
		int itemLayer = LayerMask.NameToLayer("Item");
		int bombLayer = LayerMask.NameToLayer("Bomb");

		if (collision.gameObject.layer == playerLayer
			|| collision.gameObject.layer == airWallLayer
			|| collision.gameObject.layer == itemLayer
			|| collision.gameObject.layer == bombLayer)
		{
			_itembase.SetAggroStatus(false);
		}
	}

}

