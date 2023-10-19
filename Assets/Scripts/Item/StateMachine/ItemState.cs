using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState
{
	protected ItemBase itembase;
	protected ItemStateMachine itemStateMachine;

	public ItemState(ItemBase itembase, ItemStateMachine itemStateMachine)
	{
		this.itembase = itembase;
		this.itemStateMachine = itemStateMachine;
	}

	public virtual void EnterState() { }
	public virtual void ExitState() { }
	public virtual void UpdateState() { }
	public virtual void FixedUpdateState() { }
	public virtual void AnimationTriggerEvent(ItemBase.AnimationTriggerType triggerType) { }

}
