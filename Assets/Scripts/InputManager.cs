using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static float _inputX;
    private static float _inputY;

    public static float InputX { get => _inputX; }
    public static float InputY { get => _inputY; }

    private void Update()
    {
        CheckKeyBoardDevice();
    }

    private void CheckKeyBoardDevice()
    {
        if (Keyboard.current != null)
        {
            SetInputInfomation();
        }
    }

    private void SetInputInfomation()
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
