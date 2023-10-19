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
		// Get keyboard input キーボード入力を取得する
		float horizontalInput = Input.GetAxis("Horizontal"); // Horizontal input 水平入力 
		float verticalInput = Input.GetAxis("Vertical"); // Vertical input 垂直入力

		// Calculate movement direction 移動方向を計算する
		Vector3 move = new Vector3(horizontalInput, 0.0f, verticalInput);

		// Apply movement vector 移動ベクトルを適用する
		transform.Translate(move * speed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement 移動方向に向かって向きを変える
		if (move != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.15f);
		}

		//create bomb on land
		if (Input.GetKeyDown(KeyCode.E))
		{
			Instantiate(bombPrefab, transform.position, Quaternion.identity);
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

