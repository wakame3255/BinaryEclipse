using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGenerater : MonoBehaviour
{
    private HunterHpFactory _hunterHpFacory;
    private BossHpFactory _bossHpFactory;

    private void Awake()
    {
        SetComponent();
    }

    public void UIGenerate(CharacterDictionary characterDictionary)
    {

    }

    private void SetComponent()
    {
        _hunterHpFacory = new(new HunterHpView());
        _bossHpFactory = new(new BossHpView());
       
    }
}
