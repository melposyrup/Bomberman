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
	//public Dictionary<int, bool> PlayerNumberSetup;
	//public Dictionary<int, int> PlayerScores;
	//public Dictionary<int, Color> PlayerColors;

	public Dictionary<int, PlayerInfo> PlayerInfoDictionary;




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

			PlayerInfoDictionary = new Dictionary<int, PlayerInfo>();

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
		PlayerInfoDictionary.Clear();


	}

	// be called before change to gameScene
	private void InitializePlayerNumberSetup()
    {
		PlayerInfoDictionary.Clear();
		for (int i = 1; i <= PlayerCount; i++)
		{
			Material material = PlayerMaterials[GlobalVariables.SelectColor1P];
			PlayerInfo playerInfo = new PlayerInfo(true, 0, material); 
			PlayerInfoDictionary[i] = playerInfo;
		}
	}

	// be called in PlayerController
	public int GetAvailablePlayerNumber()
	{
		foreach (KeyValuePair<int, PlayerInfo> playerInfoEntry in PlayerInfoDictionary)
		{
			if (playerInfoEntry.Value.Available)
			{
				// get key
				int playerNumber = playerInfoEntry.Key;
				// revise value
				PlayerInfo info = playerInfoEntry.Value;
				info.Available = false; 
				// put value back to dictionary
				PlayerInfoDictionary[playerNumber] = info;
				return playerNumber;
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
		if (PlayerInfoDictionary.ContainsKey(playerId))
		{
			PlayerInfo info = PlayerInfoDictionary[playerId];
			info.Score += score;

			Debug.Log($"Find player: {playerId} , Score: {info.Score}");
		}
		else
		{
			Debug.LogWarning("Check playerScore initialize.");
		}

	}

	public int GetScore(int playerId)
	{
		if (PlayerInfoDictionary.ContainsKey(playerId))
		{
			return PlayerInfoDictionary[playerId].Score;
		}
		else
		{
			Debug.Log($"Create player: {playerId} , Score: 0");
			PlayerInfo info = new PlayerInfo(true, 0, PlayerMaterials[0]);
			PlayerInfoDictionary.Add(playerId, info);
			return 0;
		}		
	}

	public Material GetMaterial(int playerId)
	{
		if (PlayerInfoDictionary.ContainsKey(playerId))
		{
			return PlayerInfoDictionary[playerId].PlayerMaterial;
		}

		return PlayerMaterials[0];
	}
}

[System.Serializable] 
public struct PlayerInfo
{
	public bool Available;
	public int Score;
	public Material PlayerMaterial;

	public PlayerInfo(bool setupComplete, int score, Material playerMaterial)
	{
		Available = setupComplete;
		Score = score;
		PlayerMaterial = playerMaterial;
	}
}