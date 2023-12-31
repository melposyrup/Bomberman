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

		if (itembase is Bomb bomb)
			bomb.ItemOnHandBaseInstance.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void EnterState()
	{
		base.EnterState();

		if (itembase is Bomb bomb)
			bomb.ItemOnHandBaseInstance.DoEnterLogic();
	}

	public override void ExitState()
	{
		base.ExitState();

		if (itembase is Bomb bomb)
			bomb.ItemOnHandBaseInstance.DoExitLogic();
	}

	public override void FixedUpdateState()
	{
		base.FixedUpdateState();

		if (itembase is Bomb bomb)
			bomb.ItemOnHandBaseInstance.DoFixedUpdateLogic();
	}

	public override void UpdateState()
	{
		base.UpdateState();

		if (itembase is Bomb bomb)
			bomb.ItemOnHandBaseInstance.DoUpdateLogic();
	}
}
