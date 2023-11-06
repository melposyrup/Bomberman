using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombAggroCheck2P : MonoBehaviour
{
	private PlayerControl_2P _playerControl2P;

	private void Awake()
	{
		_playerControl2P = GetComponentInParent<PlayerControl_2P>();
	}

	//when item get into trigger and it has bomb.cs, set playerTest.IsAggroed to true
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerControl2P.SetAggroStatus(true);
				_playerControl2P.SetBombOnFoot(other.GetComponent<Bomb>());
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerControl2P.SetAggroStatus(false);
				_playerControl2P.SetBombOnFoot(null);
			}
		}
	}




}
