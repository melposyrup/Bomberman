using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: �������त�������ե������`���ܥࡢ�ɥ������ǥӥ�E��䡩��IdleState�ˁE����I������E

/* ������Bomb
 * �त������Red Bomb
 * �ե������`��Fire
 * �ܥࣺBombUP
 * �ɥ�����Skull
 * �ǥӥ�E�Devil
 */

public class ItemBase : MonoBehaviour, IItemMoveable, ITriggerCheckable, IItemThrowable
{
    #region IItemThrowable implementation
    public bool IsThrow { get; set; }
    public void SetThrowStatus(bool isThrow) { IsThrow = isThrow; }

    public Transform IsThrownBy { get; set; }
    public void SetThrownBy(Transform transform) { IsThrownBy = transform; }
    #endregion

    #region IItemMoveable implementation
    public Rigidbody Rigidbody { get; set; }

    public void MoveItem(Vector3 velocity) { Rigidbody.velocity = velocity; }
    #endregion

    #region ITriggerCheckable implementation

    [SerializeField] private bool isAggroed;

    public bool IsAggroed
    {
        get { return isAggroed; }
        set { isAggroed = value; }
    }

    public void SetAggroStatus(bool isAggroed) { IsAggroed = isAggroed; }
    #endregion

    #region State Machine Variables

    public ItemStateMachine StateMachine { get; set; }
    public ItemIdleState IdleState { get; set; }
    public ItemOnThrowState OnThrowState { get; set; }


    #endregion

    #region ScriptableObject Variables
    [SerializeField] private ItemIdleSOBase ItemIdleBase;
    [SerializeField] private ItemOnThrowSOBase ItemOnThrowBase;
    public ItemIdleSOBase ItemIdleBaseInstance { get; set; }
    public ItemOnThrowSOBase ItemOnThrowBaseInstance { get; set; }
    #endregion

    #region Aniamtion Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        //Debug.Log("Animation Trigger: " + triggerType);
        StateMachine.CurrentItemState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        Idle,
        //Walk,
        //Run,
        //Jump,
        //Attack,
        //Death
    }

    #endregion

    protected virtual void Awake()
    {
        ItemIdleBaseInstance = Instantiate(ItemIdleBase);
        ItemOnThrowBaseInstance = Instantiate(ItemOnThrowBase);

        StateMachine = new ItemStateMachine();

        IdleState = new ItemIdleState(this, StateMachine);
        OnThrowState = new ItemOnThrowState(this, StateMachine);
    }

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        ItemIdleBaseInstance.Initialize(gameObject, this);
        ItemOnThrowBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);

    }

    protected virtual void Update()
    {
        StateMachine.CurrentItemState.UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentItemState.FixedUpdateState();
    }


}
