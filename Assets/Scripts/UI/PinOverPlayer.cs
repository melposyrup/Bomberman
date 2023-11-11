using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinOverPlayer : MonoBehaviour
{
	//set in inspector
	public Transform player;

	public Image icon;
	public float yOffset = 20f;
	private void Awake()
	{
		icon = GetComponent<Image>();
	}
	void Update()
	{
		if (player != null && icon != null)
		{
			Vector3 playerHeight = Vector3.up * 1f;
			Vector3 iconPosition = player.position + playerHeight;
			Vector3 screenPosition = Camera.main.WorldToScreenPoint(iconPosition);
			screenPosition.y += yOffset;
			icon.transform.position = screenPosition;
		}
	}
}
