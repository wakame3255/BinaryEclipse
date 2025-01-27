using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class AvoidanceState : BaseStateNode
{
    [SerializeField]
    private LayerMask _walllayerMask;
    [SerializeField]
    private LayerMask _ammoLayerMask;
    [SerializeField]
    private InputField _inputSecond;

    private bool _isAvoidance = default;
    private bool _oneTimeConstract = default;

    private AvoidanceDirectionCheck _directionCheck;

    public override void EnterState()
    {
        if (!_oneTimeConstract)
        {
            _directionCheck = new AvoidanceDirectionCheck(_walllayerMask, _ammoLayerMask, _cpuCharacter.Transform);
            _oneTimeConstract = true;
        }

        StartAvoidanceAsync();
        base.EnterState();
    }

    public override void UpdateState()
    {
        if (_isAvoidance)
        {
            Vector2 direction = _directionCheck.ReturnMoveDirection();
            _cpuController.SetInputMove(direction.x, direction.y);
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

    private async void StartAvoidanceAsync()
    {
        _isAvoidance = true;
         await Task.Delay(ConvertTextToInt(_inputSecond.text) * 1000);
        _isAvoidance = false;
        _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
    }

    /// <summary>
    /// テキストのint変換
    /// </summary>
    /// <param name="inputText">テキスト</param>
    /// <returns>変換された数値</returns>
    private int  ConvertTextToInt(string inputText)
    {
        if (int.TryParse(inputText, out int resultInt ))
        {
            return resultInt;
        }
        else
        {
            return 0;
        }
    }
}
