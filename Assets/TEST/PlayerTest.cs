using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour, ITriggerCheckable
{
	const int PLAYER_NUM = 1;

	[SerializeField] private float speed = 2.0f;

	public GameObject bombPrefab;

	[SerializeField]private bool _isOnHold = false;


	private Bomb BombOnFoot { get; set; } = null;
	public void SetBombOnFoot(Bomb bomb)
	{
		BombOnFoot = bomb;
		Debug.Log("BombOnFoot is set to " + BombOnFoot);
	}

	[SerializeField] private bool isDead = false;
	public void SetDeadStatus(bool isDead)
	{
		this.isDead = isDead;
	}


	#region ITriggerCheckable implementation
	public bool IsAggroed { get; set; } = false;

	public void SetAggroStatus(bool isAggroed)
	{
		IsAggroed = isAggroed;
	}
	#endregion


	/**
 * @fn
 * ここに関数の説明を書く
 * @brief 要約説明
 * @param (引数名) 引数の説明
 * @param (引数名) 引数の説明
 * @return 戻り値の説明
 * @sa 参照すべき関数を書けばリンクが貼れる
 * @detail 詳細な説明
 */
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

		//kick bomb
		if (Input.GetKeyDown(KeyCode.F))
		{
			//if bomb is on foot and bomb is idle, change state to bomb.OnKickState
			if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemIdleState))

				BombOnFoot.SetKickStatus(true);
				BombOnFoot.SetKickedBy(this.transform);
				SetBombOnFoot(null);
				_isOnHold= false;

		}

		//create bomb on hand
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (!_isOnHold)
			{
				GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
				//bomb.layer = LayerMask.NameToLayer("InitialBomb");
				bomb.GetComponent<Bomb>().SetOnHoldStatus(true);
				bomb.GetComponent<Bomb>().SetHoldedBy(this.transform);
				SetBombOnFoot(bomb.GetComponent<Bomb>());
				_isOnHold=true;
			}
		}

		//throw bomb
		if (Input.GetKeyDown(KeyCode.T))
		{
			if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemOnHandState))
			{
				BombOnFoot.SetThrowStatus(true);
				BombOnFoot.SetThrownBy(this.transform);
				SetBombOnFoot(null);
				_isOnHold= false;
			}
		}


		//if is hit by bomb in KickState, get IsStunned to true


		// if Player is dead
		if (isDead)
		{

			//TODO: play death sound

			//TODO: call death event
			EventManager.Instance.OnPlayerDeath.Invoke(PLAYER_NUM);

			//TODO: play death animation, when animation is over call destory
			OnDestroy();
		}


			
	}

	public void OnDestroy()
	{
		Destroy(gameObject);
	}

}

