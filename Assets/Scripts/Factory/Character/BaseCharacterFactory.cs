using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterFactory : MonoBehaviour
{
    [SerializeField][Header("�������鐔")]
    private int _generateCharacterCount;

    public abstract void GenerateCharacter();
}
