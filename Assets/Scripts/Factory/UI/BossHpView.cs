using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel.DataAnnotations;
using UnityEngine.UI;

public class BossHpView : BaseHpView
{
    [SerializeField, Required]
    private Slider _slider;

    public override void UpdateSlider(int hp)
    {
        _slider.value = hp;
    }
    public override void SetMaxValue(int hp)
    {
        _slider.maxValue = hp;
    }
}
