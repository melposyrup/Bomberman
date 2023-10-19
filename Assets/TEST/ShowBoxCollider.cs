using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShowBoxCollider : MonoBehaviour
{
	private BoxCollider boxCollider;

	void Start()
	{
		boxCollider = GetComponent<BoxCollider>();
	}

	void OnDrawGizmos()
	{
		if (boxCollider == null) return;

		Gizmos.color = Color.green; // ע�ͣ�����Gizmo��ɫ��
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale); // ע�ͣ�����Gizmo��λ�á���ת�����š�

		// ע�ͣ�����һ�������塣
		Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
	}
}
