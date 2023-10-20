using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ItemBase, IItemKickable, IItemHoldable
{
    #region IItemKickable implementation
    public bool IsKick { get; set; } = false;
    public void SetKickStatus(bool isKick)
    {
        IsKick = isKick;
    }
    public Transform IsKickedBy { get; set; }
    public void SetKickedBy(Transform transform)
    { IsKickedBy = transform; }

    #endregion

    #region IItemHoldable implementation
    public bool IsOnHold { get; set; }
    public void SetOnHoldStatus(bool isHolding)
    {
        IsOnHold = isHolding;
    }
    #endregion

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

    protected override void Awake()
    {
        base.Awake();
        ItemOnKickBaseInstance = Instantiate(ItemOnKickBase);
        ItemOnHandBaseInstance = Instantiate(ItemOnHandBase);

        OnKickState = new ItemOnKickState(this, base.StateMachine);
        OnHandState = new ItemOnHandState(this, base.StateMachine);

    }

    protected override void Start()
    {
        base.Start();

        ItemOnKickBaseInstance.Initialize(gameObject, this);
        ItemOnHandBaseInstance.Initialize(gameObject, this);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //Debug.Log("bomb state:  " + base.StateMachine.CurrentItemState);
    }


    //when player get out of the trigger,set trigger to false
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Bomb");
        }
    }


}
