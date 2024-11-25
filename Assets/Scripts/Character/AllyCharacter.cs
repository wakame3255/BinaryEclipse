using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationMove))]
[RequireComponent(typeof(CpuController))]
public class AllyCharacter : BaseCharacter, ICpuCharacter
{
    [SerializeField][Header("スタートノード")]
    private StartStateNode _startStateNode;
    [SerializeField]
    private Cpu.StateMachine _stateMachine;

    private RotationMove _rotationMove;
    private CpuController _cpuController;

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

    public void UpdateStateMachine()
    {
        _stateMachine.UpdateStateNode();
        _stateMachine.Initialize(_startStateNode);
    }

    public void SetStateMachine(Cpu.StateMachine stateMachine)
    {
        MyExtensionClass.CheckArgumentNull(stateMachine, nameof(stateMachine));
        _stateMachine = stateMachine;
    }

    protected override void SetComponent()
    {
        _rotationMove = this.CheckComponentMissing<RotationMove>();
        _cpuController = this.CheckComponentMissing<CpuController>();
        base.SetComponent();
    }

    protected override void DeliveryValue()
    {
        _characterStateView.SetComponent(_characterStatus, this.transform, _cpuController);
    }
}
