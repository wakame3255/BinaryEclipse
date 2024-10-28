using UnityEngine;

public class Walk : MonoBehaviour, IWalk
{
    [SerializeField]
    private float _speed;

    public void DoWalk(float inputX, float inputY)
    {
        transform.position += new Vector3(inputX, inputY, 0)* _speed * Time.deltaTime;
    }
}
