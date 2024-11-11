using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class MoveStateNode : BaseStateNode

{
    [SerializeField]
    private Vector3 _targetPos;

    public override void EnterState() { }
    public override void UpdateState()
    {
        if (Vector3.Distance(_targetPos, _cpuCharacter.transform.position) <= 1)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextNodeState);    
        }
        else
        {
            Vector3 targetDirection = (_targetPos - _cpuCharacter.transform.position).normalized;
            _cpuController.SetInputMove(targetDirection.x, targetDirection.y);
        }      
    }
    public override void ExitState()
    {
        _cpuController.SetInputMove(0, 0);
    }
    
}
