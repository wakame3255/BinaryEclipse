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
    private protected void SetCharacterInformation(CharacterInformation resource, BaseCharacter baseCharacter, Component character)
    {
        MyExtensionClass.CheckArgumentNull(resource, nameof(resource));
        MyExtensionClass.CheckArgumentNull(baseCharacter, nameof(baseCharacter));
        MyExtensionClass.CheckArgumentNull(character, nameof(character));

        Debug.LogWarning("引数コンパクトにする");

        if (character is ICharacterAction action)
        {
            action.SetResourceInformation(baseCharacter.CharacterStatusView, resource.BulletFactorys);
        }
    }
}
