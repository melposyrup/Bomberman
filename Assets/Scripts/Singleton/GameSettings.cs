using System.Collections.Generic;
using UnityEngine;


// EXAMPLE
// GameSettings.Instance.AddScore(1);
public class GameSettings : MonoBehaviour
{
	public static GameSettings Instance { get; private set; }


	// numbers of players
	private int _playerCount = 2;
	public int PlayerCount
	{
		get { return _playerCount; }
		set { _playerCount = value; }
	}


	// store the scores of the players
	public Dictionary<int, int> PlayerScores;
	public Dictionary<int, Color> PlayerColors;

	public int LastWinner { get; set; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			PlayerScores = new Dictionary<int, int>();
			PlayerColors = new Dictionary<int, Color>();
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// initialize player scores and colors
	/// </summary>
	public void Initialize()
	{
		PlayerScores.Clear();
		PlayerColors.Clear();
	}

	/// <summary>
	/// playerId from 1 to 2
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="score"></param>
	public void AddScore(int playerId, int score = 1)
	{
		if (PlayerScores.ContainsKey(playerId))
		{
			PlayerScores[playerId] += score;
			Debug.Log($"Find player: {playerId} , Score: {PlayerScores[playerId]}");
		}
		else
		{
			Debug.LogWarning("Check playerScore initialize.");
		}

	}

	public int GetScore(int playerId)
	{
		if (PlayerScores.ContainsKey(playerId))
		{
			return PlayerScores[playerId];
		}
		else
		{
			Debug.Log($"Create player: {playerId} , Score: 0");
			PlayerScores.Add(playerId, 0);
			return 0;
		}		
	}

	public Color GetColor(int playerId)
	{
		if (PlayerColors.ContainsKey(playerId))
		{
			return PlayerColors[playerId];
		}

		return Color.cyan;
	}
}
