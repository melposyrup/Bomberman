using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
	[SerializeField] private float rotationAmount;

	void Start()
	{
		rotationAmount = 180 * Time.deltaTime; // 360 degrees per second
	}


	void Update()
	{
		transform.Rotate(0, rotationAmount, 0, Space.World);
	}
}
