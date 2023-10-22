using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombAggroCheckTEST : MonoBehaviour
{
	private PlayerTest _playerControl;

	private void Awake()
	{
		_playerControl = GetComponentInParent<PlayerTest>();
	}

	//when item get into trigger and it has bomb.cs, set playerTest.IsAggroed to true
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerControl.SetAggroStatus(true);
				_playerControl.SetBombOnFoot(other.GetComponent<Bomb>());
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerControl.SetAggroStatus(false);
				_playerControl.SetBombOnFoot(null);
			}
		}
	}




}
