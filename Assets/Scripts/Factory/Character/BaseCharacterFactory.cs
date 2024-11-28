using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterFactory : MonoBehaviour
{
    private protected List<CharacterInformation> _characterInformation;

    /// <summary>
    /// キャラクターを生成するメソッド
    /// </summary>
    public abstract void GenerateCharacter();


    /// <summary>
    /// キャラクターに情報を与えるメソッド
    /// </summary>
    /// <param name="characterAction"></param>
    private protected void SetCharacterInformation(Component characterAction, CharacterInformation resource, BaseCharacter baseCharacter)
    {
        Debug.LogWarning("引数コンパクトにする");

        MyExtensionClass.CheckArgumentNull(characterAction, nameof(characterAction));
        MyExtensionClass.CheckArgumentNull(resource, nameof(resource));

        if (characterAction is ICharacterAction action)
        {
            action.SetResourceInformation(baseCharacter.CharacterStatusView, )
        }
    }
}
