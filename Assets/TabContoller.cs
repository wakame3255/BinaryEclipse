using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabContoller : MonoBehaviour
{
    //���ׂẴ^�u
    [SerializeField]
    private GameObject[] _tabs;

    [SerializeField]
    private Image[] _tabButtons;

    //�{�^���̏�ԃJ���[
    [SerializeField]
    private Color _selectedColor;
    [SerializeField]
    private Color _unselectedColor;

    //�{�^���̏�ԃT�C�Y
    [SerializeField]
    private Vector2 _selectedSize;
    [SerializeField]
    private Vector2 _unselectedSize;

    public void OnClickTab(int index)
    {
        for (int i = 0; i < _tabs.Length; i++)
        {
            if (i == index)
            {
                _tabs[i].SetActive(true);
                _tabButtons[i].color = _selectedColor;
                _tabButtons[i].rectTransform.sizeDelta = _selectedSize;
            }
            else
            {
                _tabs[i].SetActive(false);
                _tabButtons[i].color = _unselectedColor;
                _tabButtons[i].rectTransform.sizeDelta = _unselectedSize;
            }
        }
    }
}
