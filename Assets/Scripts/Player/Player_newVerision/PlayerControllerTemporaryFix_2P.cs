using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTemporaryFix_2P : MonoBehaviour
{
	public Player player;
	public PlayerController Controller;

	public bool SwitchToPlugIn = false;
	private void Awake()
	{
		player = GetComponent<Player>();
		Controller = GetComponent<PlayerController>();

		Controller.enabled = !SwitchToPlugIn;
		this.enabled = SwitchToPlugIn;
	}

	private void Update()
	{
		InputPlugIn();
		MovePlayer();
	}

	void InputPlugIn()
	{
		if (Input.GetButtonDown("Bomb_Place_2P")) { player.IsBombPressed = true; }
		if(Input.GetButtonUp("Bomb_Place_2P")) { player.IsBombPressed = false; }

		if (Input.GetButtonDown("Bomb_Kick_2P")) { player.IsKickPressed = true; }
		if(Input.GetButtonUp("Bomb_Kick_2P")) { player.IsKickPressed = false; }

		if(Input.GetButtonDown("Bomb_Hold_2P")) { player.IsHoldPressed = true; }
		if(Input.GetButtonUp("Bomb_Hold_2P")) { player.IsHoldPressed = false; }

		if (Input.GetButtonDown("Bomb_Throw_2P")) { player.IsThrowPressed = true; }
		if(Input.GetButtonUp("Bomb_Throw_2P")) { player.IsThrowPressed = false; }

		if(Input.GetButtonDown("Bomb_Expand_2P")) { player.IsExpandPressed = true; }
		if(Input.GetButtonUp("Bomb_Expand_2P")) { player.IsExpandPressed = false; }
	}

	// movement
	void MovePlayer()
	{
		// ÉLÅ[ì¸óÕÇ©ÇÁílÇéÊìæ
		float InputHorizontal = Input.GetAxis("Horizontal_2P");
		float InputVertical = Input.GetAxis("Vertical_2P");
		// ì¸óÕÇ≥ÇÍÇΩï˚å¸Ç…à⁄ìÆÇ∑ÇÈ
		// Calculate movement direction
		Vector3 move = new Vector3(InputHorizontal, 0, -InputVertical);
		move = move.normalized;

		// Apply movement vector 
		transform.Translate(move * player.Speed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement
		if (move != Vector3.zero)
		{
			float rotateSpeed = 15f;
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(move.normalized),
				rotateSpeed * Time.deltaTime);
		}
	}

	// orientation
























}
