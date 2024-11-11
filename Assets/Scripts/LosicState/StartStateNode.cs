using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class StartStateNode : BaseStateNode
{

    public override void EnterState()
    {
        if (_outNode.NextNodeState != null)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextNodeState);
        }
    }
    public override void UpdateState() { }
    public override void ExitState() { }
}
