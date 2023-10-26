using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	// ���˥�`������ȫ���g���룩
	public float animationDuration = 1.0f;
	// ��ܞ�ٶȣ��뤴�Ȥζ�����
	public float rotationSpeed = -360f;

	private Vector3 initialScale; // ���ڥ����`��
	private Material material; // �ޥƥꥢ��
	private float elapsedTime = 0.0f; // �U�^�r�g

	void Start()
	{
		initialScale = transform.localScale; // ���ڥ����`���ȡ��
		material = GetComponent<MeshRenderer>().material; // Mesh Renderer����ޥƥꥢ���ȡ��		
		material.color = new Color(1f, 1f, 1f, 1f); // ���ڤ�ɫ��͸���Ȥ�1���O��
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;

		// �U�^�r�g�˻��Ť���scale��alpha��Ӌ��
		float progress = elapsedTime / animationDuration;
		float currentScale = Mathf.Lerp(1f, 2f, progress);
		float alpha = Mathf.Lerp(1f, 0f, progress);

		// scale��alpha���m��
		transform.localScale = initialScale * currentScale;
		material.color = new Color(1f, 1f, 1f, alpha);

		// ���֥������Ȥ��ܞ������
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

		// ���˥�`����󤬽K�ˤ����顢���֥������Ȥ��Ɖ�
		if (elapsedTime >= animationDuration)
		{
			Destroy(gameObject);
		}
	}
}