using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalMoveState : BaseStateNode
{
    [SerializeField]
    Vector3 _targetDropdown;

    private Vector3 _targetPos;

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (Vector3.Distance(_targetPos, _cpuCharacter.Transform.position) <= 1)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
        else
        {
            Vector3 targetDirection = (_targetPos - _cpuCharacter.Transform.position).normalized;
            _cpuController.SetInputMove(targetDirection.x, targetDirection.y);
        }
    }
    public override void ExitState()
    {
        _cpuController.SetInputMove(0, 0);
        base.ExitState();
    }

    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode();

        return new OutNode[] { _outNode };
    }
}
