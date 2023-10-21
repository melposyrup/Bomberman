using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemMoveable
{
	Rigidbody Rigidbody { get; set; }


	void MoveItem(Vector3 velocity);
	void MoveItemKinematic(Vector3 velocity);


}
