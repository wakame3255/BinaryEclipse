using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class TimeCounter : MonoBehaviour
{
    private Subject<int> _timerSubject = new Subject<int>();

    public Observable<int> OnTimeChanged
    {
        get { return _timerSubject; }
    }

    private void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        int time = 100;
        while(time > 0)
        {
            time--;

            _timerSubject.OnNext(time);

            yield return new WaitForSeconds(1);
        }
    }
}
