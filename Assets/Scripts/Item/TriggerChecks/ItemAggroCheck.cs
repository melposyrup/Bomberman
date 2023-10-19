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
		if (collision.gameObject.CompareTag("AirWall")
			|| collision.gameObject.CompareTag("Player")
			|| collision.gameObject.CompareTag("Item"))
		{
			_itembase.SetAggroStatus(true);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.CompareTag("AirWall")
			|| collision.gameObject.CompareTag("Player")
			|| collision.gameObject.CompareTag("Item"))
		{
			_itembase.SetAggroStatus(false);
		}
	}

}

