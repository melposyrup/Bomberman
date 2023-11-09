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


	// orientation

























}
