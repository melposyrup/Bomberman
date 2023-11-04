using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <para> state change in GameScene </para>
/// <para> Enter -> Start -> ->End -> MoveToResult </para>
/// <para> Enter -> Start -> Hurry -> End -> MoveToResult </para>
/// </summary>

public class GameSceneManager : SceneManagerBase
{

	[SerializeField] private int _playerCount;

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

	#region Player Death
	private Dictionary<int, bool> playerLife;
	public void HandlePlayerDeath(int playerNum)
	{
		_playerCount--;
		playerLife[playerNum] = false;

		// end of game
		if (_playerCount < 2)
		{
			//find surviving player
			int? lastAlive = FindLastAlivePlayer();

			//add score to surviving player
			if (lastAlive != null)
			{
				EventManager.AddPlayerScore.Invoke((int)lastAlive);
				GameSettings.Instance.LastWinner = (int)lastAlive;
			}
			else { GameSettings.Instance.LastWinner = 0; } // draw

			sceneStateMachine.ChangeState(EndState);
		}
	}

	private int? FindLastAlivePlayer()
	{
		int? lastAlivePlayerId = null;

		foreach (var pair in playerLife)
		{
			if (pair.Value) { lastAlivePlayerId = pair.Key; }
		}

		return lastAlivePlayerId;
	}

	#endregion

	protected virtual void Awake()
	{
		EventManager = this.GetComponent<GameEventManager>();

		sceneStateMachine = new SceneStateMachine();
		EnterState = new GameEnterState(this);
		StartState = new GameStartState(this);
		HurryState = new GameHurryState(this);
		EndState = new GameEndState(this);

		playerLife = new Dictionary<int, bool>();

	}
	protected virtual void Start()
	{
		sceneStateMachine.Initialize(EnterState);

		Timer = GameObject.Find("Timer").GetComponent<TimerManager>();
		if (!Timer) { Debug.Log("TimerPrefab is Undefined"); }

		// Initialize all players living status
		_playerCount = GameSettings.Instance.PlayerCount;
		for (int playerId = 1; playerId < _playerCount + 1; playerId++)
		{
			playerLife[playerId] = true;
		}
	}

	protected virtual void Update()
	{
		sceneStateMachine.CurrentState.UpdateState();

		//test
		if (Input.GetKeyDown(KeyCode.I))
		{
			sceneStateMachine.ChangeState(HurryState);

		}
	}




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
		//Debug.Log("GameEnterState");
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
		//Debug.Log("GameStartState");
		// 1. players controllable
		// 2. Start Timer 
		gameSceneManager.EventManager.StartGameScene.Invoke();

	}

	public override void UpdateState()
	{
		// if Timer < 60 seconds, change state to HurryState
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
		//Debug.Log("HurryState");
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
		//Debug.Log("GameEndState");
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
			gameSceneManager.SceneChange(SceneManagerBase.EScene.ResultScene);
		}

	}

}
