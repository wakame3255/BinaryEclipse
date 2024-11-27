using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterFactory : MonoBehaviour
{
    [SerializeField][Header("生成する数")]
    private int _generateCharacterCount;

    public abstract void GenerateCharacter();
}
