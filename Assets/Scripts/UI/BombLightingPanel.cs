using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLightingPanel : MonoBehaviour
{
	[SerializeField]private SpriteRenderer bombLine;
	[SerializeField]private SpriteRenderer bombLineLighting;
	[SerializeField]private SpriteRenderer explosionLine;
	[SerializeField]private SpriteRenderer explosionLineLighting;

	public void SetBombLighting(bool onOff)
	{
		bombLineLighting.color = new Color(1, 1, 1, onOff ? 1 : 0);
		bombLine.color = new Color(1, 1, 1, onOff ? 0 : 1);
	}

	public void SetExplosionLighting(bool onOff)
	{
		explosionLineLighting.color = new Color(1, 1, 1, onOff ? 1 : 0);
		explosionLine.color = new Color(1, 1, 1, onOff ? 0 : 1);
	}

}
