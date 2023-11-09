using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemIdleStationary", menuName = "ItemLogic/IdleLogic/ItemIdleStationary")]
public class ItemIdleStationary : ItemIdleSOBase
{
	Vector3 keepStable; 

	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
		itembase.GetComponent<Rigidbody>().isKinematic = false;
		keepStable= itembase.transform.position;
		if(itembase is Bomb bomb)
		{
			bomb.SetCounting(true);
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

	
		if (itembase is Bomb bomb)
		{
			//OnHoldState
			if (bomb.IsOnHold)
			{
				bomb.SetCounting(false);
				bomb.SetOnHoldStatus(false);//back to false
				bomb.StateMachine.ChangeState(bomb.OnHandState);
				return;
			}

			//OnKickState
			if (bomb.IsKick)
			{
				bomb.SetKickStatus(false);//back to false
				bomb.StateMachine.ChangeState(bomb.OnKickState);
				return;
			}
		}
		
		itembase.transform.position = 
			new Vector3(keepStable.x, itembase.transform.position.y, keepStable.z);

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


