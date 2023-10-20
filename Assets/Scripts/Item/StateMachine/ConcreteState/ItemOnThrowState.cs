using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnThrowState : ItemState
{
    public ItemOnThrowState(ItemBase itembase, ItemStateMachine itemStateMachine) : base(itembase, itemStateMachine)
    {
    }

    public override void AnimationTriggerEvent(ItemBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        itembase.ItemOnThrowBaseInstance.DoAnimationTriggerEventLogic(triggerType);

    }

    public override void EnterState()
    {
        base.EnterState();

        itembase.ItemOnThrowBaseInstance.DoEnterLogic();

    }

    public override void ExitState()
    {
        base.ExitState();

        itembase.ItemOnThrowBaseInstance.DoExitLogic();

    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        itembase.ItemOnThrowBaseInstance.DoFixedUpdateLogic();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        itembase.ItemOnThrowBaseInstance.DoUpdateLogic();

    }
}
