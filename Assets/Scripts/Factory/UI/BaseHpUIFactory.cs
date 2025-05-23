using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseHpUIFactory : MonoBehaviour
{

    public abstract BaseHpView GenerateHpSlider(Transform canvasObj, int hp);
}
