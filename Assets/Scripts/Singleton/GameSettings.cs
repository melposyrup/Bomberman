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
	public Dictionary<int, bool> PlayerNumberSetup;

	// PlayerMaterials[0] white
	// PlayerMaterials[1] black
	// PlayerMaterials[2] blue
	// PlayerMaterials[3] red
	public Material[] PlayerMaterials;

	public int LastWinner { get; set; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			PlayerScores = new Dictionary<int, int>();
			PlayerColors = new Dictionary<int, Color>();
			PlayerNumberSetup = new Dictionary<int, bool>();
			DontDestroyOnLoad(gameObject);
			
			// test
			InitializePlayerNumberSetup();
			
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// initialize player scores and colors, be called when get into selectScene
	/// </summary>
	public void InitializeAll()
	{
		PlayerScores.Clear();
		PlayerColors.Clear();
		PlayerNumberSetup.Clear();
	}

	// be called before change to gameScene
	private void InitializePlayerNumberSetup()
    {
		PlayerNumberSetup.Clear();
        for (int i = 1; i <= PlayerCount; i++)
        {
            PlayerNumberSetup[i] = true;
        }
    }

	// be called in PlayerController
	public int GetAvailablePlayerNumber()
	{
		foreach (KeyValuePair<int, bool> playerNumber in PlayerNumberSetup)
		{
			if (playerNumber.Value)
			{
            PlayerNumberSetup[playerNumber.Key] = false;
            return playerNumber.Key;
			}
		}

		return 0;
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
