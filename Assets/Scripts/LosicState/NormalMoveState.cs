using UnityEngine;

public class NormalMoveState : BaseStateNode
{
    private Vector2 _targetDropdown = new Vector3(10, 8);
    private Vector3 _wayPointPosition;

    public override void EnterState()
    {
        float randomPositionX = Random.Range(-_targetDropdown.x, _targetDropdown.x);
        float randomPositionY = Random.Range(-_targetDropdown.y, _targetDropdown.y);

        _wayPointPosition = new Vector3(randomPositionX, randomPositionY, 0);
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (Vector3.Distance(_wayPointPosition, _cpuCharacter.Transform.position) <= 1)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
        else
        {
            Vector3 targetDirection = (_wayPointPosition - _cpuCharacter.Transform.position).normalized;
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
