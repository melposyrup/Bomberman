using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateMachine
{
	public SceneState CurrentState { get; set; }

	public void Initialize(SceneState startingState)
	{
		CurrentState = startingState;
		CurrentState.EnterState();
	}

	public void ChangeState(SceneState newState)
	{
		//CurrentState.ExitState();
		CurrentState = newState;
		CurrentState.EnterState();
	}


}
