
using UnityEngine;

interface ICharacterAction
{
    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public void SetControlInformation(ICharacterController inputManager, Transform myTransform);
}
