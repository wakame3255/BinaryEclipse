using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private int _hp;

    public int Hp { get => _hp;}

    public void SubtractionHp(int damage)
    {
        _hp -= damage;
    }
}
