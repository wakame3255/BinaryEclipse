using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : BaseCharacter, ICpuCharacter
{
    [SerializeField] [Header("スタートノード")]
    private StartStateNode _startStateNode;
    
    [SerializeField]
    private StateMachine _stateMachine;
    private CpuController _cpuController;

    public StartStateNode StartStateNode { get => _startStateNode; }
    public StateMachine StateMachine { get => _stateMachine; }
    public Transform Transform { get => _cacheTransform; }
    public CpuController CpuController { get => _cpuController; }

    public override void PhysicsUpDate()
    {
        _stateMachine.UpdateState();
        _characterAction.SetInput(_cpuController);
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
        _cpuController = CheckComponentMissing<CpuController>();
        base.SetComponent();
    }

    protected override void DeliveryValue()
    {
        _characterStateView.SetComponent(_characterStatus, this.transform, _cpuController);
    }
}
