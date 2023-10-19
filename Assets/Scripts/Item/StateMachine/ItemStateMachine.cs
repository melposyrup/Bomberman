using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStateMachine 
{
	public ItemState CurrentItemState { get; set; }

	public void Initialize(ItemState startingState)
	{
		CurrentItemState = startingState;
		CurrentItemState.EnterState();
	}

	public void ChangeState(ItemState newState)
	{
		CurrentItemState.ExitState();
		CurrentItemState = newState;
		CurrentItemState.EnterState();
	}

}
