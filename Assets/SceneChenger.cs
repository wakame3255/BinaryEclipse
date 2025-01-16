using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScenesNames
{
    StartMenuScene,
    TestMoveScene,
    Algore,
    ClearScene,
    GameOverScene,
    none
}

public class SceneChanger : MonoBehaviour
{
    // ÉVÅ[ÉìñºÇóÒãìå^Ç≈ä«óù
    

    [SerializeField]
    private ScenesNames targetScene;

    public void ChangeScene()
    {
        string sceneName = targetScene.ToString();

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' is not in Build Settings. Please add it.");
        }
    }

    public void ChangeScene(ScenesNames scene)
    {
        string sceneName = scene.ToString();

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' is not in Build Settings. Please add it.");
        }
    }
}
