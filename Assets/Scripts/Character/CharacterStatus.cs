using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private int _hp = default;

    private ReactiveProperty<int> _reactiveHp;

    public ReactiveProperty<int> ReactiveHp { get => _reactiveHp; }


    public CharacterStatus(int hp)
    {
        print("ステータス生成");
        _reactiveHp = new ReactiveProperty<int>();
        _hp = hp;
        _reactiveHp.Value = _hp;
    }

    public void SubtractionHp(int damageAmount)
    {
        _reactiveHp.Value -= damageAmount;
        _hp -= damageAmount;
        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddHp(int recoveryAmount)
    {
        _reactiveHp.Value += recoveryAmount;
        _hp += recoveryAmount;
    }
}
