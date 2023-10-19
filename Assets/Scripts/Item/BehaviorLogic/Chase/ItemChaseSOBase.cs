using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChaseSOBase : ScriptableObject
{
	protected ItemBase itembase;
	protected Transform transform;
	protected GameObject gameObject;

	protected Transform playerTransform;

	public virtual void Initialize(GameObject gameObject, ItemBase itembase)
	{
		this.gameObject = gameObject;
		this.transform = gameObject.transform;
		this.itembase = itembase;

		this.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public virtual void DoEnterLogic() { }

	public virtual void DoExitLogic() { ResetValues(); }

	public virtual void DoUpdateLogic()
	{


	}

	public virtual void DoFixedUpdateLogic() { }

	public virtual void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType) { }

	public virtual void ResetValues() { }

}
