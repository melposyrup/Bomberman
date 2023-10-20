using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemOnHandMoveWithPlayer", menuName = "ItemLogic/OnHandLogic/ItemOnHandMoveWithPlayer")]

public class ItemOnHandMoveWithPlayer : ItemOnHandSOBase
{
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
			//if(bomb.IsThrow) add force to bomb
			//else
			//go with player
			/*
			Vector3 pos = 
			itembase.transform
			*/

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
