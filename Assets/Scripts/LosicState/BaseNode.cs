using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public abstract class BaseNode : MonoBehaviour, IDragHandler,IEndDragHandler
{
    private protected Transform _myTransform;
    private protected LineRenderer _lineRenderer;

    private Vector3 _startPosition;
    private protected bool _isConect;

    public Transform MyTransform { get => _myTransform; }
    protected virtual void Start()
    {
        _myTransform = transform;
        _startPosition = _myTransform.position;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    protected virtual void Update()
    {
        _lineRenderer.SetPosition(0, _startPosition);
        _lineRenderer.SetPosition(1, _myTransform.position);
    }

    public virtual void OnDrag(PointerEventData eventData) { }

    public virtual void OnEndDrag(PointerEventData eventData) { }
}
