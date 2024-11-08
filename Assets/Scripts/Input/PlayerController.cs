using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ICharacterController
{
    private static float _inputX;
    private static float _inputY;

    private static Vector3 _mousePosition;

    private static bool _k_key;

    public float InputX { get => _inputX; }
    public float InputY { get => _inputY; }

    public Vector3 Direction { get => _mousePosition; }

    public bool IsAttack { get => _k_key; }

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
        //�}�E�X�̍��W���擾����
        Vector3 mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�����
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        _mousePosition = pos;
    }

    private void SetMoveInfomation()
    {
        float inputX;
        float inputY;

        if (Keyboard.current.wKey.isPressed)
        {
            inputY = 1 ;
        }
        else if(Keyboard.current.sKey.isPressed)
        {
            inputY = -1;
        }
        else
        {
            inputY = 0;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            inputX = -1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            inputX = 1;
        }
        else
        {
            inputX = 0;
        }
        _inputX = inputX;
        _inputY = inputY;
    }
}
