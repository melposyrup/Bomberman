using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour, ITriggerCheckable
{

	public float speed = 2.0f;

	public GameObject bombPrefab;



	public Bomb BombOnFoot { get; set; } = null;
	public void SetBombOnFoot(Bomb bomb)
	{
		BombOnFoot = bomb;
		Debug.Log("BombOnFoot is set to " + BombOnFoot);
	}


	#region ITriggerCheckable implementation
	public bool IsAggroed { get; set; } = false;

	public void SetAggroStatus(bool isAggroed)
	{
		IsAggroed = isAggroed;
	}
	#endregion



	void Update()
	{
		// Get keyboard input 
		float horizontalInput = Input.GetAxis("Horizontal"); // Horizontal input 
		float verticalInput = Input.GetAxis("Vertical"); // Vertical input 

		// Calculate movement direction 
		Vector3 move = new Vector3(horizontalInput, 0.0f, verticalInput);

		// Apply movement vector 
		transform.Translate(move * speed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement 
		if (move != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.15f);
		}

		//create bomb on land
		if (Input.GetKeyDown(KeyCode.E))
		{
			GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
			bomb.layer = LayerMask.NameToLayer("InitialBomb");
		}

		//create bomb on hand
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
			bomb.layer = LayerMask.NameToLayer("InitialBomb");
			bomb.GetComponent<Bomb>().SetOnHoldStatus(true);
			bomb.GetComponent<Bomb>().SetHoldedBy(this.transform);
		}

		//kick bomb
		if (Input.GetKeyDown(KeyCode.F))
		{
			//if bomb is on foot and bomb is idle, change state to bomb.OnKickState
			if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemIdleState))
			{
				BombOnFoot.SetKickStatus(true);
				BombOnFoot.SetKickedBy(this.transform);
			}
		}

		//throw bomb
		if (Input.GetKeyDown(KeyCode.T))
        {
			//BombOnFoot.SetThrowStatus(true);

		}


		//if is hit by bomb in KickState, get IsStunned to true


	}
}

