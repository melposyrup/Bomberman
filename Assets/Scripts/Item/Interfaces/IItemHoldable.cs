using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemHoldable
{
	bool IsOnHold { get; set; }
	void SetOnHoldStatus(bool isHolding);
}
