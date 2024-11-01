using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(CircleCollider2D))]
public class OutNode : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UILineConnector _lineRenderer;
    [SerializeField]
    private RectTransform _rectOriginPosition;
    private RectTransform _rectMyTransform;

    [SerializeField]
    private LayerMask _collisionLayerMask;

    private CircleCollider2D _circleCollider;
    private RaycastHit2D[] _collisionResults;

    private BaseStateNode _nextStateNode;
    private bool _isConect = default;
    private bool _isComponentNull = default;

    public RectTransform MyRectTransform { get => _rectMyTransform; }
    public BaseStateNode NextNodeState { get => _nextStateNode; }
    public bool IsConect { get => _isConect; }
    private void Start()
    {
        _rectMyTransform = transform as RectTransform;
        CheckComponentNull();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        _rectMyTransform.position = TargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isComponentNull)
        {
            CheckCollisionNode();
        }  
    }

    public void SetParentNodeState(GameObject stateNode)
    {

    }

    private void CheckCollisionNode()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius, Vector2.down, 0, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            if (CheckHasComponent(_collisionResults[i]))
            {
                return;
            }
        }
        ResetNode();
    }

    private bool CheckHasComponent(RaycastHit2D raycastHit)
    {
        bool hasComponent = default;
        BaseStateNode stateNode;

        if (raycastHit.collider.TryGetComponent<BaseStateNode>(out stateNode))
        {
            //if ()
            //{
                hasComponent = true;
                UpDateConectState(stateNode);
            //}
        }
        else
        {
            hasComponent = false;
            ResetNode();
        }

        _isConect = hasComponent;
        print(hasComponent + " " + raycastHit.collider.name);
        return hasComponent;
    }

    private void UpDateConectState(BaseStateNode stateNode)
    {
        transform.position = _rectOriginPosition.TransformPoint(_rectOriginPosition.anchoredPosition3D);
        _nextStateNode = stateNode;

        _lineRenderer.transforms = new RectTransform[]
        {
            _rectOriginPosition,
            stateNode.InNode.RectMyTransform
        };
    }

    private void ResetNode()
    {
        transform.position = _rectOriginPosition.TransformPoint(_rectOriginPosition.anchoredPosition3D);
        _isConect = false;

        _lineRenderer.transforms = new RectTransform[]
       {
            _rectOriginPosition,
            _rectMyTransform
       };
    }

    private void CheckComponentNull()
    {
        if (!TryGetComponent<CircleCollider2D>(out _circleCollider))
        {
            Debug.Log(transform.name + " " + "サークル無い");
            _isComponentNull = true;
        }
        if (_lineRenderer == null)
        {
            Debug.Log(transform.name + " " + "ライン無い");
            _isComponentNull = true;
        }
    }
}
