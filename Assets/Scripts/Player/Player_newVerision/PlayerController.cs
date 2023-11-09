using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public int PlayerNumber = 0;

	public PlayerInput playerInput;
	private Player player;

	private Vector3 moveInput = Vector3.zero;



	private void Awake()
	{
		player = GetComponent<Player>();
		playerInput = GetComponent<PlayerInput>();

		playerInput.enabled = true;
	}

	private void Start()
	{
		// setup playerNumer and InputSystem
		PlayerNumber = GameSettings.Instance.GetAvailablePlayerNumber();
		if (PlayerNumber < 1) { Debug.LogError("PlayerNumber is not set"); }
		//else { SetDefaultControlSchemeByIndex(PlayerNumber - 1); }

	}
	private void FixedUpdate()
	{
		if (!(player.playerStateMachine.CurrentState == player.StunState
			|| player.playerStateMachine.CurrentState == player.DeadState))
		{
			Movement(moveInput);
			ReviseOrientation();
		}
	}

	#region input system
	public void OnExpand(InputAction.CallbackContext context)
	{
		if (context.started) { player.IsExpandPressed = true; }
		else if (context.canceled) { player.IsExpandPressed = false; }
	}
	public void OnThrow(InputAction.CallbackContext context)
	{
		if (context.started) { player.IsThrowPressed = true; }
		else if (context.canceled) { player.IsThrowPressed = false; }
	}

	public void OnHold(InputAction.CallbackContext context)
	{
		if (context.started) { player.IsHoldPressed = true; }
		else if (context.canceled) { player.IsHoldPressed = false; }
	}
	public void OnKick(InputAction.CallbackContext context)
	{
		if (context.started) { player.IsKickPressed = true; }
		else if (context.canceled) { player.IsKickPressed = false; }
	}
	public void OnBomb(InputAction.CallbackContext context)
	{
		if (context.started) { player.IsBombPressed = true; }
		else if (context.canceled) { player.IsBombPressed = false; }
	}
	#endregion

	private void SetDefaultControlSchemeByIndex(int index)
	{
		var controlSchemes = playerInput.actions.controlSchemes;

		if (index >= 0 && index < controlSchemes.Count)
		{
			var controlScheme = controlSchemes[index].name;

			playerInput.defaultControlScheme = controlScheme;
			playerInput.defaultActionMap = controlScheme;

			playerInput.SwitchCurrentControlScheme(controlScheme);
		}
		else
		{
			Debug.LogWarning("Control scheme index out of range: " + index);
		}
	}

	private void Movement(Vector2 direction)
	{
		transform.Translate(moveInput * player.Speed * Time.deltaTime, Space.World);
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Vector2 input = context.ReadValue<Vector2>();
			moveInput = new Vector3(input.x, 0, input.y);
			moveInput = moveInput.normalized;
		}
		else if (context.canceled)
		{
			moveInput = Vector3.zero;
		}

		bool move = moveInput.magnitude > 0;
		player.Animator.SetBool("isMove", move);
	}

	private void ReviseOrientation()
	{
		if (moveInput != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(moveInput);

			float rotationSpeed = 15.0f;

			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				targetRotation,
				rotationSpeed * Time.deltaTime);
		}
		// freeze x and z rotation in inspector rigidbody
	}


}
