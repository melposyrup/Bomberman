using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ItemBase;

public class PlayerAttribute : MonoBehaviour
{
	private Player player;


	public SerializableDictionary<ItemBase.ItemType, int> ItemList;



	private void Awake()
	{
		player = GetComponentInParent<Player>();
		ItemList = new SerializableDictionary<ItemBase.ItemType, int>();
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
		{
			if (player.playerStateMachine.CurrentState != player.DeadState)
			{
				player.playerStateMachine.ChangeState(player.DeadState);
			}
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
		{
			ItemBase item = other.GetComponent<ItemBase>();

			if (item != null)
			{
				// update ItemList
				if (ItemList.ContainsKey(item.Type))
				{
					ItemList[item.Type]++;
				}
				else
				{
					ItemList[item.Type] = 1;
				}

				SoundManager.Instance.PlaySE(SESoundData.SE.GetItem);

				switch (item.Type)
				{
					case ItemType.Fire:
						// call following function in playerState
						// bomb.BombRed(player.Attribute.GetKey(ItemType.Fire));
						// when instantiate bomb
						break;
					case ItemType.Skull:


						break;
					case ItemType.Devil:


						break;
					case ItemType.BombUp:
						player.BombCountMax++;

						break;
					case ItemType.Power:
						// call following function in playerState
						// bomb.BombPowerUP(player.Attribute.GetKey(ItemType.BombUp));
						// when instantiate bomb
						break;
					case ItemType.Undefined:
					default:
						// Handle Undefined or unexpected types
						break;
				}

				item.DestroyItem();
			}

		}


	}


	public int GetKey(ItemBase.ItemType type)
	{
		if (ItemList.ContainsKey(type))
		{
			return ItemList[type];
		}
		else
		{
			return 0;
		}
	}
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField] private List<TKey> keys = new List<TKey>();
	[SerializeField] private List<TValue> values = new List<TValue>();


	// be called before save to inspector
	public void OnBeforeSerialize()
	{
		keys.Clear();
		values.Clear();
		foreach (KeyValuePair<TKey, TValue> pair in this)
		{
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}

	// be called after load from inspector
	public void OnAfterDeserialize()
	{
		this.Clear();

		if (keys.Count != values.Count)
			throw new Exception("keys and values lists have different counts!");

		for (int i = 0; i < keys.Count; i++)
			this.Add(keys[i], values[i]);
	}
}