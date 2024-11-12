using UnityEngine;

public class RamificationStateNode : BaseStateNode
{
    [SerializeField]
    private OutNode _falseNode;

    [SerializeField]
    private bool _changeTrg = default;
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

    public override OutNode[] ReturnHasOutNode()
    {
        _outNode.UpdateNextNode();
        _falseNode.UpdateNextNode();

        return new OutNode[] { _outNode, _falseNode };
    }
}
