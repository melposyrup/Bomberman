using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : SceneManagerBase
{
	public KeyCode InputNextScene = KeyCode.Return;

	[SerializeField] private FadingImage _fadingImage;
	[SerializeField] private Text _startText;

	private void Start()
	{
		if (_fadingImage) { _fadingImage.StartFadingOut(); }
		StartCoroutine(ChangeColorCoroutine());
	}


	private void Update()
	{
		PressKeyForSceneChange();
	}

	/// <summary>
	/// process input key for scene change
	/// </summary>
	private void PressKeyForSceneChange()
	{
		if (Input.GetKeyDown(InputNextScene))
		{
			_fadingImage.StartFadingIn();
			Invoke("BackToSelectionScene", 1f);
		}
	}
	private void BackToSelectionScene()
	{
		SceneChange(SceneManagerBase.EScene.SelectionScene);
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
