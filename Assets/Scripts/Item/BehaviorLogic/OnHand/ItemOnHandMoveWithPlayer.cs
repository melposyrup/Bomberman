using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemOnHandMoveWithPlayer", menuName = "ItemLogic/OnHandLogic/ItemOnHandMoveWithPlayer")]

public class ItemOnHandMoveWithPlayer : ItemOnHandSOBase
{
	public float HoldDistance = 0.5f;
	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();
		itembase.Rigidbody.isKinematic = false;

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
			//if(bomb.IsThrow) changeState to ThrowByPlayer, return;
			//(add force to bomb in EnterState,if OnLand() is true back to stationary)
			
			//go with player
			Vector3 pos = bomb.IsHoldedBy.position + bomb.IsHoldedBy.forward * HoldDistance;
			itembase.transform.position = pos;
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
