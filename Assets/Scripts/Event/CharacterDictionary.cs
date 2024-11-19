using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OtherCharacterStatus
{
    public OtherCharacterStatus(List<CharacterStateView> allys, List<CharacterStateView> enemys)
    {
        _allys = allys;
        _enemys = enemys;
    }

    [SerializeField]
    private List<CharacterStateView> _allys = new List<CharacterStateView>();
    [SerializeField]
    private List<CharacterStateView> _enemys = new List<CharacterStateView>();

    public List<CharacterStateView> Allys { get => _allys; }
    public List<CharacterStateView> Enemys { get => _enemys; }
}

public class CharacterDictionary : MonoBehaviour
{
    private List<BaseCharacter> _allAllys;
    private List<BaseCharacter> _allBosses;
    private List<ICpuCharacter> _allCpuCharacters;

    public List<BaseCharacter> AllAllys {get => _allAllys; }
    public List<BaseCharacter> AllBosses { get => _allBosses; }
    public List<ICpuCharacter> AllCpuCharacters { get => _allCpuCharacters; }

    /// <summary>
    /// �L�����N�^�[�ɏ���^���郁�\�b�h
    /// </summary>
    /// <param name="objectDictionary">�I�u�W�F�N�g����</param>
    public void SetCharacterState(ObjectDictionary objectDictionary)
    {
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));

        List<BaseCharacter> baseCharacters = objectDictionary.GetHasComponent<BaseCharacter>();

        _allBosses = objectDictionary.GetHasSubClass(baseCharacters, typeof(BossCharacter));
        _allAllys = objectDictionary.GetHasSubClass(baseCharacters, typeof(PlayerCharacter));
        _allAllys.AddRange(objectDictionary.GetHasSubClass(baseCharacters, typeof(AllyCharacter)));

        _allCpuCharacters = objectDictionary.GetHasComponent<ICpuCharacter>();
    }

    /// <summary>
    /// �w�肵���R���|�[�l���g�����f�B�N�V���i���[��Ԃ����\�b�h
    /// </summary>
    /// <typeparam name="T">�l�ƂȂ�R���|�[�l���g</typeparam>
    /// <param name="gameObjects">���ׂẴI�u�W�F�N�g</param>
    /// <returns>�w��̗v�f���������f�B�N�V���i���[</returns>
    private Dictionary<GameObject, T> GetDictionary<T>(IEnumerable<GameObject> gameObjects)
    {
        MyExtensionClass.CheckArgumentNull(gameObjects, nameof(gameObjects));

        Dictionary<GameObject, T> saveDictionary = new Dictionary<GameObject, T>();

        foreach (GameObject gameObject in gameObjects)
        {
            T component;

            if (gameObject.TryGetComponent<T>(out component))

                saveDictionary.Add(gameObject, component);
        }

        return saveDictionary;
    }
}
