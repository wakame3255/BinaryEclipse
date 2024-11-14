using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private int _hp;

    private Subject<int> _hpSubject = new Subject<int>();

    public int Hp { get => _hp;}

    public void SubtractionHp(int damage)
    {
        _hp -= damage;
    }
}
