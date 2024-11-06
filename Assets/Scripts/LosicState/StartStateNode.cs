using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class StartStateNode : BaseStateNode
{
    List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();

    public List<BaseStateNode> UpDateStateNodeFlow()
    {
        if (_outNode != null)
        {
            _baseStateNodes.Clear();

            _baseStateNodes.Add(this);
            CheckNextState(_outNode);
        }
      
        return _baseStateNodes;
    }

    /// <summary>
    /// �X�e�[�g�̘A�Ȃ���ċA�I�Ɋm�F
    /// </summary>
    /// <param name="outNode"></param>
    private void CheckNextState(OutNode outNode)
    {

        if (outNode.NextNodeState != null)
        {
            _baseStateNodes.Add(outNode.NextNodeState);
            CheckNextState(outNode.NextNodeState.OutNode);
        }
    }
}
