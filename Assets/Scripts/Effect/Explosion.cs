using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	// Total animation time in seconds アニメーションの全期間（秒）
	public float animationDuration = 1.0f;
	// Rotation speed in degrees per second 回転速度（秒ごとの度数）
	public float rotationSpeed = -360f;

	private Vector3 initialScale;
	private Material material; 
	private float elapsedTime = 0.0f; // 経過時間

	void Start()
	{
		initialScale = transform.localScale; 
		material = GetComponent<MeshRenderer>().material; 
		material.color = new Color(1f, 1f, 1f, 1f); // 初期の色と透明度を1に設定
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;

		// Calculate scale and alpha based on elapsed time 経過時間に基づいてscaleとalphaを計算
		float progress = elapsedTime / animationDuration;
		float currentScale = Mathf.Lerp(1f, 2f, progress);
		float alpha = Mathf.Lerp(1f, 0f, progress);

		// update scale, alpha and rotation 
		transform.localScale = initialScale * currentScale;
		material.color = new Color(1f, 1f, 1f, alpha);
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

		// Destroy the game object once the animation finishes アニメーションが終了したらゲームオブジェクトを破棄
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