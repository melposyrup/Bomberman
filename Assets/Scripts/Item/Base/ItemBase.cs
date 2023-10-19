using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ±¬Ž¡¢³à¤¤±¬Ž¡¢¥Õ¥¡¥¤¥¢©`¡¢¥Ü¥à¡¢¥É¥¯¥í¡¢¥Ç¥Ó¥ë¤òÆä¡©¤ËIdleState¤Ë„Ó¤­¤ò„IÀí¤¹¤ë

/* ±¬Ž£ºBomb
 * ³à¤¤±¬Ž£ºRed Bomb
 * ¥Õ¥¡¥¤¥¢©`£ºFire
 * ¥Ü¥à£ºBombUP
 * ¥É¥¯¥í£ºSkull
 * ¥Ç¥Ó¥ë£ºDevil
 */

public class ItemBase : MonoBehaviour, IItemMoveable, ITriggerCheckable
{


	#region IItemMoveable implementation
	public Rigidbody Rigidbody { get; set; }

	public void MoveItem(Vector3 velocity)
	{
		Rigidbody.velocity = velocity;
	}
	#endregion

	#region ITriggerCheckable implementation

	[SerializeField] private bool isAggroed;

	public bool IsAggroed
	{
		get { return isAggroed; }
		set { isAggroed = value; }
	}


	public void SetAggroStatus(bool isAggroed)
	{
		IsAggroed = isAggroed;
	}


	#endregion

	#region State Machine Variables

	public ItemStateMachine StateMachine { get; set; }
	public ItemIdleState IdleState { get; set; }
	public ItemOnHandState OnHandState { get; set; }

	#endregion

	#region ScriptableObject Variables
	[SerializeField] private ItemIdleSOBase ItemIdleBase;
	[SerializeField] private ItemOnHandSOBase ItemOnHandBase;

	public ItemIdleSOBase ItemIdleBaseInstance { get; set; }
	public ItemOnHandSOBase ItemOnHandBaseInstance { get; set; }


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

	private void Awake()
	{
		ItemIdleBaseInstance = Instantiate(ItemIdleBase);
		ItemOnHandBaseInstance = Instantiate(ItemOnHandBase);

		StateMachine = new ItemStateMachine();

		IdleState = new ItemIdleState(this, StateMachine);
		OnHandState = new ItemOnHandState(this, StateMachine);

	}

	private void Start()
	{
		Rigidbody = GetComponent<Rigidbody>();

		ItemIdleBaseInstance.Initialize(gameObject, this);

		StateMachine.Initialize(IdleState);
	}

	private void Update()
	{
		StateMachine.CurrentItemState.UpdateState();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentItemState.FixedUpdateState();
	}


}
