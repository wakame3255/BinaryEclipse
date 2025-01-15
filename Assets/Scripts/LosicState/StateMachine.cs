using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachineInformation
{   
    public ICpuCharacter CpuCharacterDate { get; private set; }
    public CpuController CpuControllerDate { get; private set; }
    public StartStateNode StartStateNodeDate { get; private set; }
    public OtherCharacterStatus OtherCharacterStatusDate { get; private set; }

    public StateMachineInformation(ICpuCharacter cpuCharacter, CpuController cpuController, StartStateNode startNode, OtherCharacterStatus otherCharacterStatus)
    {
        CpuCharacterDate = cpuCharacter;
        CpuControllerDate = cpuController;
        StartStateNodeDate = startNode;
        OtherCharacterStatusDate = otherCharacterStatus;
    }
}

namespace Cpu
  {
    [Serializable]
    public class StateMachine
    {
        [SerializeField]
        private List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();

        private ICpuCharacter _cpuCharacter;
        private CpuController _cpuController;
        private StartStateNode _startStateNode;
        [SerializeField]
        private OtherCharacterStatus _otherCharacterStatus;

        public BaseStateNode CurrentStateNode { get; private set; }

        /// <summary>
        /// ステートの開始処理
        /// </summary>
        /// <param name="baseState">ステート</param>
        public void Initialize()
        {
            UpdateStateNode();
            CurrentStateNode = _startStateNode;
            if (CurrentStateNode != null)
            {
                CurrentStateNode.EnterState();
            }
        }

        /// <summary>
        /// ステートの切り替え処理
        /// </summary>
        /// <param name="nextStateNode">ステート</param>
        public void TransitionNextState(BaseStateNode nextStateNode)
        {
            BaseStateNode baseStateNode;

            if (nextStateNode == null)
            {
                baseStateNode = _baseStateNodes[0];
            }
            else
            {
                baseStateNode = nextStateNode;
            }
            CurrentStateNode.ExitState();
            CurrentStateNode = baseStateNode;
            CurrentStateNode.EnterState();
        }

        /// <summary>
        /// ステートのUpdate処理
        /// </summary>
        public void UpdateState()
        {
            if (CurrentStateNode != null)
            {
                CurrentStateNode.UpdateState();
            }
        }

        /// <summary>
        /// ステートマシンの生成
        /// </summary>
        /// <param name="stateMachineInformation">キャラクター情報</param>
        public StateMachine(StateMachineInformation stateMachineInformation)
        {
            MyExtensionClass.CheckArgumentNull(stateMachineInformation, nameof(stateMachineInformation));

            _cpuCharacter = stateMachineInformation.CpuCharacterDate;
            _cpuController = stateMachineInformation.CpuControllerDate;
            _startStateNode = stateMachineInformation.StartStateNodeDate;
            _otherCharacterStatus = stateMachineInformation.OtherCharacterStatusDate;

            if (_startStateNode != null)
            {
                _startStateNode.SetCharacterInformation(_cpuCharacter);
                _startStateNode.SetCpuContoller(_cpuController);
            }
           
        }

        /// <summary>
        /// ステートの流れ更新
        /// </summary>
        public void UpdateStateNode()
        {
            if (CurrentStateNode != null)
            {
                CurrentStateNode.ExitState();
            }

            UpDateStateNodeFlow();

            foreach (BaseStateNode baseState in _baseStateNodes)
            {
                baseState.SetCharacterInformation(_cpuCharacter);
                baseState.SetOtherCharacterInformation(_otherCharacterStatus);
                baseState.SetCpuContoller(_cpuController);
            }
        }

        /// <summary>
        /// ステートリセットと更新メソッド
        /// </summary>
        private void UpDateStateNodeFlow()
        {
            if (_startStateNode != null)
            {
                _baseStateNodes.Clear();
                _startStateNode.GetHasOutNode();
                OutNode[] outNodes = new OutNode[] { _startStateNode.OutNode };
                CacheNextState(outNodes);
            }
        }

        /// <summary>
        /// ステートの連なりを再帰的に確認
        /// </summary>
        /// <param name="outNode"></param>
        private void CacheNextState(OutNode[] outNodes)
        {
            MyExtensionClass.CheckArgumentNull(outNodes, nameof(outNodes));

            foreach (OutNode outNode in outNodes)
            {
                outNode.UpdateNextNode();
                BaseStateNode nextStateNode = outNode.NextStateNode;

                if (nextStateNode != null && !CheckStateLoop(_baseStateNodes, nextStateNode))
                {
                    _baseStateNodes.Add(nextStateNode);

                    CacheNextState(nextStateNode.GetHasOutNode());
                }
            }
        }

        /// <summary>
        /// ステートのループチェック
        /// </summary>
        /// <param name="cacheBaseStates">キャッシュステート群</param>
        /// <param name="baseState">追加予定のステート</param>
        /// <returns>ループしているかのBool</returns>
        private bool CheckStateLoop(List<BaseStateNode> cacheBaseStates, BaseStateNode baseState)
        {
            foreach (BaseStateNode stateNode in cacheBaseStates)
            {
                if (stateNode == baseState)
                {
                    Debug.Log("ループ検知");
                    return true;
                }
            }

            return false;
        }
    }
}
