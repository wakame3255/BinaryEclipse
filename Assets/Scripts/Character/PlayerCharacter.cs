using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationMove))]
public class PlayerCharacter : BaseCharacter
{
    [SerializeField]
    BaseCharacterAction _characterAction;
    RotationMove _rotationMove; 

    protected override void SetComponent()
    {
        _rotationMove = CheckComponentMissing<RotationMove>();
        base.SetComponent();
    }

    public override void PhysicsUpDate()
    {
        _rotationMove.DoRotationMove(InputManager.MousePosition);
        base.PhysicsUpDate();
    }
}
