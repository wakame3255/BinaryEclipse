using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStateMachine : MonoBehaviour
{
    [SerializeField]
    private StartStateNode _startStateNode;

    [SerializeField]
    private GameObject _canvas;

    public StartStateNode StartStateNode { get => _startStateNode; }

    public void CreateStateMachine()
    {
        HealStateMachine healState = Instantiate(this, _canvas.transform);
        healState.gameObject.SetActive(true);
        DontDestroyOnLoad(_canvas);
    }
}
