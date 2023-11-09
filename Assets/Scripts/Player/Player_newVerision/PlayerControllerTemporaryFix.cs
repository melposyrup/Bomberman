using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTemporaryFix : MonoBehaviour
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
		if (Input.GetButtonDown("Bomb_Place")) { player.IsBombPressed = true; }
		if(Input.GetButtonUp("Bomb_Place")) { player.IsBombPressed = false; }

		if (Input.GetButtonDown("Bomb_Kick")) { player.IsKickPressed = true; }
		if(Input.GetButtonUp("Bomb_Kick")) { player.IsKickPressed = false; }

		if(Input.GetButtonDown("Bomb_Hold")) { player.IsHoldPressed = true; }
		if(Input.GetButtonUp("Bomb_Hold")) { player.IsHoldPressed = false; }

		if (Input.GetButtonDown("Bomb_Throw")) { player.IsThrowPressed = true; }
		if(Input.GetButtonUp("Bomb_Throw")) { player.IsThrowPressed = false; }

		if(Input.GetButtonDown("Bomb_Expand")) { player.IsExpandPressed = true; }
		if(Input.GetButtonUp("Bomb_Expand")) { player.IsExpandPressed = false; }
	}

	// movement
	void MovePlayer()
	{
		// ÉLÅ[ì¸óÕÇ©ÇÁílÇéÊìæ
		float InputHorizontal = Input.GetAxis("Horizontal");
		float InputVertical = Input.GetAxis("Vertical");
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
