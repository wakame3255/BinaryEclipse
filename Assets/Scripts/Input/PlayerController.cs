
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
    private bool _isSkill;
    private bool _isMenu;

    private ReactiveProperty<Transform> _reactiveTargetTransform = new ReactiveProperty<Transform>();

    public static PlayerController Instance { get => _instance; }
    public float InputX { get => _inputX; }
    public float InputY { get => _inputY; }
    public Vector3 Direction { get => _mousePosition; }
    public Transform Target { get => _targetPosition; }
    public bool IsAttack { get => _k_key; }
    public bool IsSkill { get => _isSkill; }
    public bool IsMenu { get => _isMenu; }

    public ReactiveProperty<Transform> ReactiveTargetTransform { get => _reactiveTargetTransform; }
    private void Awake()
    {
        //�C���X�^���X�����݂��Ȃ��ꍇ�A���g���C���X�^���X�ɂ���
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogError(transform.root.name + "�����̃C���X�^���X�����݂��܂�");
        }

        _targetPosition = transform;
    }

    private void Update()
    {
        CheckKeyBoardDevice();
        SetMousePosition();
        SetButton();
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
        //�}�E�X�̍��W���擾����
        Vector3 mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�����
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        _mousePosition = pos;
    }

    private void SetMoveInfomation()
    {
        _inputX = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
        _inputY = Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0;
    }

    private void SetButton()
    {
        _k_key = Mouse.current.leftButton.isPressed;
        _isSkill = Mouse.current.rightButton.isPressed;
        _isMenu = Keyboard.current.escapeKey.wasPressedThisFrame;
    }
}
