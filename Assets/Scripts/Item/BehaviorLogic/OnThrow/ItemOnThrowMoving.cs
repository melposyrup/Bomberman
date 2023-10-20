using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///	state transfer logic: OnHold -> OnThrow -> Idle
/// </summary>
[CreateAssetMenu(fileName = "ItemOnThrowMoving", menuName = "ItemLogic/OnThrowLogic/ItemOnThrowMoving")]
public class ItemOnThrowMoving : ItemOnThrowSOBase
{
	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
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
