using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class EventJuge : MonoBehaviour
{
    [SerializeField]
    private ScenesNames _clearScene;

    [SerializeField]
    private ScenesNames _overScene;

    private PlayerCharacter _playerCharacter;
    private List<AllyCharacter> _allyCharacters;
    private List<BossCharacter> _enemyCharacters;

    private EnemyStateMachine _enemyStateMachine;
    private TankStateMachine _tankStateMachine;
    private HealStateMachine _healStateMachine;

    public void SetInformation(ObjectDictionary objectDictionary)
    {
        _playerCharacter = objectDictionary.GetHasComponent<PlayerCharacter>()[0];
        _allyCharacters = objectDictionary.GetHasComponent<AllyCharacter>();
        _enemyCharacters = objectDictionary.GetHasComponent<BossCharacter>();

        _enemyStateMachine = objectDictionary.GetHasComponent<EnemyStateMachine>()[0];
        _tankStateMachine = objectDictionary.GetHasComponent<TankStateMachine>()[0];
        _healStateMachine = objectDictionary.GetHasComponent<HealStateMachine>()[0];
    }

    public void JugeEvent()
    {
        if (CheckHasScene(_overScene))
        {
            OverEvent();
        }

        if (CheckHasScene(_clearScene))
        {
            ClearEvent();
        }     
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
        DestroyState();
        SceneManager.LoadScene(_clearScene.ToString());
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
        DestroyState();
        SceneManager.LoadScene(_overScene.ToString());
    }

    private bool CheckHasScene(ScenesNames scene)
    {
        if (scene == ScenesNames.none)
        {
            return false;
        }
        return true;
    }

    private void DestroyState()
    {
        Destroy(_enemyStateMachine.gameObject);
        Destroy(_tankStateMachine.gameObject);
        Destroy(_healStateMachine.gameObject);
    }
}
