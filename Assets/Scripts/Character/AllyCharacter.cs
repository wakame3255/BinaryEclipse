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
    private StateMachine _stateMachine;

    private RotationMove _rotationMove;
    private CpuController _cpuController;


    public StateMachine StateMachine { get => _stateMachine; }
    public Transform Transform { get => _cacheTransform; }

    public override void PhysicsUpDate()
    {
        _stateMachine.UpdateState();
        _characterAction.SetInput(_cpuController);
        _rotationMove.DoRotationMove(_cpuController.Target);
        base.PhysicsUpDate();
    }

    public void UpdateStateMachine()
    {
        _stateMachine.UpdateStateNode();
        _stateMachine.Initialize(_startStateNode);
    }

    public void SetStateMachine(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    protected override void SetComponent()
    {
        _rotationMove = CheckComponentMissing<RotationMove>();
        _cpuController = CheckComponentMissing<CpuController>();
        base.SetComponent();
    }

    protected override void DeliveryValue()
    {
        _characterStateView.SetComponent(_characterStatus, this.transform, _cpuController);
    }
}
