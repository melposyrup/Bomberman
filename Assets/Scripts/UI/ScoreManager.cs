using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	// プレイヤースコアのオブジェクト取得用
	//public GameObject Score;
	public Text[] PlayerScore;
	//public SaveData saveData;
	[SerializeField] private int[] playerScores;

	void Start()
	{
		// int totalPlayers = Data.TotalPlayers;

		playerScores = new int[PlayerScore.Length];
		for (int i = 0; i < PlayerScore.Length; i++)
		{
			PlayerScore[i].text = "0";
			//PlayerScore[i].text = saveData.playerScores[i].ToString();
		}

	}

	public void AddPlayerScore(int playerNum)
	{
		Debug.Log("playerNum:" + playerNum);
		if (playerNum > 0 && playerNum < (PlayerScore.Length + 1))
		{
			playerScores[playerNum]++;

			PlayerScore[playerNum].text = playerScores[playerNum].ToString();
		}
		else
		{
			Debug.LogError("Invalid player number.");
		}
	}
}
