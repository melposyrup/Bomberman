using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnKickState : ItemState
{
	public ItemOnKickState(ItemBase itembase, ItemStateMachine itemStateMachine) : base(itembase, itemStateMachine)
	{
	}

	public override void AnimationTriggerEvent(ItemBase.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);

		if (itembase is Bomb bomb)
			bomb.ItemOnKickBaseInstance.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void EnterState()
	{
		base.EnterState();

		if (itembase is Bomb bomb)
			bomb.ItemOnKickBaseInstance.DoEnterLogic();
	}

	public override void ExitState()
	{
		base.ExitState();

		if (itembase is Bomb bomb)
			bomb.ItemOnKickBaseInstance.DoExitLogic();
	}

	public override void FixedUpdateState()
	{
		base.FixedUpdateState();

		if (itembase is Bomb bomb)
			bomb.ItemOnKickBaseInstance.DoFixedUpdateLogic();
	}

	public override void UpdateState()
	{
		base.UpdateState();

		if (itembase is Bomb bomb)
			bomb.ItemOnKickBaseInstance.DoUpdateLogic();
	}
}
