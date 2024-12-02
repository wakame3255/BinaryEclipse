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

    public override void GenerateHpSlider()
    {
        Instantiate(_cacheSlider);
    }
}
