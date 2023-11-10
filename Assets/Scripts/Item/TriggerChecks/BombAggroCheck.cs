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
		// Unity§«•ÅE§•‰©`§Ú π”√§∑§∆≈–∂®§π§ÅE≥©`•…
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
			|| collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
		{
			_itembase.SetAggroStatus(true);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
		{
			_itembase.SetAggroStatus(false);
		}

		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			_itembase.SetAggroStatus(false);

			if (_itembase is Bomb bomb)
			{
				if (bomb.StateMachine.CurrentItemState is not ItemOnHandState
					|| bomb.StateMachine.CurrentItemState is not ItemOnThrowState)
				{
					_itembase.gameObject.layer = LayerMask.NameToLayer("Bomb");
				}

			}


		}

	}
}
