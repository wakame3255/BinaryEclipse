using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationMove))]

public class PlayerCharacter : BaseCharacter
{
    private RotationMove _rotationMove;
    
    protected override void SetComponent()
    {
        _rotationMove = this.CheckComponentMissing<RotationMove>();
        base.SetComponent();
    }

    public override void PhysicsUpDate()
    {
        _characterAction.SetControlInformation(PlayerController.Instance);
        _rotationMove.DoRotationMove(PlayerController.Instance.Direction);
        base.PhysicsUpDate();
    }

    protected override void DeliveryValue()
    {
        _characterStateView.SetComponent(_characterStatus, this.transform, PlayerController.Instance);
    }
}
