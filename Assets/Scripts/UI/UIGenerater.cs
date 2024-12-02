using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class UIGenerater : MonoBehaviour
{
    [SerializeField, Required]
    private HunterHpView _hunterHpView;
    [SerializeField, Required]
    private BossHpView _bossHpView;
    [SerializeField, Required]
    private Transform _sceneObject;



    private HunterHpFactory _hunterHpFacory;
    private BossHpFactory _bossHpFactory;
    private CharacterHpPresenter _hpPresenter;

    private void Awake()
    {
        SetComponent();
    }

    public void UIGenerate(CharacterDictionary characterDictionary)
    {
        _hpPresenter = new(_hunterHpFacory, _bossHpFactory, characterDictionary, _sceneObject);
    }

    private void SetComponent()
    {
        _hunterHpFacory = new(_hunterHpView);
        _bossHpFactory = new(_bossHpView);
    }
}
