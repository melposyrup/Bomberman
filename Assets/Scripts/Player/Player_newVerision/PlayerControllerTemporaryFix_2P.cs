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

	public int _bombPlaceNum;

	// !プレイヤーのバフ、デバフ
	// 爆弾所持数
	[SerializeField] int _bombMaxNum = 2;
	public void IncreaseBombMaxNum() { _bombMaxNum++; }

	// 火力
	[SerializeField] int _firePowerNum = 0;
	public void IncreaseFirePowerNum() { _firePowerNum++; }

	// パワーボムか red bomb
	[SerializeField] private bool _powerBomb = false;
	public void SetPowerBomb(bool powerbomb) { _powerBomb = powerbomb; }

	[Header("Player Action")]
	//hold
	[SerializeField] private bool _isOnHold = false;
	private GameObject _bombOnHold = null;

	//kick
	private Bomb BombOnFoot { get; set; } = null;
	public void SetBombOnFoot(Bomb bomb)
	{
		BombOnFoot = bomb;
		Debug.Log("BombOnFoot is set to " + BombOnFoot);
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
		// キー入力から値を取得
		float InputHorizontal = Input.GetAxis("Horizontal_2P");
		float InputVertical = Input.GetAxis("Vertical_2P");
		// 入力された方向に移動する
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
