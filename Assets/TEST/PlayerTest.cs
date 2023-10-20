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
		// Get keyboard input ･ｭｩ`･ﾜｩ`･ﾉﾈ・ｦ､｡ｵﾃ､ｹ､・
		float horizontalInput = Input.GetAxis("Horizontal"); // Horizontal input ﾋｮﾆｽﾈ・ｦ 
		float verticalInput = Input.GetAxis("Vertical"); // Vertical input ｴｹﾖｱﾈ・ｦ

		// Calculate movement direction ﾒﾆ・ｷｽﾏ桐网ｹ､・
		Vector3 move = new Vector3(horizontalInput, 0.0f, verticalInput);

		// Apply movement vector ﾒﾆ・･ﾙ･ｯ･ﾈ･・mﾓﾃ､ｹ､・
		transform.Translate(move * speed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement ﾒﾆ・ｷｽﾏﾋﾏｫ､ﾃ､ﾆﾏｭ､荀ｨ､・
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

		}

		//kick bomb
		if (Input.GetKeyDown(KeyCode.F))
		{

			//if bomb is on foot and bomb is idle, change state to bomb.OnKickState
			if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemIdleState))
			{
				Debug.Log(BombOnFoot.StateMachine.CurrentItemState);
				BombOnFoot.SetKickStatus(true);
				Debug.Log(BombOnFoot.StateMachine.CurrentItemState);
			}

		}

		//throw bomb



		//if is hit by bomb in KickState, get IsStunned to true


	}
}

