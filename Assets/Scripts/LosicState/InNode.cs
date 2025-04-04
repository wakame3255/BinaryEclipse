using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InNode : MonoBehaviour
{
    private RectTransform _rectMyTransform;

    public RectTransform RectMyTransform { get => _rectMyTransform; }

    private void Start()
    {
        _rectMyTransform = transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        transform.position = TargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public BaseStateNode ReturnStateNode()
    {
        return null;
    }
}
