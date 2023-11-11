using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para>Setup following prefabs in inspector:</para>
/// <para>- PlayerModelPrefab</para>
/// <para>- ScorePrefab</para>
/// <para>- FadingImage</para>
/// </summary>

public class ResultSceneManager : SceneManagerBase
{
	private bool _hasWinner = false;
	[SerializeField] private Text _startText;

	// cooldown for input key
	private float _countdown = 2;

	// setup prefabs in inspector
	public GameObject PlayerModelPrefab;
	public GameObject ScorePrefab;
	public FadingImage FadingImage;

	// for player model position calculation
	private float _startY = 5.0f; // Y's maximum value
	private float _endY = -5.0f; // Y's minimum value
	private float _startX = -6f;
	// for player score position calculation
	private float _distanceX = 3f;
	private float _fixedZ = 4f;
	private float _timeToMove = 2f;

	[SerializeField] private Transform[] playerTransforms;
	[SerializeField] private Transform scoreTransfrom;
	[SerializeField] private Vector3 scoreStartPosition = new Vector3(0, 0, -6);
	[SerializeField] private Vector3 scoreTargetPosition = Vector3.zero;

	private GameObject winner;

	private void Start()
	{
		// check if there is a winner
		CheckWinner();

		// show player models and scores
		InstantiatePlayerModelsAndScores();

		// play startAnimation if not draw
		if (GameSettings.Instance.LastWinner != 0)
		{
			scoreTransfrom = Instantiate(ScorePrefab,
				scoreStartPosition,
				Quaternion.identity).transform;
			ScoreMovingAnimation();
		}

		// play fade out animation
		if (FadingImage) { FadingImage.StartFadingOut(); }

		// play sound
		SoundManager.Instance.PlaySE(SESoundData.SE.ResultSE);//just once

		StartCoroutine(ChangeColorCoroutine());
	}

	private void Update()
	{
		// press the key to load GameScene
		PressKeyForSceneChange();
	}

	/// <summary>
	/// if someone has 3 scores, set _hasWinner to true
	/// </summary>
	private void CheckWinner()
	{
		// get score date from GameSettings
		foreach (var pair in GameSettings.Instance.PlayerInfoDictionary)
		{
			if (pair.Value.Score >= 3)
			{
				_hasWinner = true;
				break;
			}
		}
	}

	/// <summary>
	/// scene change with fadingIn animation
	/// </summary>
	private void PressKeyForSceneChange()
	{
		if (_countdown < 0)
		{
			if (Input.GetButtonDown("InputNextScene"))
			{
				FadingImage.StartFadingIn();
				Invoke("StartNewGame", 1f);
			}
		}
		else { _countdown -= Time.deltaTime; }
	}
	private void StartNewGame()
	{
		SoundManager.Instance.StopSE(SESoundData.SE.ResultSE);
		if (_hasWinner) { SceneChange(SceneManagerBase.EScene.WinnerScene); }
		else { SceneChange(SceneManagerBase.EScene.GameScene); }
	}

	/// <summary>
	/// automatically calculate positions of Prefabs and instantiate them
	/// </summary>
	private void InstantiatePlayerModelsAndScores()
	{
		int playerCount = GameSettings.Instance.PlayerCount;
		if (playerCount < 2) { Debug.LogError("invalid playerCount"); return; }

		playerTransforms = new Transform[playerCount];

		float interval = (_startY - _endY) / (playerCount - 1);

		// for each player
		for (int i = 0; i < playerCount; i++)
		{
			// instantiate player model
			GameObject newPlayerModel = Instantiate(PlayerModelPrefab);
			playerTransforms[i] = newPlayerModel.transform;

			// set player model position
			float posY = _startY - (interval * i);
			playerTransforms[i].position = new Vector3(_startX, posY, _fixedZ);

			// set player model color
			Material material = GameSettings.Instance.GetMaterial(i + 1);

			SkinnedMeshRenderer meshRenderer = 
				newPlayerModel.GetComponentInChildren<SkinnedMeshRenderer>();
			if (meshRenderer != null)
			{
				meshRenderer.material = material;
			}

			// process player scores
			int score = GameSettings.Instance.GetScore(i + 1);

			// if this player is the winner,
			// update scoreTargetPosition,
			// keep the last score for ScoreMovingAnimation()
			if (i + 1 == GameSettings.Instance.LastWinner)
			{
				scoreTargetPosition = playerTransforms[i].position
					+ new Vector3(_distanceX * score, 0, 0);
				score -= 1;

				winner = playerTransforms[i].gameObject;
			}

			for (int j = 0; j < score; j++)
			{
				// instantiate score prefab
				GameObject newScore = Instantiate(ScorePrefab);
				// set position
				Vector3 pos = playerTransforms[i].position
					+ new Vector3(_distanceX * (j + 1), 0, 0);
				newScore.transform.position = pos;
			}
		}
	}


	/// <summary>
	/// move scorePrefab from screen center to the target position in 2 seconds
	/// </summary>
	private void ScoreMovingAnimation()
	{
		StartCoroutine(MoveToPosition());
	}
	private IEnumerator MoveToPosition()
	{
		var startPosition = scoreTransfrom.position;
		var timePassed = 0f;

		while (timePassed < _timeToMove)
		{
			var lerpValue = timePassed / _timeToMove;

			// Update the position of the ScoreTransform
			scoreTransfrom.position = Vector3.Lerp(
				startPosition, scoreTargetPosition, lerpValue);

			timePassed += Time.deltaTime;

			yield return null;
		}
		scoreTransfrom.position = scoreTargetPosition;

		// play WinAnimation of player
		winner.GetComponentInChildren<Animator>().SetTrigger("Win");
	}


	/// <summary>
	/// for _startText color change
	/// </summary>
	/// <returns></returns>
	IEnumerator ChangeColorCoroutine()
	{
		Color yellowColor = Color.yellow;
		Color orangeColor = new Color(1f, 0.5f, 0f);
		float duration = 2f;

		while (true)
		{
			yield return LerpColor(yellowColor, orangeColor, duration);
			yield return LerpColor(orangeColor, yellowColor, duration);
		}
	}
	IEnumerator LerpColor(Color fromColor, Color toColor, float duration)
	{
		float time = 0;
		while (time < duration)
		{
			_startText.color = Color.Lerp(fromColor, toColor, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		_startText.color = toColor;
	}

}
