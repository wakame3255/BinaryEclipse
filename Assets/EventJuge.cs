using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class EventJuge : MonoBehaviour
{
    [SerializeField]
    private SceneAsset _clearScene;

    [SerializeField]
    private SceneAsset _overScene;

    private PlayerCharacter _playerCharacter;
    private List<AllyCharacter> _allyCharacters;
    private List<BossCharacter> _enemyCharacters;

    public void SetInformation(ObjectDictionary objectDictionary)
    {
        _playerCharacter = objectDictionary.GetHasComponent<PlayerCharacter>()[0];
        _allyCharacters = objectDictionary.GetHasComponent<AllyCharacter>();
        _enemyCharacters = objectDictionary.GetHasComponent<BossCharacter>();
    }

    public void JugeEvent()
    {
        OverEvent();
        ClearEvent();
    }

    private void ClearEvent()
    {
        foreach (BossCharacter bossCharacter in _enemyCharacters)
        {
            if (bossCharacter.CharacterStatusView.Hp > 0)
            {
                return;
            }
        }
        SceneManager.LoadScene(_clearScene.name);
    }

    private void OverEvent()
    {
        foreach (AllyCharacter allyCharacter in _allyCharacters)
        {
            if (allyCharacter.CharacterStatusView.Hp > 0)
            {
                return;
            }
            if (_playerCharacter.CharacterStatusView.Hp > 0)
            {
                return;
            }
        }
        SceneManager.LoadScene(_overScene.name);
    }
}
