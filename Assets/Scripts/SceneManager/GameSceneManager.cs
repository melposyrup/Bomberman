using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// state change in GameScene
// Enter -> Start -> ->End -> MoveToResult
// Enter -> Start -> UrgentCountdown -> End -> MoveToResult

public class GameSceneManager : SceneManagerBase
{

	[SerializeField] private int _alivePlayerCount;
	[SerializeField] private bool _isGameEnd = false;

	public GameEventManager EventManager;
	public TimerManager Timer;

	#region State Machine Variables
	public SceneStateMachine sceneStateMachine;

	// game scene states
	public GameEnterState EnterState { get; set; }
	public GameStartState StartState { get; set; }
	public GameHurryState HurryState { get; set; }
	public GameEndState EndState { get; set; }

	#endregion

	protected virtual void Awake()
	{
		EventManager = this.GetComponent<GameEventManager>();

		sceneStateMachine = new SceneStateMachine();
		EnterState = new GameEnterState(this);
		StartState = new GameStartState(this);
		HurryState = new GameHurryState(this);
		EndState = new GameEndState(this);
	}
	protected virtual void Start()
	{
		sceneStateMachine.Initialize(EnterState);
		// Initialize the number of alive players by finding them via their tag
		_alivePlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;

		Timer = GameObject.Find("Timer").GetComponent<TimerManager>();
		if (!Timer) { Debug.Log("TimerPrefab is Undefined"); }
	}

	protected virtual void Update()
	{
		sceneStateMachine.CurrentState.UpdateState();

		if (_isGameEnd) { sceneStateMachine.ChangeState(EndState); }

		//test
		if (Input.GetKeyDown(KeyCode.I))
		{
			sceneStateMachine.ChangeState(HurryState);

		}
	}

	#region Player Variables
	public void HandlePlayerDeath()
	{
		_alivePlayerCount--;
		if (_alivePlayerCount == 1) { _isGameEnd = true; }
		Debug.Log("_alivePlayerCount =" + _alivePlayerCount);

	}
	#endregion


}
/// <summary>
/// 
/// </summary>
public class GameEnterState : SceneState
{
	GameSceneManager gameSceneManager;
	private float _countdown = 3.0f;

	public GameEnterState(SceneManagerBase sceneManagerBase) : base(sceneManagerBase)
	{
		if (sceneManagerBase is GameSceneManager manager)
		{ gameSceneManager = manager; }
	}

	public override void EnterState()
	{
		Debug.Log("GameEnterState");
		// 1. fading in
		// 2. READY GO ¤Î¥á¥Ã¥»©`¥¸¤ò±íÊ¾
		// 3. players uncontrollable
		gameSceneManager.EventManager.EnterGameScene.Invoke();
	}
	public override void UpdateState()
	{
		if (_countdown > 0)
		{ _countdown -= Time.deltaTime; }
		else
		{
			// change state
			gameSceneManager.sceneStateMachine.ChangeState(
				gameSceneManager.StartState);
		}
	}
}
/// <summary>
/// 
/// </summary>
public class GameStartState : SceneState
{
	GameSceneManager gameSceneManager;
	public GameStartState(SceneManagerBase sceneManagerBase) : base(sceneManagerBase)
	{
		if (sceneManagerBase is GameSceneManager manager)
		{ gameSceneManager = manager; }
	}

	public override void EnterState()
	{
		Debug.Log("GameStartState");
		// 1. players controllable
		// 2. Start Timer 
		gameSceneManager.EventManager.StartGameScene.Invoke();

	}

	public override void UpdateState()
	{
		// if Timer < 60 seconds, change state to UrgentCountdownState
		if (gameSceneManager.Timer.GetMinutes() < 1)
		{
			gameSceneManager.sceneStateMachine.ChangeState(
				gameSceneManager.HurryState);
		}
	}

}
/// <summary>
/// 
/// </summary>
public class GameHurryState : SceneState
{
	GameSceneManager gameSceneManager;

	public GameHurryState(SceneManagerBase sceneManagerBase) : base(sceneManagerBase)
	{
		if (sceneManagerBase is GameSceneManager manager)
		{ gameSceneManager = manager; }
	}

	public override void EnterState()
	{
		Debug.Log("GameUrgentCountdownState");
		// 1. start Hurry animation (HURRY UP !)
		// 2. change player move speed
		gameSceneManager.EventManager.HurryGameScene.Invoke();
	}

	public override void UpdateState() { }

}

/// <summary>
/// 
/// </summary>
public class GameEndState : SceneState
{
	GameSceneManager gameSceneManager;
	private float _countdown = 3.0f;


	public GameEndState(SceneManagerBase sceneManagerBase) : base(sceneManagerBase)
	{
		if (sceneManagerBase is GameSceneManager manager)
		{ gameSceneManager = manager; }
	}

	public override void EnterState()
	{
		Debug.Log("GameEndState");
		// 1. stop timer
		// 2. players uncontrollable
		gameSceneManager.EventManager.EndGameScene.Invoke();
	}

	public override void UpdateState()
	{
		// wait for 3 seconds
		if (_countdown > 1)
		{
			_countdown -= Time.deltaTime;
		}
		else if (_countdown > 0)
		{
			_countdown -= Time.deltaTime;
			// call fadingIn, default duration = 1f
			gameSceneManager.EventManager.FadingIn.Invoke();
		}
		else
		{
			// change scene
			gameSceneManager.SceneChange(3);
			Debug.Log("SceneChange");
		}

	}

}
