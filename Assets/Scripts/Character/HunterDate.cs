
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
    private int _attack;//攻撃力

    [Required][SerializeField]
    [InterfaceField(typeof(ICharacterAction))]
    private Component _hunterPrefab; //使う武器のプレハブ
    
    [SerializeField, Required]
    private BaseBulletFactory[] _bullets; //使う武器のプレハブ

    public int ID { get => _id; }
    public int Hp { get => _hp; }
    public int Attack { get => _attack; }
    public Component HunterPrefab { get => _hunterPrefab; }
    public BaseBulletFactory[] Bullets { get => _bullets; }
}

[CreateAssetMenu(menuName = "ScriptableObject/Character Setting", fileName = "CharacterDate")]
public class HunterDate : ScriptableObject
{
    [SerializeField]
    private List<HunterInformation> _hunterInformation;

    public List<HunterInformation> HunterInformation { get => _hunterInformation; }
}