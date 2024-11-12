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
           
        }
        else
        {
            _cpuCharacter.StateMachine.TransitionNextState(_falseNode.NextStateNode);
        }
    }
    public override void ExitState()
    {
        //_changeTrg = true;
        base.ExitState();
    }
}
