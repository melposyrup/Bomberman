using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingImage : MonoBehaviour
{
	private Image _image;

	void Start()
	{
		_image = this.GetComponent<Image>();
		_image.color = new Color(0, 0, 0, 0);
	}

	public void StartFadingIn(float duration = 1f)
	{
		StartCoroutine(FadeIn(duration));
	}

	private IEnumerator FadeIn(float duration = 1f)
	{
		float elapsed = 0f;
		Color startColor = _image.color;
		Color endColor = new Color(0, 0, 0, 1);

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = Mathf.Clamp01(elapsed / duration);
			_image.color = Color.Lerp(startColor, endColor, t);
			yield return null;
		}

		_image.color = endColor;
	}

	public void StartFadingOut(float duration = 1f)
	{
		StartCoroutine(FadeOut(duration));
	}

	private IEnumerator FadeOut(float duration = 1f)
	{
		float elapsed = 0f;
		Color startColor = new Color(0, 0, 0, 1);
		Color endColor = new Color(0, 0, 0, 0);

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = Mathf.Clamp01(elapsed / duration);
			_image.color = Color.Lerp(startColor, endColor, t);
			yield return null;
		}

		_image.color = endColor;
	}

}
