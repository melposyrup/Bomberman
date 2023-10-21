using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ShowSphereCollider : MonoBehaviour
{
	private SphereCollider sphereCollider;
	[SerializeField] float _radius;

	void Start()
	{
		sphereCollider = GetComponent<SphereCollider>();
		_radius =GetComponent<SphereCollider>().radius;
	}

	void OnDrawGizmos()
	{
		if (sphereCollider == null) return;

		Gizmos.color = Color.green;
		Gizmos.matrix = Matrix4x4.TRS(
			transform.position,
			transform.rotation,
			transform.lossyScale);

		Gizmos.DrawWireSphere(radius: _radius, center: Vector3.zero);
	}
}

