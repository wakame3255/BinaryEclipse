using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(CircleCollider2D))]
public class OutNode : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UILineRenderer _lineRenderer;
    [SerializeField]
    private Transform _originPosition;
    [SerializeField]
    private LayerMask _collisionLayerMask;

    private CircleCollider2D _circleCollider;
    private RaycastHit2D[] _collisionResults;

    private BaseStateNode _nextNodeState;
    private Transform _myTransform;

    private bool _isConect = default;
    private bool _isComponentNull = default;

    public BaseStateNode BaseStateNode { get => _nextNodeState; }
    public bool IsConect { get => _isConect; }
    private void Start()
    {
        _myTransform = transform;
        CheckComponentNull();
    }

    private void Update()
    {
        if (!_isComponentNull)
        {
            UpDateNodePosition(_isConect);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        _myTransform.position = TargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckCollisionNode();
        print("やめ");
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
        bool hasComponent;
        BaseStateNode inNode;

        if (raycastHit.collider.TryGetComponent<BaseStateNode>(out inNode))
        {
            hasComponent = true;
            UpDateConectState(inNode);
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

    private void UpDateConectState(BaseStateNode inNode)
    {
        _nextNodeState = inNode;
    }

    private void UpDateNodePosition(bool isConectNode)
    {
        if (isConectNode)
        {
            transform.position = _nextNodeState.InNode.transform.position;
        }
        UpDateLine();
    }

    private void UpDateLine()
    {
        _lineRenderer.Points = new Vector2[]{
            new Vector2(_originPosition.position.x, _originPosition.position.y),
            new Vector2(_myTransform.position.x, _myTransform.position.y)
         };
    }

    private void ResetNode()
    {
        transform.position = _originPosition.position;
        _isConect = false;
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
