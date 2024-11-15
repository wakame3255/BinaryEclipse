using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterStatus : MonoBehaviour
{
    private int _hp;

    private ReactiveProperty<int> _reactiveHp = new ReactiveProperty<int>();

    public ReactiveProperty<int> ReactiveHp { get => _reactiveHp; }

    private void Awake()
    {
        _reactiveHp.Value = _hp;
    }

    public void SubtractionHp(int damage)
    {
        _reactiveHp.Value -= damage;
    }
}
