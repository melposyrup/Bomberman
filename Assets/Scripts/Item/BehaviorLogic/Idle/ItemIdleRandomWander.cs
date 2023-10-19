using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemIdleRandomWander", menuName = "ItemLogic/IdleLogic/ItemIdleRandomWander")]
public class ItemIdleRandomWander : ItemIdleSOBase
{
	[SerializeField] public float RandomWanderRadius = 3f;
	[SerializeField] public float RandomWanderSpeed = 0.5f;

	private Vector3 _targetPos;
	private Vector3 _direction;

	private float collisionCooldown = 0f;

	public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
	{
		base.DoAnimationTriggerEventLogic(triggerType);
	}

	public override void DoEnterLogic()
	{
		base.DoEnterLogic();

		_targetPos = GetRandomPointInCircle(itembase.transform.position, RandomWanderRadius);
	}

	public override void DoExitLogic()
	{
		base.DoExitLogic();
	}

	public override void DoFixedUpdateLogic()
	{
		base.DoFixedUpdateLogic();
	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();


		// Calculate the direction from current position to target position on the X-Z plane
		// (x z)平面上での現在の位置から目標位置への方向を計算する
		_direction = (_targetPos - transform.position).normalized;
		_direction.y = 0;

		// Move the Item
		// アイテムを移動する
		itembase.MoveItem(_direction * RandomWanderSpeed);

		// Check if close to or at the target position
		// 目標位置に近づいたか、到達したかを確認する
		Vector3 diff = itembase.transform.position - _targetPos;
		diff.y = 0;


		if (diff.sqrMagnitude < 0.01f)
		{
			// Set a new random target position within the X-Z plane
			// (x-z) 平面上での新しいランダムな目標位置を設定する
			_targetPos = GetRandomPointInCircle(itembase.transform.position, RandomWanderRadius);
			_targetPos.y = itembase.transform.position.y;
		}

		if (collisionCooldown > 0f)
		{
			collisionCooldown -= Time.deltaTime;
		}

		if (itembase.IsAggroed && collisionCooldown <= 0f)
		{
			// Calculate the new target position based on the reversed direction
			// 逆転した方向に基づいて新しい目標位置を計算する
			_targetPos = itembase.transform.position - _direction * Random.Range(1f, RandomWanderRadius);
			_targetPos.y = itembase.transform.position.y;

			collisionCooldown = 1f;
		}

	}

	public override void Initialize(GameObject gameObject, ItemBase itembase)
	{
		base.Initialize(gameObject, itembase);
	}

	public override void ResetValues()
	{
		base.ResetValues();
	}

	/// <summary>
	/// Functions for getting a random point within a circle
	/// </summary>
	/// <param name="origin"></param>
	/// <param name="radius"></param>
	/// <returns></returns>

	private Vector3 GetRandomPointInCircle(Vector3 origin, float radius)
	{
		// Get a random point within a circle centered at 'origin' with radius 'radius'
		// 半径「radius」で「origin」を中心にした円内のランダムな点を取得する
		Vector2 randomOffset = Random.insideUnitCircle * radius;
		return new Vector3(origin.x + randomOffset.x, origin.y, origin.z + randomOffset.y);
	}


}