using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : SceneManagerBase
{
	public KeyCode InputNewGame = KeyCode.A;

	private float _countdown = 2;

	public GameObject PlayerModelPrefab;
	// player model position
	private float startY = 5.0f; // Y's maximum value
	private float endY = -5.0f; // Y's minimum value
	private float posX = -6f;
	private float posZ = 4f;

	[SerializeField] private Transform[] playerTransforms;

	private void Start()
	{
		// if someone gets 3 points, go to WinnerScene
		CheckPlayerScore();

		// show player models and scores
		InstantiatePlayerModelsAndScores();

		// intialize settings for star animation
		if (GameSettings.Instance.LastWinner != 0)
		{
			//!playAnimation if not draw
		}
	}



	private void Update()
	{
		// press the key to load GameScene
		StartNewGame();

		// play star animation

	}



	private void StartNewGame()
	{
		if (_countdown < 0)
		{
			if (Input.GetKeyDown(InputNewGame))
			{
				SceneChange(SceneManagerBase.EScene.GameScene);
			}
		}
		else { _countdown -= Time.deltaTime; }
	}

	private void CheckPlayerScore()
	{
		// get score date from GameSettings
		bool findWinner = false;
		foreach (var pair in GameSettings.Instance.PlayerScores)
		{
			if (pair.Value >= 3)
			{
				findWinner = true;
				break;
			}
		}
		if (findWinner) { SceneChange(SceneManagerBase.EScene.WinnerScene); }
	}

	private void InstantiatePlayerModelsAndScores()
	{
		int playerCount = GameSettings.Instance.PlayerCount;
		if (playerCount < 2) { Debug.LogError("invalid playerCount"); return; }

		playerTransforms = new Transform[playerCount];

		float interval = (startY - endY) / (playerCount - 1);

		for (int i = 0; i < playerCount; i++)
		{
			// instantiate player model
			float posY = startY - (interval * i);
			GameObject newPlayerModel = Instantiate(PlayerModelPrefab);
			playerTransforms[i] = newPlayerModel.transform;

			// set player model position
			playerTransforms[i].position = new Vector3(posX, posY, posZ);

			// set player model color
			Color newPlayerColor = GameSettings.Instance.GetColor(i + 1);
			//TODO: set player model color by SetPlayerColor() in PlayerController.cs

			MeshRenderer meshRenderer = newPlayerModel.GetComponent<MeshRenderer>();
			if (meshRenderer != null)
			{
				Material newMat = new Material(meshRenderer.material);
				newMat.color = newPlayerColor;
				meshRenderer.material = newMat;
			}

			// instantiate player scores



		}
	}




























}
