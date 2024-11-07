using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StateMachine
{
    [SerializeField]
    StartStateNode _startStateNode;

    [SerializeField]
    List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();

    public BaseStateNode CurrentStateNode { get; private set; }
   
    public StateMachine()
    {

    }
}
