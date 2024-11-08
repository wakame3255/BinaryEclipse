using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationMove))]
[RequireComponent(typeof(PlayerController))]
public class PlayerCharacter : BaseCharacter
{
    private RotationMove _rotationMove;
    private PlayerController _playerInput;

    protected override void SetComponent()
    {
        _rotationMove = CheckComponentMissing<RotationMove>();
        _playerInput = CheckComponentMissing<PlayerController>();
        base.SetComponent();
    }

    public override void PhysicsUpDate()
    {
        _characterAction.SetInput(_playerInput);
        _rotationMove.DoRotationMove(_playerInput.Direction);
        base.PhysicsUpDate();
    }
}
