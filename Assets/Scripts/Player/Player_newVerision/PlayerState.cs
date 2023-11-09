using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerState
{
	protected Player player;

	public PlayerState(Player player)
	{
		this.player = player;
	}
	public virtual void EnterState() { }
	public virtual void UpdateState() { }
	public virtual void ExitState() { }
}

public class PlayerIdleState : PlayerState
{
	float interactDistance = 1f;

	public PlayerIdleState(Player player) : base(player) { }

	public override void EnterState()
	{
		Debug.LogWarning("Idle State");
	}
	public override void UpdateState()
	{
		// hold
		if (player.IsHoldPressed)
		{
			if (player.BombCountLoadSuccess())
			{
				player.playerStateMachine.ChangeState(player.HoldState);
			}
			player.IsHoldPressed = false;
		}

		// bomb
		if (player.IsBombPressed)
		{
			if (player.BombCountLoadSuccess())
			{
				float bombHeight =
					player.BombPrefab.GetComponent<SphereCollider>().radius;
				Vector3 spawnPosition =
					player.transform.position + new Vector3(0f, bombHeight, 0f);
				GameObject bomb = GameObject.Instantiate(
					player.BombPrefab, spawnPosition, Quaternion.identity);
				bomb.layer = LayerMask.NameToLayer("InitialBomb");
				bomb.GetComponent<Bomb>().SetPlacedBy(player.transform);
				bomb.GetComponent<Bomb>().SetCounting(true);
			}
			player.IsBombPressed = false;
		}

		// kick
		if (player.IsKickPressed)
		{
			if (Physics.Raycast(
				player.transform.position,
				player.transform.forward,
				out RaycastHit raycastHit,
				interactDistance))
			{
				if (raycastHit.transform.TryGetComponent(out Bomb bomb))
				{
					bomb.SetKickedBy(player.transform);
					bomb.SetKickStatus(true);
					player.Animator.SetTrigger("Kick");
				}
			}
			player.IsKickPressed = false;
		}
	}
	public override void ExitState() { }

}

public class PlayerHoldState : PlayerState
{
	private Bomb bomb = null;
	float expandCooldown = 0.1f;
	float countdown = 0f;
	public PlayerHoldState(Player player) : base(player) { }

	public override void EnterState()
	{
		player.Animator.SetBool("isHold", true);

		// create bomb, set to throwState
		GameObject _bomb = GameObject.Instantiate(
			player.BombPrefab,
			player.transform.position,
			Quaternion.identity);
		bomb = _bomb.GetComponent<Bomb>();
		bomb.SetOnHoldStatus(true);
		bomb.SetHoldedBy(player.transform);

		// decrese player speed
		player.SetSpeedHold();

		Debug.LogWarning("Hold State");

	}
	public override void UpdateState()
	{
		// expand with cooldown
		if (countdown <= 0f)
		{
			if (player.IsExpandPressed)
			{
				bomb.Expand();
				countdown = expandCooldown;
			}
		}
		else { countdown -= Time.deltaTime; }

		// throw
		if (player.IsThrowPressed)
		{
			player.IsThrowPressed = false;
			player.playerStateMachine.ChangeState(player.IdleState);
		}

	}
	public override void ExitState()
	{
		bomb.SetThrowStatus(true);
		bomb.SetThrownBy(player.transform);
		bomb.SetCounting(true);
		player.Animator.SetBool("isHold", false);
		bomb = null;
		// player speed back to normal
		player.SetSpeedNormal();
	}
}

public class PlayerStunState : PlayerState
{
	private float countdown = 2f;
	public PlayerStunState(Player player) : base(player) { }

	public override void EnterState()
	{
		player.Animator.SetBool("isStun", true);

		Debug.LogWarning("Stun State");
	}
	public override void UpdateState()
	{
		if (countdown < 0f) player.playerStateMachine.ChangeState(player.IdleState);
		else countdown -= Time.deltaTime;
	}
	public override void ExitState()
	{
		countdown = 2f;
		player.Animator.SetBool("isStun", false);
	}

}

public class PlayerDeadState : PlayerState
{
	private float countdown = 2f;
	public PlayerDeadState(Player player) : base(player) { }

	public override void EnterState()
	{
		player.Animator.SetTrigger("Dead");

		Debug.LogWarning("Dead State");
	}
	public override void UpdateState()
	{
		if (countdown < 0f)
		{
			player.GameEventManager.OnPlayerDeath.Invoke(player.Controller.PlayerNumber);
			player.gameObject.SetActive(false);
		}
		else { countdown -= Time.deltaTime; }

	}
	public override void ExitState()
	{ countdown = 2f; }

}