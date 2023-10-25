using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ItemBase, IItemKickable, IItemHoldable, IBombExpandable, IBombExplodable
{


	#region IBombExpandable implementation
	public Vector3 MaxScale { get; set; }
	public float ExpandFactor { get; set; } = 0.3f;
	public float MaxExpandFactor { get; set; } = 2f;

	public void Expand()
	{
		if (transform.localScale.magnitude < MaxScale.magnitude)
			transform.localScale *= ExpandFactor;
	}


	#endregion

	#region IItemKickable implementation
	public bool IsKick { get; set; } = false;
	public void SetKickStatus(bool isKick)
	{ IsKick = isKick; }
	public Transform IsKickedBy { get; set; }
	public void SetKickedBy(Transform transform)
	{ IsKickedBy = transform; }

	#endregion

	#region IItemHoldable implementation
	public bool IsOnHold { get; set; }
	public void SetOnHoldStatus(bool isHolding)
	{ IsOnHold = isHolding; }
	public Transform IsHoldedBy { get; set; }
	public void SetHoldedBy(Transform transform)
	{ IsHoldedBy = transform; }

	#endregion

	#region IBombExplodable implementation
	public bool IsExplode { get; set; }
	public void SetExplodeStatus(bool isExplode)
	{ IsExplode = isExplode; }
	public int IsPlacedBy { get; set; }
	public void SetPlacedBy(int player)
	{ IsPlacedBy = player; }
	public float IsExplodeTimer { get; set; }
	public void SetExplodeTimer(float explodeTimer)
	{ IsExplodeTimer = explodeTimer; }
	public bool IsCounting { get; set; }
	public void SetCounting(bool isCounting)
	{ IsCounting = isCounting; }

	#endregion

	#region State Machine Variables
	public ItemOnKickState OnKickState { get; set; }
	public ItemOnHandState OnHandState { get; set; }
	#endregion

	#region ScriptableObject Variables
	[SerializeField] private ItemOnKickSOBase ItemOnKickBase;
	[SerializeField] private ItemOnHandSOBase ItemOnHandBase;
	public ItemOnKickSOBase ItemOnKickBaseInstance { get; set; }
	public ItemOnHandSOBase ItemOnHandBaseInstance { get; set; }

	#endregion


	[Header("AnimationEffects")]
	public GameObject Explosion;

	protected override void Awake()
	{
		base.Awake();
		ItemOnKickBaseInstance = Instantiate(ItemOnKickBase);
		ItemOnHandBaseInstance = Instantiate(ItemOnHandBase);

		OnKickState = new ItemOnKickState(this, base.StateMachine);
		OnHandState = new ItemOnHandState(this, base.StateMachine);

		// 爆弾のカウントダウン設定
		IsExplodeTimer = 3.0f;
		IsCounting = true;
	}

	protected override void Start()
	{
		base.Start();

		// IBombExpandable
		Vector3 InitialScale = transform.localScale;
		MaxScale = new Vector3(
			InitialScale.x * MaxExpandFactor,
			InitialScale.y * MaxExpandFactor,
			InitialScale.z * MaxExpandFactor);
		// ScriptableObject Variables
		ItemOnKickBaseInstance.Initialize(gameObject, this);
		ItemOnHandBaseInstance.Initialize(gameObject, this);
	}

	protected override void Update()
	{
		base.Update();

		// 爆弾関連
		if (IsCounting)
		{
			IsExplodeTimer -= Time.deltaTime;
		}
		if (IsExplodeTimer < 0)
		{

			IsPlacedBy--;
			Death();
		}

	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		//Debug.Log("bomb state:  " + base.StateMachine.CurrentItemState);
	}


	//when player get out of the trigger,set trigger to false
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			gameObject.layer = LayerMask.NameToLayer("Bomb");
		}
	}

	private void Death()
	{
		//playerObject.func();
		if (Explosion)
		{
			Instantiate(Explosion, transform.position, Quaternion.identity);
		}
		else { Debug.Log("Explosion Prefab undefined"); }

		Destroy(gameObject);
	}
}
