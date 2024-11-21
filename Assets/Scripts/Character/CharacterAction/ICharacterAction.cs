
using UnityEngine;

interface ICharacterAction
{
    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public void SetInput(ICharacterController inputManager);

    /// <summary>
    /// 値を代入するtransform
    /// </summary>
    /// <param name="characterTransform">キャラクターのトランスフォーム</param>
    public void SetTransform(Transform characterTransform);
}
