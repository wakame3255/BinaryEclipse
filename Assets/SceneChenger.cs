using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private SceneAsset _sceneAsset;

    private string SceneName => _sceneAsset != null ? _sceneAsset.name : string.Empty;

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(SceneName))
        {
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set.");
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
