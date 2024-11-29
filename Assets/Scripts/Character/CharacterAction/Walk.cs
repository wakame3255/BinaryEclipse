using UnityEngine;

public class Walk : MonoBehaviour, IWalk
{
    [SerializeField]
    private float _speed;

    public void DoWalk(float inputX, float inputY, Transform myTransform)
    {
        MyExtensionClass.CheckArgumentNull(inputX, nameof(inputX));
        MyExtensionClass.CheckArgumentNull(myTransform, nameof(myTransform));


        myTransform.position += new Vector3(inputX, inputY, 0)* _speed * Time.deltaTime;
    }
}
