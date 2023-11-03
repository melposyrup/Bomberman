using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurryUp : MonoBehaviour
{
	public float speed = 700.0f;
	private RectTransform _rectTransform;
	private RectTransform _rectTransformCanvas;

	void Start()
	{
		_rectTransform = this.GetComponent<RectTransform>();
		_rectTransformCanvas = this.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		float initialYPosition =
			-_rectTransformCanvas.rect.height / 2 - _rectTransform.rect.height / 2;
		_rectTransform.anchoredPosition = new Vector2(0, initialYPosition);
	}

	void Update()
	{
		_rectTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);

		if (_rectTransform.anchoredPosition.y - _rectTransform.rect.height / 2
			> _rectTransformCanvas.rect.height / 2)
		{
			this.enabled = false;
		}
	}

	public void Active()
	{
		this.enabled = true;
	}
}
