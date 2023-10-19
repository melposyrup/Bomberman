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
		if (collision.gameObject.CompareTag("Player")
			|| collision.gameObject.CompareTag("AirWall"))
		{
			_itembase.SetAggroStatus(true);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.CompareTag("Player")
			|| collision.gameObject.CompareTag("AirWall"))
		{
			_itembase.SetAggroStatus(false);
		}
	}
}
