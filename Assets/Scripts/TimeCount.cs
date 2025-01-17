using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    [SerializeField]
    private string _timeText;

    [SerializeField]
    private float _limitTime;

    private float _currentTime;

    private bool _isTimeOver = default;

    public bool IsTimeOver { get => _isTimeOver; }

    private void Start()
    {
        UpDateTime(0);
    }

    private void Update()
    {
        if (!_isTimeOver)
        {
            CountTime();
        }   
    }

    private void CountTime()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > 1f)
        {
            _currentTime = 0f;
            UpDateTime(1);
        }
    }

    private void UpDateTime(int removeCount)
    {
        _limitTime -= removeCount;

        string nowMinutsText = (_limitTime / 60).ToString("00");
        string nowSecondText = (_limitTime % 60).ToString("00");

        SetTimeInformation(nowMinutsText, nowSecondText);
    }

    private void SetTimeInformation(string minutes, string second)
    {
        _timeText = minutes + " : " + second;
    }

    private void CheckTime()
    {
        if (_limitTime <= 0)
        {
            _isTimeOver = true;
        }
    }
}
