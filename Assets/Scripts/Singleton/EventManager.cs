using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// HOW TO USE
// in Player.cs:
// EventManager.Instance.OnPlayerDeath.Invoke(PlayerNum);
// in inspector:
// setup the Objects and Functions to be called when the event is invoked


public class EventManager : MonoBehaviour
{
	public static EventManager Instance;

	// !for SelectionScene

	// !for GameScene
	// call ReadyGo(), GameSceneInitialize(), PlayerInitialize(),
	public UnityEvent GetIntoGameScene;
	// when countdown is under 60 seconds, or someone takes a special item
	// UrgentCanvusStart()
	public UnityEvent StartUrgentCountdown;
	// UrgentCanvusStop() once the round is over
	public UnityEvent StopUrgentCountdown; 

	// !for ResultScene

	// !for WinScene


	[System.Serializable]
	public class IntEvent : UnityEvent<int> { }

	public IntEvent OnPlayerDeath;



















	private void Start()
	{
		OnPlayerDeath = new IntEvent();

		GetIntoGameScene= new UnityEvent();
		StartUrgentCountdown= new UnityEvent();
		StopUrgentCountdown= new UnityEvent();
	}



	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}