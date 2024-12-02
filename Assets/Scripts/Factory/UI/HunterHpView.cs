using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel.DataAnnotations;
using UnityEngine.UI;

public class HunterHpView : MonoBehaviour
{
    [SerializeField, Required]
    private Slider _slider;

    public void UpdateSlider(int hp)
    {
        _slider.value = hp;
    }
}
