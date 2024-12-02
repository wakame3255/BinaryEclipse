using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CharacterHpPresenter : MonoBehaviour
{
    const int HP_Y_POSITION = 50;
    const int HP_X_POSITION = 25;

    public CharacterHpPresenter(HunterHpFactory hunterHpFactory, BossHpFactory bossHpFactory, CharacterDictionary characterDictionary, Transform canvas)
    {
        int positionY = 0;
        int positionX = 0;
        foreach (BaseCharacter character in characterDictionary.AllAllys)
        {
            BaseHpView baseHpView = hunterHpFactory.GenerateHpSlider(canvas, character.CharacterStatusView.Hp);
            character.CharacterStatus.ReactiveHp.Subscribe(hp => baseHpView.UpdateSlider(hp));

            baseHpView.gameObject.transform.position += Vector3.down * positionY;
            baseHpView.gameObject.transform.position += Vector3.right * positionX;
            positionY += HP_Y_POSITION;
            positionX += HP_X_POSITION;
        }                                    

        foreach (BaseCharacter character in characterDictionary.AllBosses)
        {
            BaseHpView baseHpView = bossHpFactory.GenerateHpSlider(canvas, character.CharacterStatusView.Hp);
            character.CharacterStatus.ReactiveHp.Subscribe(hp => baseHpView.UpdateSlider(hp));
        }
    }
}
