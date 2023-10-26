using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	// Total animation time in seconds ���˥�`������ȫ���g���룩
	public float animationDuration = 1.0f;
	// Rotation speed in degrees per second ��ܞ�ٶȣ��뤴�Ȥζ�����
	public float rotationSpeed = -360f;

	private Vector3 initialScale;
	private Material material; 
	private float elapsedTime = 0.0f; // �U�^�r�g

	void Start()
	{
		initialScale = transform.localScale; 
		material = GetComponent<MeshRenderer>().material; 
		material.color = new Color(1f, 1f, 1f, 1f); // ���ڤ�ɫ��͸���Ȥ�1���O��
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;

		// Calculate scale and alpha based on elapsed time �U�^�r�g�˻��Ť���scale��alpha��Ӌ��
		float progress = elapsedTime / animationDuration;
		float currentScale = Mathf.Lerp(1f, 2f, progress);
		float alpha = Mathf.Lerp(1f, 0f, progress);

		// update scale, alpha and rotation 
		transform.localScale = initialScale * currentScale;
		material.color = new Color(1f, 1f, 1f, alpha);
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

		// Destroy the game object once the animation finishes ���˥�`����󤬽K�ˤ����饲�`�४�֥������Ȥ��Ɨ�
		if (elapsedTime >= animationDuration)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// once attach to player, change player's death bool to true
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerTest>().SetDeadStatus(true);
		}
	}
}