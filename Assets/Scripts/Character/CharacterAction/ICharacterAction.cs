
using UnityEngine;

interface ICharacterAction
{
    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public void SetInput(ICharacterController inputManager, Transform myTransform);
}
