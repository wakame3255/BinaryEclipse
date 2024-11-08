using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationMove))]
[RequireComponent(typeof(CpuController))]
public class CpuCharacter : BaseCharacter
{
    [SerializeField]
    private StateMachine _stateMachine;

    private RotationMove _rotationMove;
    private CpuController _cpuController;

    public StateMachine StateMachine { get => _stateMachine; }

    protected override void SetComponent()
    {
        _rotationMove = CheckComponentMissing<RotationMove>();
        _cpuController = CheckComponentMissing<CpuController>();
        base.SetComponent();
    }

    public override void PhysicsUpDate()
    {
        _characterAction.SetInput(_cpuController);
        _rotationMove.DoRotationMove(_cpuController.Direction);
        base.PhysicsUpDate();
    }
}
