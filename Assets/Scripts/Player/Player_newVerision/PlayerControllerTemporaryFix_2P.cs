using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTemporaryFix_2P : MonoBehaviour
{
	public Player player;
	public PlayerController Controller;

	private int playerNumber = 2;

	public bool SwitchToPlugIn = false;
	private void Awake()
	{
		player = GetComponent<Player>();
		Controller = GetComponent<PlayerController>();

		Controller.enabled = !SwitchToPlugIn;
		this.enabled = SwitchToPlugIn;
	}
	private void Start()
	{
		SetupMaterial();

		player.PlayerNumber = playerNumber;

	}

	private void Update()
	{
		if (!(player.playerStateMachine.CurrentState == player.StunState
			|| player.playerStateMachine.CurrentState == player.DeadState))
		{

			MovePlayer();
			InputPlugIn();
		}
	}

	void InputPlugIn()
	{
		if (Input.GetButtonDown("Bomb_Place_2P")) { player.IsBombPressed = true; }
		else if(Input.GetButtonUp("Bomb_Place_2P")) { player.IsBombPressed = false; }

		if (Input.GetButtonDown("Bomb_Kick_2P")) { player.IsKickPressed = true; }
		else if(Input.GetButtonUp("Bomb_Kick_2P")) { player.IsKickPressed = false; }

		if (Input.GetButtonDown("Bomb_Hold_2P")) { player.IsHoldPressed = true; }
		else if(Input.GetButtonUp("Bomb_Hold_2P")) { player.IsHoldPressed = false; }

		if (Input.GetButtonDown("Bomb_Throw_2P")) { player.IsThrowPressed = true; }
		else if(Input.GetButtonUp("Bomb_Throw_2P")) { player.IsThrowPressed = false; }

		if (Input.GetButtonDown("Bomb_Expand_2P")) { player.IsExpandPressed = true; }
		else if(Input.GetButtonUp("Bomb_Expand_2P")) { player.IsExpandPressed = false; }
	}

	// movement
	void MovePlayer()
	{

		float InputHorizontal = Input.GetAxis("Horizontal_2P");
		float InputVertical = Input.GetAxis("Vertical_2P");

		Vector3 move = new Vector3(InputHorizontal, 0, InputVertical);
		move = move.normalized;


		transform.Translate(move * player.Speed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement
		bool isMove = move != Vector3.zero;
		if (isMove)
		{
			float rotateSpeed = 20f;
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(move.normalized),
				rotateSpeed * Time.deltaTime);
		}
		player.Animator.SetBool("isMove", isMove);
	}



	private void SetupMaterial()
	{

		Material material = GameSettings.Instance.GetMaterial(playerNumber);
		this.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
		Debug.Log("Player " + playerNumber + " is " + material.name);

	}





	public void SetControllerOff()
	{
		player.Animator.SetBool("isMove", false) ;
		this.enabled = false;
	}
	public void SetControllerOn()
	{
		this.enabled = true;
	}







}
