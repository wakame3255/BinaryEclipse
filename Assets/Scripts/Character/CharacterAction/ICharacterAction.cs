using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharacterAction
{
    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public void SetInput(ICharacterController inputManager);
}
