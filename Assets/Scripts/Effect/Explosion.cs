using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	// アニメーションの全期間（秒）
	public float animationDuration = 1.0f;
	// 回転速度（秒ごとの度数）
	public float rotationSpeed = -360f;

	private Vector3 initialScale; // 初期スケール
	private Material material; // マテリアル
	private float elapsedTime = 0.0f; // 経過時間

	void Start()
	{
		initialScale = transform.localScale; // 初期スケールを取得
		material = GetComponent<MeshRenderer>().material; // Mesh Rendererからマテリアルを取得		
		material.color = new Color(1f, 1f, 1f, 1f); // 初期の色と透明度を1に設定
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;

		// 経過時間に基づいてscaleとalphaを計算
		float progress = elapsedTime / animationDuration;
		float currentScale = Mathf.Lerp(1f, 2f, progress);
		float alpha = Mathf.Lerp(1f, 0f, progress);

		// scaleとalphaを適用
		transform.localScale = initialScale * currentScale;
		material.color = new Color(1f, 1f, 1f, alpha);

		// オブジェクトを回転させる
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

		// アニメーションが終了したら、オブジェクトを破壊
		if (elapsedTime >= animationDuration)
		{
			Destroy(gameObject);
		}
	}
}