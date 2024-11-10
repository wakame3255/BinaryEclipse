
using UnityEngine;

public static class MyExtensionClass 
{
    /// <summary>
    /// �R���|�[�l���g���݊m�F�B�Ȃ������ꍇ��Add���s��
    /// </summary>
    /// <typeparam name="T">�`�F�b�N�̍s���R���|�[�l���g</typeparam>
    /// <param name="monoBehaviour">�g�����\�b�h���Ăяo��MonoBehaviour</param>
    /// <returns>�R���|�[�l���g</returns>
    public static T CheckComponentMissing<T>(this MonoBehaviour monoBehaviour) where T : Component
    {
        T component;
        if (!monoBehaviour.TryGetComponent<T>(out component))
        {
            Debug.LogError(monoBehaviour.transform.name + " " + typeof(T).FullName + "������Ȃ���");
            component = monoBehaviour.gameObject.AddComponent<T>();
        }
        return component;
    }
}
