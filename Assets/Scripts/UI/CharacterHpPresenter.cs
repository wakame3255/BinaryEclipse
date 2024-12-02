using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterHpPresenter : MonoBehaviour
{
    public CharacterHpPresenter(HunterHpFactory hunterHpFactory, BossHpFactory bossHpFactory, CharacterDictionary characterDictionary)
    {
        foreach (BaseCharacter character in characterDictionary.AllAllys)
        {
            BaseHpView baseHpView = hunterHpFactory.GenerateHpSlider();
            character.CharacterStatus.ReactiveHp.Subscribe(hp => baseHpView.UpdateSlider(hp));
        }

        foreach (BaseCharacter character in characterDictionary.AllBosses)
        {
            BaseHpView baseHpView = bossHpFactory.GenerateHpSlider();
            character.CharacterStatus.ReactiveHp.Subscribe(hp => baseHpView.UpdateSlider(hp));
        }
    }
}
