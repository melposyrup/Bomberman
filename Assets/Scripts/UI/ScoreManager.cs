using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	// プレイヤースコアUIのオブジェクト取得用
	public Text[] PlayerScore;

	void Start()
	{
		for (int i = 0; i < PlayerScore.Length; i++)
		{
			// get palyer score from GameSettings
			int score = GameSettings.Instance.GetScore(i + 1);
			PlayerScore[i].text = score.ToString();
		}

	}

	public void AddPlayerScore(int playerNum)
	{
		
		if (playerNum > 0 && playerNum < (PlayerScore.Length + 1))
		{
			GameSettings.Instance.AddScore(playerNum);

			int numGet = GameSettings.Instance.GetScore(playerNum);
			Debug.Log("!!!!playerNum:" + numGet);

			PlayerScore[playerNum - 1].text =
				GameSettings.Instance.GetScore(playerNum).ToString();
		}
		else
		{
			Debug.LogError("Invalid player number.");
		}
	}
}
