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
		// Get keyboard input ���`�ܩ`��������ȡ�ä���
		float horizontalInput = Input.GetAxis("Horizontal"); // Horizontal input ˮƽ���� 
		float verticalInput = Input.GetAxis("Vertical"); // Vertical input ��ֱ����

		// Calculate movement direction �Ƅӷ����Ӌ�㤹��
		Vector3 move = new Vector3(horizontalInput, 0.0f, verticalInput);

		// Apply movement vector �Ƅӥ٥��ȥ���m�ä���
		transform.Translate(move * speed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement �Ƅӷ�����򤫤ä��򤭤�䤨��
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

