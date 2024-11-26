
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using System;


[Serializable]
public class HunterInformation
{
    [SerializeField]
    [UnityEngine.Range(0, 100)]
    private int _id;

    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _attack;//�U����

    [Required][SerializeField]
    [InterfaceField(typeof(ICharacterAction))]
    private Component _hunterPrefab; //�g������̃v���n�u
    
    [SerializeField, Required]
    private BaseBulletFactory[] _bullets; //�g������̃v���n�u
}

[CreateAssetMenu(menuName = "ScriptableObject/Character Setting", fileName = "CharacterDate")]
public class HunterDate : ScriptableObject
{
    [SerializeField]
    private List<HunterInformation> _hunterInformation;
}