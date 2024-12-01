using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackStateNode : BaseStateNode
{
    [SerializeField]
    Dropdown _targetDropdown;

    public override void EnterState()
    {
        _cpuController.SetTargetPosition(GetTarget(_targetDropdown));
        _cpuController.SetAttack(true);
        base.EnterState();
    }
    public override void UpdateState()
    {
        _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
    }
    public override void ExitState()
    {
        _cpuController.SetAttack(false);
        base.ExitState();
    }

    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode();

        return new OutNode[] { _outNode };
    }

    private Transform GetTarget(Dropdown targetName)
    {
        switch(targetName.value)
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
