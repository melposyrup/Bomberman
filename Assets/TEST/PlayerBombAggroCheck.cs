using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombAggroCheck : MonoBehaviour
{
	private PlayerTest _playerTest;

	private void Awake()
	{
		_playerTest = GetComponentInParent<PlayerTest>();
	}

	//when item get into trigger and it has bomb.cs, set playerTest.IsAggroed to true
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerTest.SetAggroStatus(true);
				_playerTest.SetBombOnFoot(other.GetComponent<Bomb>());
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerTest.SetAggroStatus(false);
				_playerTest.SetBombOnFoot(null);
			}
		}
	}




}
