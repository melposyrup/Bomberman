using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///	state transfer logic: OnHold -> OnThrow -> Idle
/// </summary>
[CreateAssetMenu(fileName = "ItemOnThrowMoving", menuName = "ItemLogic/OnThrowLogic/ItemOnThrowMoving")]
public class ItemOnThrowMoving : ItemOnThrowSOBase
{
	[SerializeField] private float throwForce = 3f; // force magnitude

	private float collisionCooldown = 0.3f;


	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();

		itembase.Rigidbody.isKinematic = false;

		// check if there is a value in itembase.ThrowDirection
		if (itembase.ThrowDirection != Vector3.zero)
		{
			// if there is, use it as the direction of throw force
			itembase.Rigidbody.AddForce(itembase.ThrowDirection * throwForce, ForceMode.Impulse);
		}
		else
		{
			// or do the calculation based on forward vector of the player
			Vector3 direction = (itembase.IsThrownBy.forward + Vector3.up * 4.0f).normalized;
			itembase.Rigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
		}

	}

	public override void DoExitLogic()
	{
		base.DoExitLogic();
		itembase.Rigidbody.isKinematic = true;
	}

	public override void DoFixedUpdateLogic()
	{
		base.DoFixedUpdateLogic();

	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();


		if (collisionCooldown < 0f)
		{
			if (itembase.IsOnLand)//TODO: if item touch land, change state to idle
			{
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
