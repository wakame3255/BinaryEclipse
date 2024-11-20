using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class StartStateNode : BaseStateNode
{

    public override void EnterState()
    {
        if (_outNode.NextStateNode != null)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
    }
    public override void UpdateState() { }
    public override void ExitState() { }

    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode();

        return new OutNode[] { _outNode };
    }
}
