using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState
{
	protected SceneManagerBase sceneManagerBase;

	public SceneState(SceneManagerBase sceneManagerBase)
	{
		this.sceneManagerBase = sceneManagerBase;
	}

	public virtual void EnterState() { }
	public virtual void UpdateState() { }

}
