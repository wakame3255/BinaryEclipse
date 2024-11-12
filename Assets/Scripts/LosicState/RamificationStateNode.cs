using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamificationStateNode : BaseStateNode
{
    [SerializeField]
    private OutNode _falseNode;

    bool _changeTrg = default;
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (_changeTrg)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
            _changeTrg = false;
        }
        else
        {
            _cpuCharacter.StateMachine.TransitionNextState(_falseNode.NextStateNode);
            _changeTrg = true;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
