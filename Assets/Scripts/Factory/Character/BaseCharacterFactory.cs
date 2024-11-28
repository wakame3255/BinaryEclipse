using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterFactory : MonoBehaviour
{
    private protected List<CharacterInformation> _characterInformation;

    /// <summary>
    /// �L�����N�^�[�𐶐����郁�\�b�h
    /// </summary>
    public abstract void GenerateCharacter();


    /// <summary>
    /// �L�����N�^�[�ɏ���^���郁�\�b�h
    /// </summary>
    /// <param name="characterAction"></param>
    private protected void SetCharacterInformation(Component characterAction, CharacterInformation resource, BaseCharacter baseCharacter)
    {
        Debug.LogWarning("�����R���p�N�g�ɂ���");

        MyExtensionClass.CheckArgumentNull(characterAction, nameof(characterAction));
        MyExtensionClass.CheckArgumentNull(resource, nameof(resource));

        if (characterAction is ICharacterAction action)
        {
            action.SetResourceInformation(baseCharacter.CharacterStatusView, )
        }
    }
}
