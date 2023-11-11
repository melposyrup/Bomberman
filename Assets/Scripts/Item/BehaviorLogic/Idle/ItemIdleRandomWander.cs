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
	private float gravityForceMagnitude = 4f;

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
		itembase.Rigidbody.AddForce(Vector3.down * gravityForceMagnitude);
	}

	public override void DoUpdateLogic()
	{
		base.DoUpdateLogic();

		_direction = (_targetPos - transform.position).normalized;
		_direction.y = 0;

		itembase.MoveItem(_direction * RandomWanderSpeed);

		Vector3 diff = itembase.transform.position - _targetPos;
		diff.y = 0;


		if (diff.sqrMagnitude < 0.01f)
		{
			_targetPos = GetRandomPointInCircle(itembase.transform.position, RandomWanderRadius);
			_targetPos.y = itembase.transform.position.y;
		}

		if (collisionCooldown > 0f)
		{
			collisionCooldown -= Time.deltaTime;
		}

		if (itembase.IsAggroed && collisionCooldown <= 0f)
		{
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
		Vector2 randomOffset = Random.insideUnitCircle * radius;
		return new Vector3(origin.x + randomOffset.x, origin.y, origin.z + randomOffset.y);
	}


}