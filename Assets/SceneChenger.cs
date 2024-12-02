using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    StartMenuScene,
    TestMoveScene,
    Algore,
    ClearScene,
    GameOverScene
}

public class SceneChanger : MonoBehaviour
{
    // ÉVÅ[ÉìñºÇóÒãìå^Ç≈ä«óù
    

    [SerializeField]
    private Scenes targetScene;

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

    public void ChangeScene(Scenes scene)
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
