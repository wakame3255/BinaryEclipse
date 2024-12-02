using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpFactory : BaseHpUIFactory
{
    private BossHpView _cacheSlider;

    public BossHpFactory(BossHpView slider)
    {
        _cacheSlider = slider;
    }

    public override BaseHpView GenerateHpSlider(Transform canvasObj, int hp)
    {
        _cacheSlider.SetMaxValue(hp);
        return Instantiate(_cacheSlider, canvasObj);
    }
}
