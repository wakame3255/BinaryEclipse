using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCheckStateNode : BaseStateNode
{
    [SerializeField]
    private OutNode _falseNode;

    [SerializeField]
    private int _hp;

    private bool _existsCharacter = default;
    public override void EnterState()
    {
        foreach (CharacterStateView characterState in _otherCharacters.Allys)
        {
            if (characterState.Hp <= _hp)
            {
                _existsCharacter = true;
                break;
            }
            else
            {
                _existsCharacter = false;
            }
        }
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (_existsCharacter)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
        else
        {
            _cpuCharacter.StateMachine.TransitionNextState(_falseNode.NextStateNode);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }

    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode();
        _falseNode.UpdateNextNode();

        return new OutNode[] { _outNode, _falseNode };
    }
}
