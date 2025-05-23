using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class MoveStateNode : BaseStateNode

{
    [SerializeField]
    Dropdown _targetDropdown;

    private Vector3 _targetPos;

    public override void EnterState()
    {
        _targetPos = GetTarget(_targetDropdown).position;
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

    private Transform GetTarget(Dropdown targetName)
    {
        switch (targetName.value)
        {
            case 0:
                return _otherCharacters.Enemys[0].CharacterTransform;
            case 1:
                int random = Random.Range(0, _otherCharacters.Allys.Count);
                return _otherCharacters.Allys[random].CharacterTransform;
        }
        return null;
    }
}
