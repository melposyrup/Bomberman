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

		Gizmos.color = Color.green; // 注释：设置Gizmo颜色。
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale); // 注释：设置Gizmo的位置、旋转和缩放。

		// 注释：绘制一个立方体。
		Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
	}
}
