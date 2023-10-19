using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnHandState : ItemState
{
	public ItemOnHandState(ItemBase itembase, ItemStateMachine itemStateMachine) : base(itembase, itemStateMachine)
	{
	}

	public override void AnimationTriggerEvent(ItemBase.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);

		itembase.ItemOnHandBaseInstance.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void EnterState()
	{
		base.EnterState();

		itembase.ItemOnHandBaseInstance.DoEnterLogic();
	}

	public override void ExitState()
	{
		base.ExitState();

		itembase.ItemOnHandBaseInstance.DoExitLogic();
	}

	public override void FixedUpdateState()
	{
		base.FixedUpdateState();

		itembase.ItemOnHandBaseInstance.DoFixedUpdateLogic();
	}

	public override void UpdateState()
	{
		base.UpdateState();

		itembase.ItemOnHandBaseInstance.DoUpdateLogic();
	}
}
