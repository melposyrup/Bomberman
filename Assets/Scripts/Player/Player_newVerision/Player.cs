using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Idle -> Hold -> Idle
// Idle/Hold -> Stun
// Stun -> Idle
// -> Dead



public class Player : MonoBehaviour
{
	[Header("Player fields")]
	public float Speed = 4f;
	public int BombCount = 1;
	public int BombCountMax = 3;
	public float Force = 10f;

	#region Speed
	public void SetSpeedHold()
	{
		Speed /= 2f;
	}
	public void SetSpeedNormal()
	{
		Speed *= 2f;
	}
	public void SetSpeedHurry()
	{
		Speed *= 2f;
	}
	#endregion

	#region BombCount
	public void BombCountRecover()
	{
		if (BombCount < BombCountMax) BombCount++;
	}
	public bool BombCountLoadSuccess()
	{
		if (BombCount >= 0)
		{
			BombCount--;
			return true;
		}
		else
		{ return false; }
	}
	#endregion

	[Header("Setup manually")]
	public PlayerAttribute Attribute;
	public Animator Animator;
	public PlayerController Controller;
	public GameEventManager GameEventManager;
	public GameObject BombPrefab;

	#region PlayeController inputs

	private bool isBombPressed = false;
	public bool IsBombPressed
	{
		get { return isBombPressed; }
		set
		{
			isBombPressed = value; //Debug.Log($"Bomb : {isBombPressed}"); 
		}
	}

	private bool isKickPressed = false;
	public bool IsKickPressed
	{
		get { return isKickPressed; }
		set
		{
			isKickPressed = value; //Debug.Log($"Kick : {isKickPressed}");
		}
	}

	private bool isHoldPressed = false;
	public bool IsHoldPressed
	{
		get { return isHoldPressed; }
		set
		{
			isHoldPressed = value; //Debug.Log($"Hold : {isHoldPressed}"); 
		}
	}

	private bool isThrowPressed = false;
	public bool IsThrowPressed
	{
		get { return isThrowPressed; }
		set
		{
			isThrowPressed = value; //Debug.Log($"Throw : {isThrowPressed}");
		}
	}

	private bool isExpandPressed = false;
	public bool IsExpandPressed
	{
		get { return isExpandPressed; }
		set
		{
			isExpandPressed = value; //Debug.Log($"Expand : {isExpandPressed}");
		}
	}

	#endregion

	#region PlayerStateMachine
	public PlayerStateMachine playerStateMachine;

	// player states
	public PlayerIdleState IdleState;
	public PlayerHoldState HoldState;
	public PlayerStunState StunState;
	public PlayerDeadState DeadState;

	#endregion

	private void Awake()
	{
		PlayerInitialize();

		// setup player states
		playerStateMachine = new PlayerStateMachine();
		IdleState = new PlayerIdleState(this);
		HoldState = new PlayerHoldState(this);
		StunState = new PlayerStunState(this);
		DeadState = new PlayerDeadState(this);

		BombCount= BombCountMax;
	}

	private void Start()
	{
		playerStateMachine.Initialize(IdleState);
	}

	private void Update()
	{
		playerStateMachine.CurrentState.UpdateState();
	}







	private void PlayerInitialize()
	{
		// setup color from GameSettings


	}

	public void OnCollisionEnter(Collision collision)
	{
		if (playerStateMachine.CurrentState != DeadState)
		{
			// check layer if is Bomb
			if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb")
				&& collision.gameObject.TryGetComponent(out Bomb bomb))
			{
				if (bomb.IsKick) playerStateMachine.ChangeState(StunState);
			}
			else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				playerStateMachine.ChangeState(StunState);
			}
		}

	}


}
