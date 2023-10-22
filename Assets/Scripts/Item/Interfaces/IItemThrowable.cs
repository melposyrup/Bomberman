using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemThrowable
{
	bool IsThrow { get; set; }
	void SetThrowStatus(bool isThrow);

	Transform IsThrownBy { get; set; }
	void SetThrownBy(Transform transform);

	Vector3 ThrowDirection { get; set; }
	void SetThrowDirection(Vector3 direction);

	bool IsOnLand { get; set; }
	void SetOnLand(bool onLand);
}
