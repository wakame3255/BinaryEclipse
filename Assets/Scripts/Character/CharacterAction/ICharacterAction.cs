
using UnityEngine;

public interface ICharacterAction
{
    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public void SetControlInformation(ICharacterController inputManager);

    public void SetResourceInformation(CharacterStateView characterState);
}
