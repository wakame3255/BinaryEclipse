using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HPチェック用のステートノードクラス
/// </summary>
public class HpCheckStateNode : BaseStateNode
{
    [SerializeField]
    private OutNode _falseNode; // HPチェックが失敗した場合の遷移先ノード

    [SerializeField]
    private int _hp; // チェック対象のHP値

    private bool _existsCharacter = default; // 指定されたHP以下のキャラクターが存在するかどうかのフラグ

    /// <summary>
    /// ステートに入る際の処理
    /// </summary>
    public override void EnterState()
    {
        // 同盟キャラクターをループしてHPが指定値以下かを確認
        foreach (CharacterStateView characterState in _otherCharacters.Allys)
        {
            if (characterState.Hp <= _hp)
            {
                _existsCharacter = true;
                break; // 条件を満たすキャラクターが見つかったためループを抜ける
            }
            else
            {
                _existsCharacter = false;
            }
        }
        base.EnterState(); // 基底クラスのEnterStateを呼び出す
    }

    /// <summary>
    /// ステートの更新処理
    /// </summary>
    public override void UpdateState()
    {
        if (_existsCharacter)
        {
            // 条件を満たすキャラクターが存在する場合、通常の遷移先に遷移
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
        else
        {
            // 条件を満たすキャラクターが存在しない場合、別の遷移先に遷移
            _cpuCharacter.StateMachine.TransitionNextState(_falseNode.NextStateNode);
        }
    }

    /// <summary>
    /// ステートから退出する際の処理
    /// </summary>
    public override void ExitState()
    {
        base.ExitState(); // 基底クラスのExitStateを呼び出す
    }

    /// <summary>
    /// このステートノードが持つ出力ノードを取得
    /// </summary>
    /// <returns>出力ノードの配列</returns>
    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode(); // 通常の出力ノードを更新
        _falseNode.UpdateNextNode(); // 失敗時の出力ノードを更新

        return new OutNode[] { _outNode, _falseNode }; // 出力ノードを配列で返す
    }
}
