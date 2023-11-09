using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///	state transfer logic: OnHold -> OnThrow -> Idle
/// </summary>
[CreateAssetMenu(fileName = "ItemOnThrowMoving", menuName = "ItemLogic/OnThrowLogic/ItemOnThrowMoving")]
public class ItemOnThrowMoving : ItemOnThrowSOBase
{
	private float throwForce; // force magnitude
	float baseangle = 2f;

	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
		itembase.Rigidbody.isKinematic = false;

		if (itembase is Bomb bomb)
		{
			bomb.SetCounting(true);
			throwForce =
				bomb.Owner.GetComponent<Player>().Force
				* bomb.transform.localScale.magnitude;
		}

		itembase.Rigidbody.isKinematic = false;

		if (itembase.ThrowDirection != Vector3.zero)
		{
			itembase.Rigidbody.AddForce(itembase.ThrowDirection * throwForce, ForceMode.Impulse);
		}
		else
		{
			Vector3 direction =
				(itembase.IsThrownBy.forward.normalized
				+ Vector3.up * (baseangle + itembase.transform.localScale.magnitude)).normalized;

			itembase.Rigidbody.AddForce(direction * throwForce, ForceMode.Impulse);

		}

	}

	public override void DoExitLogic()
	{
		base.DoExitLogic();
	}

	public override void DoFixedUpdateLogic()
	{
		base.DoFixedUpdateLogic();

	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();

		//Debug.Log("itembase.IsOnLand : " + itembase.IsOnLand);
		if (itembase.IsOnLand)
		{
			itembase.IsOnLand = false;
			itembase.StateMachine.ChangeState(itembase.IdleState);
		}

		if (itembase is Bomb bomb)
		{
			if (itembase.Rigidbody.velocity.y < 0)
			{
				bomb.gameObject.layer = LayerMask.NameToLayer("Bomb");
			}
		}
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
