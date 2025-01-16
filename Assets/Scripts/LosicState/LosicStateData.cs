using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LosicStateData", fileName = "LosicStateData")]
public class LosicStateData : ScriptableObject
{
    [SerializeField]
    private List<BaseStateNode> _baseStates;

    public List<BaseStateNode> BaseStates { get => _baseStates; }
}

