using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: �������त�������ե������`���ܥࡢ�ɥ����ǥӥ���䡩��IdleState�˄Ӥ���I����

/* ������Bomb
 * �त������Red Bomb
 * �ե������`��Fire
 * �ܥࣺBombUP
 * �ɥ���Skull
 * �ǥӥ룺Devil
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

	#endregion

	#region ScriptableObject Variables
	[SerializeField] private ItemIdleSOBase ItemIdleBase;

	public ItemIdleSOBase ItemIdleBaseInstance { get; set; }
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

		StateMachine = new ItemStateMachine();

		IdleState = new ItemIdleState(this, StateMachine);

	}

	protected virtual void Start()
	{
		Rigidbody = GetComponent<Rigidbody>();

		ItemIdleBaseInstance.Initialize(gameObject, this);

		StateMachine.Initialize(IdleState);
	}

	protected virtual void Update()
	{
		StateMachine.CurrentItemState.UpdateState();
	}

	protected virtual void FixedUpdate()
	{
		StateMachine.CurrentItemState.FixedUpdateState();
	}


}
