using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatBeatCont : MonoBehaviour
{
    [SerializeField]
    private AudioSource heatBeatSource;

    [SerializeField]
    private float _startSoundTime;

    [SerializeField]
    private float _startSoundUpTime;

    private float _currentTime;
    private float _currentTimes;
    private int _currentCount;

    private BeatState _currentState;

    private enum BeatState
    {
        NoSound,
        MiniSound,
        SoundUp
    }
    private void Start()
    {
        heatBeatSource.volume = 0;
    }

    private void Update()
    {
        _currentTimes += Time.deltaTime;
        SetNowMotherTime(_currentTimes);
    }

    public void SetNowMotherTime(float nowTime)
    {
        switch (_currentState)
        {
            case BeatState.NoSound:
                DoNoSoundState(nowTime);
                break;

            case BeatState.MiniSound:
                DoMiniSoundState(nowTime);
                break;

            case BeatState.SoundUp:
                DoSoundUpState(nowTime);
                break;
        }
    }

    private void DoNoSoundState(float nowTime)
    {
        if (nowTime > _startSoundTime)
        {
            heatBeatSource.volume = 0.1f;
            _currentState = BeatState.MiniSound;
        }
    }

    private void DoMiniSoundState(float nowTime)
    {
        if (nowTime > _startSoundUpTime)
        {
            _currentState = BeatState.SoundUp;
        }
    }

    private void DoSoundUpState(float nowTime)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _currentCount)
        {
            _currentCount++;
            heatBeatSource.volume += 0.3f;
        }
        if (_currentCount > 3)
        {           
            _currentState = BeatState.NoSound;
            heatBeatSource.volume = 0;
            _currentCount = 0;
            _currentTimes = 0f;
            _currentTime = 0f;
        }
    }
}
