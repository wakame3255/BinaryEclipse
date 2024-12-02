using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterHpFactory : BaseHpUIFactory
{
    private HunterHpView _cacheSlider;

    public HunterHpFactory(HunterHpView slider)
    {
        _cacheSlider = slider;
    }

    public override BaseHpView GenerateHpSlider(Transform canvasObj, int hp)
    {
        _cacheSlider.SetMaxValue(hp);
        return Instantiate(_cacheSlider, canvasObj);
    }
}
