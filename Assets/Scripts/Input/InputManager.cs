using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static float _inputX;
    private static float _inputY;

    private static Vector3 _mousePosition;

    private static bool _k_key;

    public static float InputX { get => _inputX; }
    public static float InputY { get => _inputY; }

    public static Vector3 MousePosition { get => _mousePosition; }

    public static bool K_key { get => _k_key; }

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
