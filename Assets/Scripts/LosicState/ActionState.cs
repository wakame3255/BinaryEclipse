using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public enum Status
{
    Hp,
    Damage,
    Defence
}

public enum HighLow
{
    High,
    Low
}
public enum Targets
{
    Ally,
    Enemy
}
public enum Actions
{
    Attack,
    Skill,
    Move
}
public class ActionState : BaseStateNode
{
    [SerializeField]
    private Dropdown _statusDrop;

    [SerializeField]
    private Dropdown _higiLowDown;

    [SerializeField]
    private Dropdown _targetDown;

    [SerializeField]
    private Dropdown _actionDown;

    private CharacterStateView _cacheCharacter;
    private Vector3 _movePosition;
    private bool _isMove;
    private int PUSH_HOLD_TIME = 1;

    public override void EnterState()
    {

        List<CharacterStateView> characterState = GetTargetList(_targetDown);
        _cacheCharacter = GetTarget(characterState);
        switch (_actionDown.value)
        {
            case 0:
                _cpuController.SetTargetPosition(_cacheCharacter.CharacterTransform);
                StartPushAttackAsync();
                break;
            case 1:
                _cpuController.SetTargetPosition(_cacheCharacter.CharacterTransform);
                StartPushSkillAsync();
                break;
            case 2:
                _movePosition = _cacheCharacter.CharacterTransform.position;
                _isMove = true;
                break;
        }
        base.EnterState();
    }

    public override void UpdateState()
    {
        if (_isMove)
        {
            DoMove();
        }
    }

    public override void ExitState()
    {
        _cpuController.SetInputMove(0, 0);
        _cpuController.SetAttack(false);
        _cpuController.SetSkill(false);
        base.ExitState();
    }

    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode();
        return new OutNode[] { _outNode };
    }

    private List<CharacterStateView> GetTargetList(Dropdown targetName)
    {
        if (targetName == null)
        {
           
        }

        switch (targetName.value)
        {
            case 0:
                return _otherCharacters.Enemys;
            case 1:
                return _otherCharacters.Allys;
            default:
                return null;
        }
    }

    private CharacterStateView GetTarget(List<CharacterStateView> characterStates)
    {
        if (characterStates == null || characterStates.Count == 0)
        {
            return null;
        }

        List<int> vs = new List<int>();
        foreach (CharacterStateView characterStatus in characterStates)
        {
            switch (_statusDrop.value)
            {
                case 0:
                    vs.Add(characterStatus.Hp);
                    break;
                case 1:
                    vs.Add(characterStatus.Hp);
                    break;
                case 2:
                    vs.Add(characterStatus.Hp);
                    break;
            }
        }

        int index = 0;
        switch (_higiLowDown.value)
        {
            case 0:
                index = GetMaxValueIndex(vs);
                break;
            case 1:
                index = GetMinValueIndex(vs);
                break;
            case 2:
                index = Random.Range(0, characterStates.Count);
                break;
        }
        return characterStates[index];
    }

    private int GetMinValueIndex(List<int> list)
    {
        if (list == null || list.Count == 0)
        {
            return 0;
        }
        int minValue = list[0];
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] < minValue)
            {
                minValue = list[i];
                index = i;
            }
        }
        return index;
    }

    private int GetMaxValueIndex(List<int> list)
    {
        if (list == null || list.Count == 0)
        {
            return 0;
        }
        int maxValue = list[0];
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] > maxValue)
            {
                maxValue = list[i];
                index = i;
            }
        }
        return index;
    }
    private async void StartPushAttackAsync()
    {
        _cpuController.SetAttack(true);
        await Task.Delay(PUSH_HOLD_TIME * 100);
        _cpuController.SetAttack(false);
        _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
    }
    private async void StartPushSkillAsync()
    {
        _cpuController.SetSkill(true);
        await Task.Delay(PUSH_HOLD_TIME * 100);
        _cpuController.SetSkill(false);
        _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
    }

    private void DoMove()
    {
        if (Vector3.Distance(_movePosition, _cpuCharacter.Transform.position) <= 1)
        {
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
        else
        {
            Vector3 targetDirection = (_movePosition - _cpuCharacter.Transform.position).normalized;
            _cpuController.SetInputMove(targetDirection.x, targetDirection.y);
        }
    }
}