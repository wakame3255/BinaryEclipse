
using UnityEngine;
using UnityEngine.InputSystem;
using R3;

public class PlayerController : MonoBehaviour, ICharacterController
{
    private static PlayerController _instance;

    private float _inputX;
    private float _inputY;
    private Vector3 _mousePosition;
    private Transform _targetPosition;
    private bool _k_key;

    private ReactiveProperty<Transform> _reactiveTargetTransform = new ReactiveProperty<Transform>();

    public static PlayerController Instance { get => _instance; }
    public float InputX { get => _inputX; }
    public float InputY { get => _inputY; }
    public Vector3 Direction { get => _mousePosition; }
    public Transform Target { get => _targetPosition; }
    public bool IsAttack { get => _k_key; }

    public ReactiveProperty<Transform> ReactiveTargetTransform { get => _reactiveTargetTransform; }
    private void Awake()
    {
        //インスタンスが存在しない場合、自身をインスタンスにする
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogError(transform.root.name + "複数のインスタンスが存在します");
        }
    }

    private void Update()
    {
        CheckKeyBoardDevice();
        SetMousePosition();
    }

    private void CheckKeyBoardDevice()
    {
        if (Keyboard.current != null)
        {
            SetMoveInfomation();
        }
    }

    private void SetMousePosition()
    {
        //マウスの座標を取得する
        Vector3 mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換する
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        _mousePosition = pos;
    }

    private void SetMoveInfomation()
    {
        _inputX = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
        _inputY = Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0;
    }
}
