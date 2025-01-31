
using UnityEngine;

public interface ICharacterAction
{
    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public void SetControlInformation(ICharacterController inputManager);


    /// <summary>
    /// リソース情報をすべてのアクションに渡すメソッド
    /// </summary>
    /// <param name="characterState">ステータス</param>
    /// <param name="bulletFactorys">攻撃の種類</param>
    public void SetResourceInformation(CharacterStateView characterState, BaseBulletFactory[] bulletFactorys);
}
