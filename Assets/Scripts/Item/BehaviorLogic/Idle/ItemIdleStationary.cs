using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemIdleStationary", menuName = "ItemLogic/IdleLogic/ItemIdleStationary")]
public class ItemIdleStationary : ItemIdleSOBase
{
	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
		itembase.GetComponent<Rigidbody>().isKinematic = true;
	}

	public override void DoExitLogic()
	{
		base.DoExitLogic();
		itembase.GetComponent<Rigidbody>().isKinematic = false;
	}

	public override void DoFixedUpdateLogic()
	{
		base.DoFixedUpdateLogic();
	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();

	
		if (itembase is Bomb bomb)
		{
			//OnHoldState
			if (bomb.IsOnHold)
			{
				bomb.StateMachine.ChangeState(bomb.OnHandState);
				return;
			}

			//OnKickState
			if (bomb.IsKick)
			{
				bomb.StateMachine.ChangeState(bomb.OnKickState);
				return;
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


