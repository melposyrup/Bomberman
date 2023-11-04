using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Setup following prefabs in inspector:</para>
/// <para>- PlayerModelPrefab</para>
/// <para>- ScorePrefab</para>
/// <para>- FadingImage</para>
/// </summary>

public class ResultSceneManager : SceneManagerBase
{
	public KeyCode InputNewGame = KeyCode.A;

	// cooldown time for starting new game
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

	private void Start()
	{	
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
	}

	private void Update()
	{
		// press the key to load GameScene
		PressKeyToStartNewGame();
	}


	/// <summary>
	/// scene change with fading in animation
	/// </summary>
	private void PressKeyToStartNewGame()
	{
		if (_countdown < 0)
		{
			if (Input.GetKeyDown(InputNewGame))
			{
				FadingImage.StartFadingIn();
				Invoke("StartNewGameAfterDelay", 1f);
			}
		}
		else { _countdown -= Time.deltaTime; }
	}
	private void StartNewGameAfterDelay()
	{
		SceneChange(SceneManagerBase.EScene.GameScene);
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
			//TODO: set player model color by SetPlayerColor() in PlayerController.cs
			Color newPlayerColor = GameSettings.Instance.GetColor(i + 1);

			MeshRenderer meshRenderer = newPlayerModel.GetComponent<MeshRenderer>();
			if (meshRenderer != null)
			{
				Material newMat = new Material(meshRenderer.material);
				newMat.color = newPlayerColor;
				meshRenderer.material = newMat;
			}

			// process player scores
			int score = GameSettings.Instance.GetScore(i + 1);

			// if this player is the winner,
			// update scoreTargetPosition,
			// keep the last score for scoreAnimation
			if (i + 1 == GameSettings.Instance.LastWinner)
			{
				scoreTargetPosition = playerTransforms[i].position
					+ new Vector3(_distanceX * score, 0, 0);
				score -= 1;
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
	}




}
