using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateChangeMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _stateUI;

    private bool _isActive;

    private void Update()
    {
        if (PlayerController.Instance.IsMenu)
        {
            if (_isActive)
            {
                _isActive = false;
                _stateUI.SetActive(false);
            }
            else
            {
                _isActive = true;
                _stateUI.SetActive(true);
            }
        }
    }
}
