using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdleState : ItemState
{
	public ItemIdleState(ItemBase itembase, ItemStateMachine itemStateMachine) : base(itembase, itemStateMachine)
	{
	}

	public override void AnimationTriggerEvent(ItemBase.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);

		itembase.ItemIdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void EnterState()
	{
		base.EnterState();

		itembase.ItemIdleBaseInstance.DoEnterLogic();
	}

	public override void ExitState()
	{
		base.ExitState();

		itembase.ItemIdleBaseInstance.DoExitLogic();
	}

	public override void FixedUpdateState()
	{
		base.FixedUpdateState();

		itembase.ItemIdleBaseInstance.DoFixedUpdateLogic();
	}

	public override void UpdateState()
	{
		base.UpdateState();

		itembase.ItemIdleBaseInstance.DoUpdateLogic();
	}
}
