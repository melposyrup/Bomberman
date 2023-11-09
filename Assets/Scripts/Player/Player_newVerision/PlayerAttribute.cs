using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
	private Player player;

	public Dictionary<Enum, int> ItemList;



	private void Awake()
	{
		player = GetComponentInParent<Player>();
		ItemList = new Dictionary<Enum, int>();
	}

	private void OnTriggerEnter(Collider other)
	{


		if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
		{
			if(player.playerStateMachine.CurrentState != player.DeadState)
			{
				player.playerStateMachine.ChangeState(player.DeadState);
			}
		}

	}
}
