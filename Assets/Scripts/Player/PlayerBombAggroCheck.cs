using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombAggroCheck : MonoBehaviour
{
	private PlayerControl _playerControl;

	private void Awake()
	{
		_playerControl = GetComponentInParent<PlayerControl>();
	}

	//when item get into trigger and it has bomb.cs, set playerTest.IsAggroed to true
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
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
		if (other.CompareTag("Item"))
		{
			if (other.GetComponent<Bomb>() != null)
			{
				_playerControl.SetAggroStatus(false);
				_playerControl.SetBombOnFoot(null);
			}
		}
	}


}
