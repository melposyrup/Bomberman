using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemKickable
{
	bool IsKick { get; set; }
	void SetKickStatus(bool isKick);

	Transform IsKickedBy { get; set; }
	void SetKickedBy(Transform transform);
}
