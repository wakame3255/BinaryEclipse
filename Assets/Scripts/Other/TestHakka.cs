using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class TestHakka : MonoBehaviour
{
    private float _currentTime;

    [SerializeField]
    private float _apearanceTime;
    [SerializeField]
    private float _removeTime;

    private bool _isApearanceMother;
    private void Update()
    {
        SendMotheMove();
    }

    private void SendMotheMove()
    {
        _currentTime += Time.deltaTime;

        if (_isApearanceMother)
        {
            if (CheckMotherState(_removeTime))
            {
                ReMother();
            }
        }
        else
        {
            if (CheckMotherState(_apearanceTime))
            {
                ApMother();
            }
        }
    }

    private bool CheckMotherState(float eventTime)
    {
        if (_currentTime > eventTime)
        {
            _currentTime = 0f;
            return true;
        }
        return false;
    }

    private void ApMother()
    {     
        _isApearanceMother = true;
    }

    private void ReMother()
    {
        _isApearanceMother = false;
    }
}
