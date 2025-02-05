using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ステートマシンに必要な情報を保持するクラス
/// </summary>
public class StateMachineInformation
{
    public ICpuCharacter CpuCharacterDate { get; private set; }
    public CpuController CpuControllerDate { get; private set; }
    public StartStateNode StartStateNodeDate { get; private set; }
    public OtherCharacterStatus OtherCharacterStatusDate { get; private set; }

    /// <summary>
    /// コンストラクタで各情報を初期化
    /// </summary>
    /// <param name="cpuCharacter">CPUキャラクターのデータ</param>
    /// <param name="cpuController">CPUコントローラー</param>
    /// <param name="startNode">開始ステートノード</param>
    /// <param name="otherCharacterStatus">他キャラクターのステータス</param>
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

        private StateMachineInformation _stateMachineInformation;

        /// <summary>
        /// 現在のステートノードを取得
        /// </summary>
        public BaseStateNode CurrentStateNode { get; private set; }

        /// <summary>
        /// ステートマシンの初期化処理
        /// </summary>
        public void Initialize()
        {
            // ステートノードの更新を行う
            UpdateStateNode();
            // 開始ステートノードを現在のステートに設定
            CurrentStateNode = _startStateNode;
            if (CurrentStateNode != null)
            {
                // 開始ステートに入る
                CurrentStateNode.EnterState();
            }
        }

        /// <summary>
        /// 次のステートへの遷移処理
        /// </summary>
        /// <param name="nextStateNode">遷移先のステートノード</param>
        public void TransitionNextState(BaseStateNode nextStateNode)
        {
            BaseStateNode baseStateNode;

            // 次のステートが指定されていない場合、ベースステートリストの最初を使用
            if (nextStateNode == null)
            {
                baseStateNode = _baseStateNodes[0];
            }
            else
            {
                baseStateNode = nextStateNode;
            }

            // 現在のステートから退出
            CurrentStateNode.ExitState();
            // 新しいステートに切り替え
            CurrentStateNode = baseStateNode;
            // 新しいステートに入る
            CurrentStateNode.EnterState();
        }

        /// <summary>
        /// ステートの更新処理を実行
        /// </summary>
        public void UpdateState()
        {
            if (CurrentStateNode != null)
            {
                // 現在のステートの更新処理を呼び出す
                CurrentStateNode.UpdateState();
            }
        }

        /// <summary>
        /// ステートマシンのコンストラクタ
        /// </summary>
        /// <param name="stateMachineInformation">ステートマシンに必要なキャラクター情報</param>
        public StateMachine(StateMachineInformation stateMachineInformation)
        {
            // 引数がnullでないことを確認
            MyExtensionClass.CheckArgumentNull(stateMachineInformation, nameof(stateMachineInformation));

            // 各フィールドを初期化
            _cpuCharacter = stateMachineInformation.CpuCharacterDate;
            _cpuController = stateMachineInformation.CpuControllerDate;
            _startStateNode = stateMachineInformation.StartStateNodeDate;
            _otherCharacterStatus = stateMachineInformation.OtherCharacterStatusDate;

            if (_startStateNode != null)
            {
                // 開始ステートノードにキャラクター情報とコントローラーを設定
                _startStateNode.SetCharacterInformation(_cpuCharacter);
                _startStateNode.SetCpuContoller(_cpuController);
            }
        }

        /// <summary>
        /// ステートのフローを更新する
        /// </summary>
        public void UpdateStateNode()
        {
            if (CurrentStateNode != null)
            {
                // 現在のステートから退出
                CurrentStateNode.ExitState();
            }

            // ステートノードのフローをリセットおよび更新
            UpDateStateNodeFlow();

            // 各ベースステートノードにキャラクター情報を設定
            foreach (BaseStateNode baseState in _baseStateNodes)
            {
                baseState.SetCharacterInformation(_cpuCharacter);
                baseState.SetOtherCharacterInformation(_otherCharacterStatus);
                baseState.SetCpuContoller(_cpuController);
            }
        }

        /// <summary>
        /// ステートノードのフローをリセットし、再構築するメソッド
        /// </summary>
        private void UpDateStateNodeFlow()
        {
            if (_startStateNode != null)
            {
                // ベースステートノードリストをクリア
                _baseStateNodes.Clear();
                // 開始ステートノードから出力ノードを取得
                _startStateNode.GetHasOutNode();
                OutNode[] outNodes = new OutNode[] { _startStateNode.OutNode };
                // 再帰的に次のステートをキャッシュ
                CacheNextState(outNodes);
            }
        }

        /// <summary>
        /// ステートの連鎖を再帰的に確認し、キャッシュに追加するメソッド
        /// </summary>
        /// <param name="outNodes">現在のステートからの出力ノード配列</param>
        private void CacheNextState(OutNode[] outNodes)
        {
            // 引数がnullでないことを確認
            MyExtensionClass.CheckArgumentNull(outNodes, nameof(outNodes));

            foreach (OutNode outNode in outNodes)
            {
                // 次のノードを更新
                outNode.UpdateNextNode();
                BaseStateNode nextStateNode = outNode.NextStateNode;

                // 次のステートノードが有効で、ループしていない場合
                if (nextStateNode != null && !CheckStateLoop(_baseStateNodes, nextStateNode))
                {
                    // ベースステートノードリストに追加
                    _baseStateNodes.Add(nextStateNode);

                    // 再帰的に次のステートノードをキャッシュ
                    CacheNextState(nextStateNode.GetHasOutNode());
                }
            }
        }

        /// <summary>
        /// ステートのループをチェックするメソッド
        /// </summary>
        /// <param name="cacheBaseStates">既存のキャッシュされたステートノードリスト</param>
        /// <param name="baseState">追加予定のステートノード</param>
        /// <returns>ループが検出された場合はtrue、それ以外はfalse</returns>
        private bool CheckStateLoop(List<BaseStateNode> cacheBaseStates, BaseStateNode baseState)
        {
            foreach (BaseStateNode stateNode in cacheBaseStates)
            {
                if (stateNode == baseState)
                {
                    // ループが検出された場合
                    return true;
                }
            }

            return false;
        }
    }
}
