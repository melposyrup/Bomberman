using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemOnHandMoveWithPlayer", menuName = "ItemLogic/OnHandLogic/ItemOnHandMoveWithPlayer")]

public class ItemOnHandMoveWithPlayer : ItemOnHandSOBase
{
    Bomb bomb => itembase as Bomb;

    public override void DoAnimationTriggerEventLogic(ItemBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        itembase.Rigidbody.isKinematic = false;
        if (bomb)
        {
            bomb.gameObject.layer = LayerMask.NameToLayer("InitialBomb");
            bomb.IsCounting = false;
        }
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

        if (bomb)
        {
            //go with player
            Vector3 posXZ = bomb.IsHoldedBy.position
                + bomb.IsHoldedBy.forward.normalized
                * (bomb.GetComponent<SphereCollider>().radius 
                * bomb.transform.localScale.magnitude
                + bomb.Owner.GetComponent<BoxCollider>().size.z / 2f);

            float posY = bomb.IsHoldedBy.position.y;

            if (bomb.Owner.TryGetComponent<BoxCollider>(out BoxCollider box))
            { posY += box.center.y; }

            posY += bomb.transform.localScale.magnitude
                * bomb.GetComponent<SphereCollider>().radius;

            itembase.transform.position =
                new Vector3(posXZ.x, posY, posXZ.z);
        }

        if (itembase.IsThrow)
        {
            //if(bomb.IsThrow) changeState to ThrowByPlayer, return;
            itembase.IsThrow = false;
            itembase.StateMachine.ChangeState(itembase.OnThrowState);
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
}
