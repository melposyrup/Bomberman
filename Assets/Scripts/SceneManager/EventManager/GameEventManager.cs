using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// HOW TO USE
// in Player.cs:
// GameEventManager.OnPlayerDeath.Invoke(PlayerNum);
// in inspector:
// setup the Objects and Functions to be called when the event is invoked
public class GameEventManager : MonoBehaviour
{
	// !for GameScene
	// call ReadyGo(), GameSceneInitialize(), PlayerInitialize(),
	public UnityEvent EnterGameScene;
	// start timer, player controllable
	public UnityEvent StartGameScene;
	// when countdown is under 60 seconds, or someone takes a special item
	public UnityEvent HurryGameScene;
	// game end
	public UnityEvent EndGameScene;

	public UnityEvent FadingIn;


	[System.Serializable]
	public class IntEvent : UnityEvent<int> { }

	public IntEvent OnPlayerDeath;

	public IntEvent AddPlayerScore;



}
