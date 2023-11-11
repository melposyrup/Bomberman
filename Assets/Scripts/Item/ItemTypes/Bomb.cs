using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ItemBase, IItemKickable, IItemHoldable, IBombExpandable, IBombExplodable
{
	public GameObject Owner;


	#region IBombExpandable implementation
	public Vector3 MaxScale { get; set; }
	public float ExpandFactor { get; set; } = 1.1f;
	public float MaxExpandFactor { get; set; } = 2f;

	public void Expand()
	{
		if (transform.localScale.magnitude < MaxScale.magnitude)
			transform.localScale *= ExpandFactor;
	}

	#endregion

	#region IItemKickable implementation
	public bool IsKick { get; set; } = false;
	public void SetKickStatus(bool isKick)
	{ IsKick = isKick; }
	public Transform IsKickedBy { get; set; }
	public void SetKickedBy(Transform transform)
	{ IsKickedBy = transform; }

	#endregion

	#region IItemHoldable implementation
	public bool IsOnHold { get; set; }
	public void SetOnHoldStatus(bool isHolding)
	{ IsOnHold = isHolding; }
	public Transform IsHoldedBy { get; set; }
	public void SetHoldedBy(Transform transform)
	{
		IsHoldedBy = transform;
		Owner = IsHoldedBy.gameObject;
	}

	#endregion

	#region IBombExplodable implementation
	public bool IsExplode { get; set; }
	public void SetExplodeStatus(bool isExplode)
	{ IsExplode = isExplode; }

	public Transform IsPlacedBy { get; set; }
	public void SetPlacedBy(Transform player)
	{
		IsPlacedBy = player;
		Owner = IsPlacedBy.gameObject;
	}
	public float IsExplodeTimer { get; set; } = 3.0f;
	public void SetExplodeTimer(float explodeTimer)
	{ IsExplodeTimer = explodeTimer; }
	public bool IsCounting { get; set; } = false;
	public void SetCounting(bool isCounting)
	{
		IsCounting = isCounting; //Debug.Log("IsCounting : " + IsCounting);
	}
	#endregion
	const float MAX_EXPLODE_TIMER = 3.0f;


	#region State Machine Variables
	public ItemOnKickState OnKickState { get; set; }
	public ItemOnHandState OnHandState { get; set; }
	#endregion

	#region ScriptableObject Variables
	[SerializeField] private ItemOnKickSOBase ItemOnKickBase;
	[SerializeField] private ItemOnHandSOBase ItemOnHandBase;
	public ItemOnKickSOBase ItemOnKickBaseInstance { get; set; }
	public ItemOnHandSOBase ItemOnHandBaseInstance { get; set; }

	#endregion


	[Header("AnimationEffects")]
	public GameObject Explosion;

	public SphereCollider SphereCollider;

	protected override void Awake()
	{
		base.Awake();
		ItemOnKickBaseInstance = Instantiate(ItemOnKickBase);
		ItemOnHandBaseInstance = Instantiate(ItemOnHandBase);

		OnKickState = new ItemOnKickState(this, base.StateMachine);
		OnHandState = new ItemOnHandState(this, base.StateMachine);

		SphereCollider = GetComponent<SphereCollider>();
	}

	protected override void Start()
	{
		base.Start();

		// IBombExpandable
		Vector3 InitialScale = transform.localScale;
		MaxScale = new Vector3(
			InitialScale.x * MaxExpandFactor,
			InitialScale.y * MaxExpandFactor,
			InitialScale.z * MaxExpandFactor);
		// ScriptableObject Variables
		ItemOnKickBaseInstance.Initialize(gameObject, this);
		ItemOnHandBaseInstance.Initialize(gameObject, this);

		// Create and initialize BombMaterialInstance

		CreateBombMaterialInstance();

	}

	protected override void Update()
	{
		base.Update();

		CountdownToExplosion();
	}

	#region EmissionEffect
	public Material BombMaterialInstance;
	public Color EmissionColor1;
	public Color EmissionColor2;

	private void CreateBombMaterialInstance()
	{
		Renderer renderer = GetComponentInChildren<MeshRenderer>();
		BombMaterialInstance = new Material(renderer.material);
		renderer.material = BombMaterialInstance;
		BombMaterialInstance.EnableKeyword("_EMISSION");
		BombMaterialInstance.color = Color.black;
	}

	private void CountdownToExplosion()
	{

		if (IsCounting)
		{
			IsExplodeTimer -= Time.deltaTime;

			if (IsExplodeTimer < 2f)
			{
				PingPongEmissionColor();
			}
			if (IsExplodeTimer < 0)
			{
				Death();
			}
		}
		else
		{
			BombMaterialInstance.SetColor("_EmissionColor", Color.black);
			IsExplodeTimer = 3.0f;
		}

	}

	void PingPongEmissionColor()
	{
		float pingPongDuration = 0.4f;
		float lerp = Mathf.PingPong(Time.time, pingPongDuration) / pingPongDuration;

		Color emissionColor = Color.Lerp(EmissionColor1, EmissionColor2, lerp);

		BombMaterialInstance.SetColor("_EmissionColor", emissionColor);
	}

	#endregion

	public Color bombFireMax;
	public void BombRed(int? level = 0)//fire
	{
		int maxLevel = 10;
		if (level != null && level > 0 && level < maxLevel)
		{
			float factor = (float)level / (maxLevel-1);
			Color color = bombFireMax * factor;
			IsExplodeTimer = MAX_EXPLODE_TIMER - factor;
		}
	}
	public void BombPowerUP(int? level = 0)//power
	{
		int maxLevel = 10;
		if (level != null && level > 0 && level < maxLevel)
		{
			float bombPowerMax = 2f;
			float bombPower = 1f + (float)level / (maxLevel - 1) * bombPowerMax;
			this.transform.localScale *= bombPower;
		}
	}




	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		//Debug.Log("bomb state:  " + base.StateMachine.CurrentItemState);
	}

	public void Death()
	{
		if (Owner && Owner.TryGetComponent(out Player player))
		{
			player.BombCountRecover();
		}

		//playerObject.func();
		if (Explosion)
		{
			GameObject explosion =
				Instantiate(Explosion, transform.position, Quaternion.identity);
			explosion.GetComponent<Explosion>().SetScale(transform.localScale.magnitude);
		}
		else { Debug.Log("Explosion Prefab undefined"); }

		Destroy(gameObject);
	}
}
