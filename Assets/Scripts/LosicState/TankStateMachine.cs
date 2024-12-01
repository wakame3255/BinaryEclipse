using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStateMachine : MonoBehaviour
{
    [SerializeField]
    private StartStateNode _startStateNode;

    [SerializeField]
    private GameObject _canvas;

    public StartStateNode StartStateNode { get => _startStateNode; }

    public void CreateStateMachine()
    {
        TankStateMachine tankState = Instantiate(this, _canvas.transform);
        tankState.gameObject.SetActive(true);
        DontDestroyOnLoad(_canvas);
    }
}
