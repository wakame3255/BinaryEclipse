using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnusedAssetsCleaner : EditorWindow
{
    private List<string> unusedAssets = new List<string>();
    private Vector2 scrollPosition;
    private HashSet<string> excludedFolders = new HashSet<string> { "Assets/Editor", "Assets/Resources", "Assets/StreamingAssets", "Assets" }; // Assetsフォルダを除外

    [MenuItem("Tools/Unused Assets Cleaner")]
    public static void ShowWindow()
    {
        GetWindow<UnusedAssetsCleaner>("Unused Assets Cleaner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Unused Assets Cleanup Tool", EditorStyles.boldLabel);

        if (GUILayout.Button("Scan for Unused Assets"))
        {
            ScanForUnusedAssets();
        }

        if (unusedAssets.Count > 0)
        {
            GUILayout.Label($"Found {unusedAssets.Count} potentially unused assets:");

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            foreach (var asset in unusedAssets)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(asset);
                if (GUILayout.Button("Delete", GUILayout.Width(60)))
                {
                    DeleteAsset(asset);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Delete All Selected"))
            {
                DeleteAllUnusedAssets();
            }
        }
    }

    private void ScanForUnusedAssets()
    {
        unusedAssets.Clear();
        var allAssets = AssetDatabase.GetAllAssetPaths()
            .Where(IsValidAsset)
            .ToHashSet();

        var usedAssets = new HashSet<string>();

        // Scan scenes
        foreach (var scenePath in AssetDatabase.FindAssets("t:Scene")
                 .Select(guid => AssetDatabase.GUIDToAssetPath(guid)))
        {
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
            foreach (var obj in GetSceneDependencies(scene))
            {
                usedAssets.Add(obj);
            }
            EditorSceneManager.CloseScene(scene, true);
        }

        // Scan prefabs
        foreach (var prefabPath in AssetDatabase.FindAssets("t:Prefab")
                 .Select(guid => AssetDatabase.GUIDToAssetPath(guid)))
        {
            foreach (var dependency in AssetDatabase.GetDependencies(prefabPath, true))
            {
                usedAssets.Add(dependency);
            }
        }

        unusedAssets = allAssets.Except(usedAssets).ToList();
        unusedAssets.Sort();
    }

    private bool IsValidAsset(string path)
    {
        // Exclude specific folders and file types
        if (excludedFolders.Any(folder => path.StartsWith(folder))) return false;
        if (path.EndsWith(".cs")) return false;
        if (path.EndsWith(".shader")) return false;
        return true;
    }

    private IEnumerable<string> GetSceneDependencies(Scene scene)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            foreach (var component in go.GetComponentsInChildren<Component>())
            {
                if (!component) continue;

                SerializedObject so = new SerializedObject(component);
                var iterator = so.GetIterator();

                while (iterator.NextVisible(true))
                {
                    if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        var obj = iterator.objectReferenceValue;
                        if (obj != null)
                        {
                            var path = AssetDatabase.GetAssetPath(obj);
                            if (!string.IsNullOrEmpty(path)) yield return path;
                        }
                    }
                }
            }
        }
    }

    private void DeleteAsset(string assetPath)
    {
        if (assetPath == "Assets") // Assetsフォルダ自体を削除しない
        {
            Debug.LogWarning("Cannot delete the 'Assets' folder.");
            return;
        }

        if (AssetDatabase.DeleteAsset(assetPath))
        {
            Debug.Log($"Deleted unused asset: {assetPath}");
            unusedAssets.Remove(assetPath);
        }
    }

    private void DeleteAllUnusedAssets()
    {
        foreach (var asset in unusedAssets.ToList())
        {
            DeleteAsset(asset);
        }
    }
}