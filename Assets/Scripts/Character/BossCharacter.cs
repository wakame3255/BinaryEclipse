using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CpuController))]
[RequireComponent(typeof(RotationMove))]

public class BossCharacter : BaseCharacter, ICpuCharacter
{
    
    [SerializeField] [Header("スタートノード")]
    private StartStateNode _startStateNode;
    
    [SerializeField]
    private Cpu.StateMachine _stateMachine;
    private CpuController _cpuController;
    private RotationMove _rotationMove;

    public StartStateNode StartStateNode { get => _startStateNode; }
    public Cpu.StateMachine StateMachine { get => _stateMachine; }
    public Transform Transform { get => _cacheTransform; }
    public CpuController CpuController { get => _cpuController; }

    public override void PhysicsUpDate()
    {
        _stateMachine.UpdateState();
        _characterAction.SetControlInformation(_cpuController);
        _rotationMove.DoRotationMove(_cpuController.Target);
        base.PhysicsUpDate();
    }

    public void SetStateMachine(Cpu.StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    protected override void SetComponent()
    {
        _cpuController = this.CheckComponentMissing<CpuController>();
        _rotationMove = this.CheckComponentMissing<RotationMove>();
        base.SetComponent();
    }

    protected override void DeliveryValue()
    {
        _characterStateView.SetComponent(_characterStatus, this.transform, _cpuController);
    }
}
