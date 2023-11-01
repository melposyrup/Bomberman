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
	

	// !for SelectionScene


	// !for ResultScene

	// !for WinScene


	[System.Serializable]
	public class IntEvent : UnityEvent<int> { }

	public IntEvent OnPlayerDeath;



















	private void Start()
	{
		// IntEvent
		OnPlayerDeath = new IntEvent();

	}

	// !Singleton

	public static EventManager Instance;

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

	// Destroy the Singleton instance
	public void DestroySingleton()
	{
		if (Instance == this)
		{
			Destroy(gameObject);
			Instance = null;
		}
	}
}