using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* 爆弾：Bomb
 * 赤い爆弾：Red Bomb
 * ファイアー：Fire
 * ボム：BombUP
 * ドクロ：Skull
 * デビル：Devil
 */

public class ItemBase : MonoBehaviour, IItemMoveable, ITriggerCheckable, IItemThrowable
{
	public enum ItemType
	{
		Fire,
		Skull,
		Devil,
		BombUp,
		Undefined
	}
	public virtual ItemType Type => ItemType.Undefined;


	#region IItemThrowable implementation
	public bool IsThrow { get; set; }
	public void SetThrowStatus(bool isThrow) { IsThrow = isThrow; }

	public Transform IsThrownBy { get; set; }
	public void SetThrownBy(Transform transform) { IsThrownBy = transform; }

	public Vector3 ThrowDirection { get; set; } = Vector3.zero;
	public void SetThrowDirection(Vector3 direction) { }

	public bool IsOnLand { get; set; } = false;
	public void SetOnLand(bool isOnLand) { IsOnLand = isOnLand; }


	#endregion

	#region IItemMoveable implementation
	public Rigidbody Rigidbody { get; set; }

	public void MoveItem(Vector3 velocity)
	{
		Rigidbody.isKinematic = false;
		Rigidbody.velocity = velocity;
	}
	public void MoveItemKinematic(Vector3 velocity)
	{
		// Move the object upward at a speed of 1 unit/second
		Rigidbody.isKinematic = true;
		Vector3 newPosition = Rigidbody.position + velocity * Time.deltaTime;
		Rigidbody.MovePosition(newPosition);
	}

	#endregion

	#region ITriggerCheckable implementation

	[SerializeField] private bool isAggroed;

	public bool IsAggroed
	{
		get { return isAggroed; }
		set { isAggroed = value; }
	}

	public void SetAggroStatus(bool isAggroed) { IsAggroed = isAggroed; }
	#endregion

	#region State Machine Variables

	public ItemStateMachine StateMachine { get; set; }
	public ItemIdleState IdleState { get; set; }
	public ItemOnThrowState OnThrowState { get; set; }


	#endregion

	#region ScriptableObject Variables
	[SerializeField] private ItemIdleSOBase ItemIdleBase;
	[SerializeField] private ItemOnThrowSOBase ItemOnThrowBase;
	public ItemIdleSOBase ItemIdleBaseInstance { get; set; }
	public ItemOnThrowSOBase ItemOnThrowBaseInstance { get; set; }
	#endregion

	#region Aniamtion Triggers
	private void AnimationTriggerEvent(AnimationTriggerType triggerType)
	{
		//Debug.Log("Animation Trigger: " + triggerType);
		StateMachine.CurrentItemState.AnimationTriggerEvent(triggerType);
	}

	public enum AnimationTriggerType
	{
		Idle,
		//Walk,
		//Run,
		//Jump,
		//Attack,
		//Death
	}

	#endregion

	protected virtual void Awake()
	{
		ItemIdleBaseInstance = Instantiate(ItemIdleBase);
		ItemOnThrowBaseInstance = Instantiate(ItemOnThrowBase);

		StateMachine = new ItemStateMachine();

		IdleState = new ItemIdleState(this, StateMachine);
		OnThrowState = new ItemOnThrowState(this, StateMachine);
	}

	protected virtual void Start()
	{
		Rigidbody = GetComponent<Rigidbody>();

		ItemIdleBaseInstance.Initialize(gameObject, this);
		ItemOnThrowBaseInstance.Initialize(gameObject, this);

		StateMachine.Initialize(IdleState);

	}

	protected virtual void Update()
	{
		StateMachine.CurrentItemState.UpdateState();

		//debug
		if (this.transform.position.x < -5)
		{
			Destroy(this.gameObject);
		}
	}

	protected virtual void FixedUpdate()
	{
		StateMachine.CurrentItemState.FixedUpdateState();

		//keep rotation not changed
		this.transform.rotation = Quaternion.identity;
	}

	#region OnLand
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("StageLand"))
		{
			SetOnLand(true);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("StageLand"))
		{
			SetOnLand(false);
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("StageLand"))
		{
			SetOnLand(true);
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("StageLand"))
		{
			SetOnLand(false);
		}
	}
	#endregion
}
