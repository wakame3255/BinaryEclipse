using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : BaseCharacter, ICpuCharacter
{
    [SerializeField] [Header("スタートノード")]
    private StartStateNode _startStateNode;
    
    private StateMachine _stateMachine;
    private CpuController _cpuController;

    public StateMachine StateMachine { get => _stateMachine; }
    public Transform Transform { get => _cacheTransform; }

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

    public void InitializeStateMachine(OtherCharacterStatus characterState)
    {
        //インスタンスの生成
        _stateMachine = new StateMachine(this, _cpuController, _startStateNode, characterState);
    }

    protected override void SetComponent()
    {
        _cpuController = CheckComponentMissing<CpuController>();
        base.SetComponent();
    }
}
