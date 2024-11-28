
using UnityEngine;
using UnityEngine.DedicatedServer;

public static class MyExtensionClass 
{
    /// <summary>
    /// �R���|�[�l���g���݊m�F�B�Ȃ������ꍇ��Add���s��
    /// </summary>
    /// <typeparam name="T">�`�F�b�N�̍s���R���|�[�l���g</typeparam>
    /// <param name="monoBehaviour">�g�����\�b�h���Ăяo��MonoBehaviour</param>
    /// <returns>�R���|�[�l���g</returns>
    public static T CheckComponentMissing<T>(this MonoBehaviour monoBehaviour, GameObject gameObject = null) where T : class
    {
        T component = monoBehaviour.GetComponent(typeof(T)) as T;
        if (component == null)
        {
            Debug.LogError(monoBehaviour.transform.name + " " + typeof(T).FullName + "������Ȃ���");

            //�t�������I�u�W�F�N�g�̎w��
            if (typeof(Component).IsAssignableFrom(typeof(T)))
            {
                if (gameObject == null)
                {
                    component = monoBehaviour.gameObject.AddComponent(typeof(T)) as T;
                }
                else
                {
                    component = gameObject.AddComponent(typeof(T)) as T;
                }
            }
        }
        return component;
    }

    /// <summary>
    /// ������null���ǂ������`�F�b�N���郁�\�b�h
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arugment">����</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static void CheckArgumentNull<T>(T arugment, string arugmentName)
    {
        if (arugment == null)
        {
            throw new System.ArgumentNullException(arugmentName);
        }
    }
}
