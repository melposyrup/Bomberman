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
	private float _speed = 8f;

	private float collisionCooldown = 0.5f;



	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
		//calculate direction from player to item
		itembase.Rigidbody.isKinematic = false;
		itembase.Rigidbody.useGravity = true;

		if (itembase is Bomb bomb)
		{
			_direction = (itembase.transform.position 
				- bomb.IsKickedBy.transform.position).normalized;
			_direction.y = 0;

			bomb.SetCounting(false);
		}
	}

	public override void DoExitLogic()
	{
		base.DoExitLogic();

		if (itembase is Bomb bomb)
		{
			bomb.SetExplodeTimer(bomb.IsExplodeTimer);
			bomb.SetCounting(true);
		}

	}

	public override void DoFixedUpdateLogic()
	{
		base.DoFixedUpdateLogic();

		//move item until it reaches player or air wall
		//itembase.MoveItem(_direction * _speed);

		Vector3 velocityXZ = _direction * _speed;
		// for some bugs the bome keep floating in air,
		// temporary fix by adding a y velocity
		float velocityY = -3f;

		itembase.Rigidbody.velocity = new Vector3(velocityXZ.x, velocityY, velocityXZ.z);

	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();

		//if reach player or air wall, stop moving
		if (collisionCooldown < 0f)
		{
			if (itembase.IsAggroed)
			{
				itembase.StateMachine.ChangeState(itembase.IdleState);
			}
		}
		else { collisionCooldown -= Time.deltaTime; }


	}
	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
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
