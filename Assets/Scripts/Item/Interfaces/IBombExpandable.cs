using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBombExpandable
{
	Vector3 MaxScale { get; set; }
	float ExpandFactor { get; set; }
	float MaxExpandFactor { get; set; }

	void Expand();

}
