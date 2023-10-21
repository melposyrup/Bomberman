using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// state transfer logic: Idle -> Onkick -> Idle
/// </summary>



[CreateAssetMenu(fileName = "ItemOnKickMoving", menuName = "ItemLogic/OnKickLogic/ItemOnKickMoving")]
public class ItemOnKickMoving : ItemOnKickSOBase
{
	private Vector3 _direction;
	private float _speed = 3.0f;
	bool _isMoving = true;

	private float collisionCooldown = 0.3f;

	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
		//calculate direction from player to item
		itembase.GetComponent<Rigidbody>().isKinematic = false;
		if (itembase is Bomb bomb)
		{
			_direction = (itembase.transform.position - bomb.IsKickedBy.transform.position).normalized;
			_direction.y = 0;
			_isMoving = true;
		}
	}

	public override void DoExitLogic()
	{
		base.DoExitLogic();
		itembase.GetComponent<Rigidbody>().isKinematic = true;
	}

	public override void DoFixedUpdateLogic()
	{
		base.DoFixedUpdateLogic();

		//move item until it reaches player or air wall
		if (_isMoving)
		{
			//itembase.MoveItem(_direction * _speed);
			itembase.GetComponent<Rigidbody>().isKinematic = true;
			itembase.MoveItemKinematic(_direction * _speed);
		}
	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();

		//if reach player or air wall, stop moving
		if (collisionCooldown < 0f)
		{
			if (itembase.IsAggroed)
			{
				_isMoving = false;
				_direction = Vector3.zero;
				itembase.StateMachine.ChangeState(itembase.IdleState);
			}
		}
		else { collisionCooldown -= Time.deltaTime; }


	}

	public override void Initialize(GameObject gameObject, ItemBase itembase)
	{
		base.Initialize(gameObject, itembase);
	}

	public override void ResetValues()
	{
		base.ResetValues();
	}


}
