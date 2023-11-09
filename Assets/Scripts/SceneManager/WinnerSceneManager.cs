using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WinnerSceneManager : SceneManagerBase
{
	public KeyCode InputBackToSelection = KeyCode.A;

	public Animator PlayerModelAnimator;

	// setup prefabs in inspector
	public FadingImage FadingImage;
	public Transform BannerTransform;
	public Transform Background_white;
	public Transform Background_color;

	// cooldown for input key
	private float _countdown = 2;

	// for banner moving
	[SerializeField] private Vector3 bannerStartPos = new Vector3(0, -0.4f, -3.6f);
	[SerializeField] private Vector3 bannerTargetPos = new Vector3(0, -2.7f, 1.5f);
	private float duration = 5f;

	// for background rotating
	private float repeatingDuration = 1.4f;

	private void Start()
	{
		StartCoroutine(MovingBanner());
		RotatingBackImage();

		//TODO: player model animation

		// play fade out animation
		if (FadingImage) { FadingImage.StartFadingOut(); }

		// player sound
		SoundManager.Instance.PlayBGM(BGMSoundData.BGM.WinnerScene);
	}

	private void Update()
	{
		PressKeyForSceneChange();
	}

	private void PlayerAnimation()
	{
		PlayerModelAnimator.SetTrigger("Win");
	}

	/// <summary>
	/// scene change with fadingIn animation
	/// </summary>
	private void PressKeyForSceneChange()
	{
		if (_countdown < 0)
		{
			if (Input.GetKeyDown(InputBackToSelection))
			{
				FadingImage.StartFadingIn();
				Invoke("BackToSelectionScene", 1f);
			}
		}
		else { _countdown -= Time.deltaTime; }
	}
	private void BackToSelectionScene()
	{
		SceneChange(SceneManagerBase.EScene.SelectionScene);
	}

	/// <summary>
	/// moving banner from start position to target position in 5 seconds
	/// </summary>
	IEnumerator MovingBanner()
	{
		BannerTransform.position = bannerStartPos;

		float startTime = Time.time;
		float endTime = startTime + duration;

		while (Time.time < endTime)
		{
			float elapsedRatio = (Time.time - startTime) / duration;
			BannerTransform.position =
				Vector3.Lerp(bannerStartPos, bannerTargetPos, elapsedRatio);
			yield return null;
		}
		BannerTransform.position = bannerTargetPos;

		PlayerAnimation();
	}

	/// <summary>
	/// roating background images
	/// </summary>
	private void RotatingBackImage()
	{
		StartCoroutine(RotateInDirection(Background_white, true));
		StartCoroutine(RotateInDirection(Background_color, false));
	}

	IEnumerator RotateInDirection(Transform target, bool clockwise)
	{
		float degreesPerSecond = 180f / repeatingDuration;

		while (true)
		{
			Quaternion targetRotation =
				Quaternion.Euler(0, 0, target.eulerAngles.z + (clockwise ? -180f : 180f));

			float startTime = Time.time;
			float endTime = startTime + repeatingDuration;

			while (Time.time < endTime)
			{
				float elapsed = Time.time - startTime;
				float angleThisFrame = degreesPerSecond * Time.deltaTime;

				target.rotation = Quaternion.RotateTowards(
					target.rotation,
					targetRotation,
					angleThisFrame);
				yield return null;
			}

			target.rotation = Quaternion.identity;

			yield return null;
		}
	}



}