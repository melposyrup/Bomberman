using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerControl : MonoBehaviour, ITriggerCheckable
{
	// 爆弾のPrefabを設定
	[SerializeField] GameObject Bomb;

	// プレイヤーの移動入力を検知した際に代入される値
	float InputHorizontal = 0;
	float InputVertical = 0;
	// プレイヤーの移動速度
	float _playerMoveSpeed = 7.0f;

	// プレイヤーの行動
	// 現在ステージに設置している爆弾の数
	public int _bombPlaceNum;
	//public void BombPlaceIncrement 
	// プレイヤーが気絶しているか
	//bool _playerFainting = false;
	// プレイヤーがやられているか
	bool _IsDead = false;

	[Header("BUFF and DEBUFF Effects from 5 Types of Items")]
	// !プレイヤーのバフ、デバフ
	// 爆弾所持数
	[SerializeField] int _bombMaxNum = 1;
	public void IncreaseBombMaxNum() { _bombMaxNum++; }

	// 火力
	[SerializeField] int _firePowerNum = 0;
	public void IncreaseFirePowerNum() { _firePowerNum++; }

	// パワーボムか red bomb
	[SerializeField] private bool _powerBomb = false;
	public void SetPowerBomb(bool powerbomb) { _powerBomb = powerbomb; }

	// デビル状態か
	[SerializeField] private bool _isDevil = false;
	public void SetDevil(bool devil) { _isDevil = devil; }

	// ドクロ状態か
	[SerializeField] private bool _isSkull = false;
	public void SetSkull(bool skull) { _isSkull = skull; }


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


	#region ITriggerCheckable implementation
	public bool IsAggroed { get; set; } = false;

	public void SetAggroStatus(bool isAggroed)
	{
		IsAggroed = isAggroed;
	}
	#endregion

	#region IBombExplodable implementation
	public bool IsExplode { get; set; }
	public void SetExplodeStatus(bool isExplode)
	{ IsExplode = isExplode; }
	public Transform IsPlacedBy { get; set; }
	public void SetPlacedBy(Transform transform)
	{ IsPlacedBy = transform; }
	public float IsExplodeTimer { get; set; }
	public void SetExplodeTimer(float explodeTimer)
	{ IsExplodeTimer = explodeTimer; }
	public bool IsCounting { get; set; }
	public void SetCounting(bool isCounting)
	{ IsCounting = isCounting; }

	#endregion



	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// 移動関連の処理
		MovePlayer();
		// 爆弾関連の処理
		BombMovement();
		// プレイヤーがやられたときの処理(爆風に触れたときに入れる？)
		IsPlayerDead();
	}

	// プレイヤーの移動関連の処理
	void MovePlayer()
	{
		// キー入力から値を取得
		InputHorizontal = Input.GetAxis("Horizontal");
		InputVertical = Input.GetAxis("Vertical");
		// 入力された方向に移動する
		// Calculate movement direction
		Vector3 move = new Vector3(InputHorizontal, 0, InputVertical);

		// Apply movement vector 
		transform.Translate(move * _playerMoveSpeed * Time.deltaTime, Space.World);

		// Change orientation to face the direction of movement
		if (move != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.15f);
		}
	}

	void BombMovement()
	{
		// place bomb
		if (Input.GetButtonDown("Bomb_Place") && _bombMaxNum > _bombPlaceNum)
		{
			// 爆弾のPrefabを生成
			GameObject bomb = Instantiate(Bomb, transform.position, Quaternion.identity);
			bomb.layer = LayerMask.NameToLayer("InitialBomb");
			// 自分が設置しているボムのカウントを増やす
			++_bombPlaceNum;
			// 誰が生成したかの情報を渡す
			bomb.GetComponent<Bomb>().SetPlacedBy(_bombPlaceNum);
		}

		//kick bomb
		if (Input.GetButtonDown("Bomb_Kick"))
		{

			//if bomb is on foot and bomb is idle, change state to bomb.OnKickState
			if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemIdleState))
			{
				BombOnFoot.SetKickedBy(transform);
				BombOnFoot.SetKickStatus(true);
				//Debug.Log(BombOnFoot.StateMachine.CurrentItemState);
			}
		}

		//hold bomb
		if (Input.GetButtonDown("Bomb_PickUp"))
		{
			if (!_isOnHold)
			{
				_isOnHold = true;
				_bombOnHold = Instantiate(Bomb, transform.position, Quaternion.identity);
				Bomb bomb = _bombOnHold.GetComponent<Bomb>();
				//bomb.layer = LayerMask.NameToLayer("InitialBomb");
				bomb.SetOnHoldStatus(true);
				bomb.SetHoldedBy(this.transform);
			}
		}

		//throw bomb
		if (Input.GetButtonDown("Bomb_Throw"))
		{
			if (_isOnHold && _bombOnHold)
			{
				Bomb bomb = _bombOnHold.GetComponent<Bomb>();
				bomb.SetThrowStatus(true);
				bomb.SetThrownBy(this.transform);
				_bombOnHold = null;
				_isOnHold = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// アイテム取得
		// 爆風を受けたら消滅(_playerDead)
		if (other.CompareTag("Explosion"))
		{
			_IsDead = true;
		}
	}

	// プレイヤーがやられたときの処理
	void IsPlayerDead()
	{
		if (_IsDead)
		{
			if (_isOnHold) { Destroy(_bombOnHold); }
			//TODO: instantiate items
			//TODO: play death sound
			//TODO: play death animation
			Destroy(gameObject);
		}
	}
}