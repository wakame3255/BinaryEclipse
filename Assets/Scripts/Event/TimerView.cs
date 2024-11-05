using UnityEngine;
using UnityEngine.UI;
using R3;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private TimeCounter _timeCounter;

    [SerializeField]
    private Text counterText;

    private void Start()
    {
        _timeCounter.OnTimeChanged.Subscribe(time =>
        {
            counterText.text = time.ToString();
        });
    }
}
