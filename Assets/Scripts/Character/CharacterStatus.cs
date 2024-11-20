using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private int _hp;

    private ReactiveProperty<int> _reactiveHp = new ReactiveProperty<int>();

    public ReactiveProperty<int> ReactiveHp { get => _reactiveHp; }

    private void Awake()
    {
        _reactiveHp.Value = _hp;
    }

    public void SubtractionHp(int damageAmount)
    {
        _reactiveHp.Value -= damageAmount;
        _hp -= damageAmount;
    }

    public void AddHp(int recoveryAmount)
    {
        _reactiveHp.Value += recoveryAmount;
        _hp += recoveryAmount;
    }
}
