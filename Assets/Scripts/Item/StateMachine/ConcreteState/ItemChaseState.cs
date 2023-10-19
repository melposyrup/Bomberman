using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChaseState : ItemState
{
	public ItemChaseState(ItemBase itembase, ItemStateMachine itemStateMachine) : base(itembase, itemStateMachine)
	{
	}

	public override void AnimationTriggerEvent(ItemBase.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);

		//itembase.ItemChaseBaseInstance.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void EnterState()
	{
		base.EnterState();

		//itembase.ItemChaseBaseInstance.DoEnterLogic();
	}

	public override void ExitState()
	{
		base.ExitState();

		//itembase.ItemChaseBaseInstance.DoExitLogic();
	}

	public override void FixedUpdateState()
	{
		base.FixedUpdateState();

		//itembase.ItemChaseBaseInstance.DoFixedUpdateLogic();
	}

	public override void UpdateState()
	{
		base.UpdateState();

		//itembase.ItemChaseBaseInstance.DoUpdateLogic();
	}
}
